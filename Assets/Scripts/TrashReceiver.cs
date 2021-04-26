using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashReceiver : ObjectReceiver
{
    public override void OnReceiveItem(Ingridient ingridient)
    {
        base.OnReceiveItem(ingridient);

        ingridient.Delete();
    }
}
