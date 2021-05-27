using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessTable : ItemProcessor
{
    public override void OnReceiveIngredient(IPickable pickable)
    {
        if(currentKitchenItemHolding != null) return;
        
        if (!(pickable is ICortable)) return;
        
        pickable.MoveTo(this);
        currentKitchenItemHolding = pickable;
        
    }
}
