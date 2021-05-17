﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashReceiver : PickableReceiver
{
    public override void OnReceiveIngredient(IPickable pickable)
    {
        
        if(onHoverParticles_FB.isPlaying) onHoverParticles_FB.Stop();
        pickable.MoveTo(this);

        pickable.Delete();
    }
}
