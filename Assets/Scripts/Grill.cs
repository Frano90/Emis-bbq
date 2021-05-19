using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : ItemProcessor
{
    public override void OnReceiveIngredient(IPickable pickable)
    {
        if(onHoverParticles_FB.isPlaying) onHoverParticles_FB.Stop();

        if(currentKitchenItemHolding != null) return;
        
        if (!(pickable is IGrillable)) return;
        
        pickable.MoveTo(this);
        currentKitchenItemHolding = pickable;
    }
    
    
}
