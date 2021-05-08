using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderView : MonoBehaviour
{
    private List<GameObject> _currentRecipe = new List<GameObject>();
    
    public void UpdateRecipe(Recipe recipe)
    {
        CleanRecipe();
        
        foreach (IngredientData ingredient in recipe.ingredients)
        {
            SetIngredientImage(ingredient.grabbedImage);
        }
    }

    public void SetIngredientImage(Sprite img)
    {
        var imageObject = Resources.Load<Image>("ClientIngredientView");
        Image uiObject = Instantiate(imageObject, transform);
        uiObject.sprite = img;
        
        _currentRecipe.Add(uiObject.gameObject);
    }

    public void CleanRecipe()
    {
        for (int i = 0; i < _currentRecipe.Count; i++)
        {
            Destroy(_currentRecipe[i].gameObject);
        }
        
        _currentRecipe.Clear();
    }
}