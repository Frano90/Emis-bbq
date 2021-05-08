using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientDispatcher : MonoBehaviour
{

    [SerializeField] private string ingredientName;
    private Ingredient currentIngredient;
    
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
        currentIngredient.OnMoveToAnotherPlace -= DispatchIngredient;
        
        CreateNewIngredient();
    }
}
