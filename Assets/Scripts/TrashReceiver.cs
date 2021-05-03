﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashReceiver : PickableReceiver
{
    public override void OnReceiveIngredient(IPickable pickable)
    {
        base.OnReceiveIngredient(pickable);

        pickable.Delete();
    }
}
