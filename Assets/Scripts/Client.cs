using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class Client : PickableReceiver
{
    [SerializeField] private Recipe[] recipes;
    private Recipe _currentRecipe;
    [SerializeField] OrderView orderView;

    private float elapsedTimeOrdering = 0;
    [SerializeField] private float totalOrderTime;

    [SerializeField] private Animator _anim;

    private bool makeMyOrder = false;
    public event Action OnFinishRecieveOrderFeedback;

    public void Init()
    {
        StartCoroutine(ClientIntro());

        elapsedTimeOrdering = totalOrderTime;
    }

    IEnumerator ClientIntro()
    {
        float count = 0;
        float introTime = 2;
        _anim.Play("Ask");
        while (count <= introTime)
        {
            count += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        Order();
    }
    private void Update()
    {
        OrderViewUpdate();
    }

    private void OrderViewUpdate()
    {
        if (orderView == null) return;

        if (!makeMyOrder) return;

        if (elapsedTimeOrdering < 0)
        {
            CheckOrder(false);
            return;
        }

        elapsedTimeOrdering -= Time.deltaTime;

        orderView.RefreshClockView(elapsedTimeOrdering, totalOrderTime);
    }

    public void Order()
    {
        makeMyOrder = true;
        int rgn = Random.Range(0, recipes.Length);
        _currentRecipe = recipes[rgn];
        
        orderView.UpdateRecipe(_currentRecipe);
    }

    public override void OnReceiveIngredient(IPickable pickable)
    {
        if (!(pickable is IEntregable)) return;
        
        bool isGood = true;
        
        pickable.MoveTo(this);
        pickable.Delete();
        
        IEntregable entregable = pickable as IEntregable;
        List<IngredientData> auxIngredientList = entregable.GetIngredientsInOrder();

        if (!auxIngredientList.SequenceEqual(_currentRecipe.ingredients))
            isGood = false;

        CheckOrder(isGood);
    }

    void CheckOrder(bool isGood)
    {
        Destroy(orderView.gameObject);
        OrderStatus orderStatus = OrderStatus.Good;
        
        if (isGood)
        {
            _anim.Play("GoodOrder");
        }
        else
        {
            _anim.Play("BadOrder");
        }

        
        float percent = elapsedTimeOrdering / totalOrderTime;
        Debug.Log(isGood + " " + percent);
        if (percent <= 0 || !isGood)    //Sacar a un metodo que calcule order status asi es mas legible
        {
            orderStatus = OrderStatus.Unacceptable;
        }
        else if (percent < .3f)
        {
            orderStatus = OrderStatus.Bad;
        }
        else if(percent <= .6f)
        {
            orderStatus = OrderStatus.Regular;
        }else if (percent <= 1f)
        {
            orderStatus = OrderStatus.Good;
        }
        
        StartCoroutine(ReciveOrder());
        
        
        Main.instance.eventManager.TriggerEvent(GameEvent.ClientDonePurchase, orderStatus, _currentRecipe);
    }

    IEnumerator ReciveOrder()
    {
        float count = 0;
        while (count <= 5f)
        {
            count += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        OnFinishRecieveOrderFeedback?.Invoke();
        Delete();
    }

    public void GoToWindow(Transform windowTransform)
    {
        transform.position = windowTransform.position;
        transform.rotation = windowTransform.rotation;
    }

    public enum OrderStatus
    {
        Undefined,
        Unacceptable,
        Bad,
        Regular,
        Good
    }

}
