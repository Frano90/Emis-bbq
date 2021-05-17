using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessTable : PickableReceiver
{
    public override void OnReceiveIngredient(IPickable pickable)
    {
        if (pickable is IProcesable)
        {
            //_currentIngredient = pickable as Ingredient;
            _currentIngredient.MoveTo(this);
        
            
        }
        
        if(onHoverParticles_FB.isPlaying) onHoverParticles_FB.Stop();
    }
}
