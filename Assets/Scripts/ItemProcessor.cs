using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProcessor : PickableReceiver, IKitchenItemProcessor
{
    protected virtual void Update()
    {
        if (currentKitchenItemHolding != null)
        {
            ProcessKitchenItem();
        }
        else
        {
            _count = 0;
        }
        
        uiPanel.SetActive(currentKitchenItemHolding != null);
        
    }
    
    public void ProcessKitchenItem()
    {
        if(currentKitchenItemHolding.Grabbed()) return;
        
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
