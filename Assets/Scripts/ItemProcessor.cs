using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProcessor : PickableReceiver, IKitchenItemProcessor
{
    private float _count;  
    
    public override void OnReceiveIngredient(IPickable pickable)
    {
        if (!(pickable is IGrillable)) return;
      
        pickable.MoveTo(this);
        currentKitchenItemHolding = pickable as Ingredient;
    }

    private void Update()
    {
        if(currentKitchenItemHolding != null) ProcessKitchenItem();
        
        uiPanel.SetActive(currentKitchenItemHolding != null);
    }
    
    public void ProcessKitchenItem()
    {
        var currentIngridient = currentKitchenItemHolding as Ingredient;
        var data = currentIngridient.CurrentIngredientData;
        
        _count += Time.deltaTime;
        
        if (_count >= data.processTime)
        {
            currentIngridient.Process();
            _count = 0;
        }
        
        processBar.fillAmount = _count / data.processTime;
    }
}
