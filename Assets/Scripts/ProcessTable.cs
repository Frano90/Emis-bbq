using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessTable : ItemProcessor
{
    public override void OnReceiveIngredient(IPickable pickable)
    {
        if(onHoverParticles_FB.isPlaying) onHoverParticles_FB.Stop();

        if(currentKitchenItemHolding != null) return;
        
        if (!(pickable is ICortable)) return;
        
        pickable.MoveTo(this);
        currentKitchenItemHolding = pickable;
        
    }
}
