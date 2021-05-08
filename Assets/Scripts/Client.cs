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

    private void Start()
    {
        Order();
    }

    public void Order()
    {
        int rgn = Random.Range(0, recipes.Length);
        _currentRecipe = recipes[rgn];
        
        orderView.UpdateRecipe(_currentRecipe);
    }

    public override void OnReceiveIngredient(IPickable pickable)
    {
        if (pickable is PreparedDish)
        {
            PreparedDish preparedDish = pickable as PreparedDish;
            List<IngredientData> auxIngredientList = preparedDish.GetIngredientsInOrder();

            if (_currentRecipe.ingredients.Length != preparedDish.GetIngredientsInOrder().Count)
            {
                pickable.Delete();
                return;
            }
            
            for (int i = 0; i < auxIngredientList.Count; i++)
            {
                if (!_currentRecipe.ingredients[i].Equals(auxIngredientList[i]))
                {
                    Debug.Log("NO ERA MI PEDIDO");
                    return;
                }
            }

            Debug.Log("ES MI PEDIDO");
        }
    }

    public void CheckIfOrderIsGood(Recipe myFood)
    {
        
    }
    
    
    protected override void Update(){ }
}
