using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Client : PickableReceiver
{
    [SerializeField] private Recipe[] recipes;
    private Recipe _currentRecipe;
    [SerializeField] OrderView orderView;

    private float elapsedTimeOrdering = 0;
    [SerializeField]private float totalOrderTime;

    public int MyWindow { get; set; }
    public event Action<bool, Client> OnReceiveOrder;

    public void Init()
    {
        Order();

        elapsedTimeOrdering = totalOrderTime;
    }

    private void Update()
    {
        if(elapsedTimeOrdering < 0) return;
        
        elapsedTimeOrdering -= Time.deltaTime;
        
        orderView.RefreshClockView(elapsedTimeOrdering, totalOrderTime);
    }

    public void Order()
    {
        int rgn = Random.Range(0, recipes.Length);
        _currentRecipe = recipes[rgn];
        
        orderView.UpdateRecipe(_currentRecipe);
    }

    public override void OnReceiveIngredient(IPickable pickable)
    {
        if(onHoverParticles_FB.isPlaying) onHoverParticles_FB.Stop();
        
        if (!(pickable is IEntregable)) return;
        
        pickable.MoveTo(this);
        pickable.Delete();
        
        IEntregable entregable = pickable as IEntregable;
        List<IngredientData> auxIngredientList = entregable.GetIngredientsInOrder();

        //Check si tiene la misma cantidad de ingredientes de lo que pidio
        if (_currentRecipe.ingredients.Length != entregable.GetIngredientsInOrder().Count)
        {
            BadOrder(pickable);
            return;
        }
        
        for (int i = 0; i < auxIngredientList.Count; i++)
        {
            if (!_currentRecipe.ingredients[i].Equals(auxIngredientList[i]))
            {
                BadOrder(pickable);
                return;
            }
        }
        
        GoodOrder();
        
    }

    void BadOrder(IPickable pickable)
    {
        Debug.Log("MAL PEDIDO");
        pickable.Delete();
        OnReceiveOrder?.Invoke(false, this);
        
        Destroy(gameObject);
    }

    void GoodOrder()
    {
        OnReceiveOrder?.Invoke(true, this);
        
        Debug.Log("ES MI PEDIDO");

        ComputeScore();
        Destroy(gameObject);
    }

    private void ComputeScore()
    {
        //Como hacemos esto?
    }
}
