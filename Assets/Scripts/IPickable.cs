using System;
using UnityEngine;

public interface IPickable
{
    void PickUp();
    void Release();
    void MoveTo(PickableReceiver receiver);
    PickableReceiver GetCurrentReceiver();
    void Delete();

    bool Grabbed();
    Vector3 GetPosition();

    Sprite GetGrabImage();
}
