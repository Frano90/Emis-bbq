using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderView : MonoBehaviour
{
    [SerializeField] private List<GameObject> _currentRecipe = new List<GameObject>();

    [SerializeField] private Image clockView;
    public void UpdateRecipe(Recipe recipe)
    {
        CleanRecipe();
        
        foreach (IngredientData ingredient in recipe.ingredients)
        {
            SetIngredientImage(ingredient.grabbedImage);
        }
    }

    public void RefreshClockView(float currentTime, float maxTime)
    {
        float percent = currentTime / maxTime;
        clockView.fillAmount = percent;

        if (percent < .3f)
        {
            clockView.color = Color.red;
        }
        else if(percent <= .6f)
        {
            clockView.color = Color.yellow;
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