﻿using System;
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
        
        
        if (pickable is IEntregable)
        {
            if(onHoverParticles_FB.isPlaying) onHoverParticles_FB.Stop();
            pickable.MoveTo(this);
            pickable.Delete();
            
            IEntregable entregable = pickable as IEntregable;
            //PreparedDish preparedDish = pickable as PreparedDish;
            List<IngredientData> auxIngredientList = entregable.GetIngredientsInOrder();

            if (_currentRecipe.ingredients.Length != entregable.GetIngredientsInOrder().Count)
            {
                Debug.Log("MAL PEDIDO");
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

    protected override void Update(){ }
}
