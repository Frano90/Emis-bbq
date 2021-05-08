using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour, IPickable
{
    [SerializeField] private IngredientData[] _ingridientStates;
    private int _currentIngridientStateIndex = 0;

    [SerializeField] private Transform modelView;

    [SerializeField]private GrabbedItemView _grabbedItemView;

    private PickableReceiver _currentReceiver;
    public event Action OnMoveToAnotherPlace;
    public IngredientData CurrentIngredientData => _ingridientStates[_currentIngridientStateIndex];

    void Start()
    {
        _grabbedItemView = new GrabbedItemView(modelView);

        _ingridientStates[_currentIngridientStateIndex].Enter(modelView);
    }

    public PickableReceiver GetCurrentReceiver()
    {
        return _currentReceiver;
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
    public void Process()
    {
        _ingridientStates[_currentIngridientStateIndex].Exit(modelView);
        
        if (_currentIngridientStateIndex + 1 >= _ingridientStates.Length)
        {
            Delete();
            return;
        }
        _currentIngridientStateIndex++;
        
        _ingridientStates[_currentIngridientStateIndex].Enter(modelView);
    }

    public void PickUp()
    {
        _grabbedItemView.EnablePickUpFeedback();
    }

    public void Release()
    {
        _grabbedItemView.DisablePickUpFeedback();
    }

    public void MoveTo(PickableReceiver receiver)
    {
        OnMoveToAnotherPlace?.Invoke();
        
        _currentReceiver = receiver;
        
        if (receiver == null) return;
        transform.position = receiver.PlaceToPutObject.position;
        
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void AddPickUpListener(Action callback)
    {
        
    }

    public void AddReleaseListener(Action callback)
    {
        
    }

    public void RemovePickUpListener(Action callback)
    {
        
    }

    public void RemoveReleaseListener(Action callback)
    {
        
    }
}


