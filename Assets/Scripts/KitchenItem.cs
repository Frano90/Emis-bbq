using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenItem : MonoBehaviour, IPickable
{
    private PickableReceiver _currentReceiver;
    public event Action OnMoveToAnotherPlace;
    
    [SerializeField] protected Transform modelView;
    [SerializeField] protected Sprite grabImage;
    [SerializeField] protected GrabbedItemView _grabbedItemView;


    protected virtual void Start()
    {
        _grabbedItemView = new GrabbedItemView(modelView);
        Main.instance.eventManager.SubscribeToEvent(GameEvent.OnGrabPickable, DisableCollider);
        Main.instance.eventManager.SubscribeToEvent(GameEvent.OnReleasePickable, EnableCollider);
    }

    void DisableCollider()
    {
        GetComponent<Collider>().enabled = false;
    }
    
    void EnableCollider()
    {
        GetComponent<Collider>().enabled = true;
    }
    
    public void PickUp()
    {
        _grabbedItemView.EnablePickUpFeedback();
    }

    public void Release()
    {
        _grabbedItemView.DisablePickUpFeedback();
    }

    public void MoveTo(PickableReceiver receiver)
    {
        OnMoveToAnotherPlace?.Invoke();
        
        _currentReceiver = receiver;
        
        if (receiver == null) return;
        transform.position = receiver.PlaceToPutObject.position;
    }

    public PickableReceiver GetCurrentReceiver()
    {
        return _currentReceiver;
    }

    public void Delete()
    {
        if(_currentReceiver != null) _currentReceiver.RemovePickable();
        
        Main.instance.eventManager.UnsubscribeToEvent(GameEvent.OnGrabPickable, DisableCollider);
        Main.instance.eventManager.UnsubscribeToEvent(GameEvent.OnReleasePickable, EnableCollider);
        
        Destroy(gameObject);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Sprite GetGrabImage()
    {
        return grabImage;
    }
}
