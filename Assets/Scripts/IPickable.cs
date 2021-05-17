using System;
using UnityEngine;

public interface IPickable
{
    void PickUp();
    void Release();
    void MoveTo(PickableReceiver receiver);

    PickableReceiver GetCurrentReceiver();
    void Delete();

    Vector3 GetPosition();

    Sprite GetGrabImage();
}
