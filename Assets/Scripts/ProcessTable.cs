﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessTable : ItemProcessor
{
    //private float _count;
    public override void OnReceiveIngredient(IPickable pickable)
    {
        if(onHoverParticles_FB.isPlaying) onHoverParticles_FB.Stop();

        if (!(pickable is ICortable)) return;
        
        pickable.MoveTo(this);
        currentKitchenItemHolding = pickable;
        
    }
}
