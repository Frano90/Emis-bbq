using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientDispatcher : MonoBehaviour
{


    [SerializeField] private Ingridient currentIngredient;
    
    private PickableReceiver _currentPickableReceiver;
    
    private void Start()
    {
        CreateNewIngredient();
    }

    private void CreateNewIngredient()
    {
       Ingridient newIngredient = Resources.Load<Ingridient>("IngredienteQueso");
       currentIngredient = Instantiate(newIngredient, transform.position, Quaternion.identity, transform);
       currentIngredient.OnMoveToAnotherPlace += DispatchIngredient;
    }

    private void DispatchIngredient()
    {
        currentIngredient.OnMoveToAnotherPlace -= DispatchIngredient;
        
        CreateNewIngredient();
    }
}
