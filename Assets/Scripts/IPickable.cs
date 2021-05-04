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
    void AddPickUpListener(Action callback);
    void AddReleaseListener(Action callback);
    void RemovePickUpListener(Action callback);
    void RemoveReleaseListener(Action callback);
}
