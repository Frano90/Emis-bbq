using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientDispatcher : MonoBehaviour
{

    [SerializeField] private string ingredientName;
    private Ingredient currentIngredient;
    [SerializeField] private int costMoney;

    private PickableReceiver _currentPickableReceiver;
    
    private void Start()
    {
        CreateNewIngredient();
    }

    private void CreateNewIngredient()
    {
       Ingredient newIngredient = Resources.Load<Ingredient>(ingredientName);
       currentIngredient = Instantiate(newIngredient, transform.position, Quaternion.identity, transform);
       currentIngredient.OnMoveToAnotherPlace += DispatchIngredient;
    }

    private void DispatchIngredient()
    {
        Main.instance.eventManager.TriggerEvent(GameEvent.BuyIngredient, costMoney);
        
        currentIngredient.OnMoveToAnotherPlace -= DispatchIngredient;
        
        CreateNewIngredient();
    }
}
