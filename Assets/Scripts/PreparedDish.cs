using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparedDish : Ingredient //Cambiar esto a que no herede de ingrediente
{
    [SerializeField] private List<IngredientData> _currentRecipe = new List<IngredientData>();

    public void AddIngredient(IngredientData iData)
    {
        _currentRecipe.Add(iData);
    }

    public List<IngredientData> GetIngredientsInOrder() => _currentRecipe;
}
