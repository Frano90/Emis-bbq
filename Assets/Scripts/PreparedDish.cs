using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparedDish : MonoBehaviour, IEntregable, IPickable
{
    [SerializeField] private List<IngredientData> _currentRecipe = new List<IngredientData>();

    private PickableReceiver _currentReceiver;
    
    [SerializeField] private Transform modelView;
    
    [SerializeField]private GrabbedItemView _grabbedItemView;
    public event Action OnMoveToAnotherPlace;

    [SerializeField] private Sprite grabImage;
    
    public void AddIngredient(IngredientData iData)
    {
        _currentRecipe.Add(iData);
    }

    public List<IngredientData> GetIngredientsInOrder() => _currentRecipe;

    void Start()
    {
        _grabbedItemView = new GrabbedItemView(modelView);
    }
    public void Entregar()
    {
        
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

    public PickableReceiver GetCurrentReceiver()
    {
        return _currentReceiver;
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Sprite GetGrabImage()
    {
        return grabImage;
    }
}
