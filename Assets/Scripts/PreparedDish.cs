using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparedDish : KitchenItem, IEntregable 
{
    [SerializeField] private List<IngredientData> _currentRecipe = new List<IngredientData>();

    [SerializeField] private Sprite grabImage;
    
    public void AddIngredient(IngredientData iData)
    {
        _currentRecipe.Add(iData);
    }

    public List<IngredientData> GetIngredientsInOrder() => _currentRecipe;
    
    public void Entregar()
    {
        
    }

    public override Sprite GetGrabImage()
    {
        return grabImage;
    }
}
