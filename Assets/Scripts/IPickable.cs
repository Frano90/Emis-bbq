using System;
using UnityEngine;

public interface IPickable
{
    void PickUp();
    void Release();
    void MoveTo(Vector3 newPos, PickableReceiver receiver);

    PickableReceiver GetCurrentReceiver();
    void Delete();

    Vector3 GetPosition();
    void AddPickUpListener(Action callback);
    void AddReleaseListener(Action callback);
    void RemovePickUpListener(Action callback);
    void RemoveReleaseListener(Action callback);
}
