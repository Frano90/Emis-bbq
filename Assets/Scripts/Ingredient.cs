using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : KitchenItem, IProcesable
{
    [SerializeField] private IngredientData[] _ingridientStates;
    private int _currentIngridientStateIndex = 0;
    
    public IngredientData CurrentIngredientData => _ingridientStates[_currentIngridientStateIndex];

    protected override void Start()
    {
        base.Start();
        _ingridientStates[_currentIngridientStateIndex].Enter(modelView);
    }
    
    public void Process()
    {
        _ingridientStates[_currentIngridientStateIndex].Exit(modelView);
        
        if (_currentIngridientStateIndex + 1 >= _ingridientStates.Length)
        {
            Delete();
            return;
        }
        _currentIngridientStateIndex++;
        
        _ingridientStates[_currentIngridientStateIndex].Enter(modelView);
    }
    
    public override Sprite GetGrabImage()
    {
        return CurrentIngredientData.grabbedImage;
    }
}


