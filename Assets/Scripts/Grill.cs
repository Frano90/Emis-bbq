using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : ItemProcessor
{
    
    [SerializeField] private ParticleSystem grillFire_feedback;

    protected override void Update()
    {
        base.Update();
        
        if (currentKitchenItemHolding != null)
        {
            if(!grillFire_feedback.isPlaying) grillFire_feedback.Play();
        }
        
        if (currentKitchenItemHolding == null)
        {
            if(grillFire_feedback.isPlaying) grillFire_feedback.Stop();
        }
    }

    public override void OnReceiveIngredient(IPickable pickable)
    {
        if(currentKitchenItemHolding != null) return;
        
        if (!(pickable is IGrillable)) return;
        
        pickable.MoveTo(this);
        currentKitchenItemHolding = pickable;
    }
    
    
}
