using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickableReceiver : MonoBehaviour
{

    [SerializeField] protected Image processBar; 
    [SerializeField] protected GameObject uiPanel;

    [SerializeField] private Transform placeToPutObject;

    [SerializeField]protected IPickable currentKitchenItemHolding;

    protected float _count;

    private void Start()
    {
        Main.instance.eventManager.SubscribeToEvent(GameEvent.OnGrabPickable, EnableCollider);
        Main.instance.eventManager.SubscribeToEvent(GameEvent.OnReleasePickable, DisableCollider);
    }
    
    void DisableCollider()
    {
        GetComponent<Collider>().enabled = false;
    }
    
    void EnableCollider()
    {
        GetComponent<Collider>().enabled = true;
    }

    public Transform PlaceToPutObject => placeToPutObject;
    public void OnDragObjectHover() {}

    public void OnExitDragObjectHover(){}
    
    public virtual void OnReceiveIngredient(IPickable pickable){}

    public void RemovePickable()
    {
        _count = 0;
        currentKitchenItemHolding = null;
    }

    public void Delete()
    {
        Main.instance.eventManager.UnsubscribeToEvent(GameEvent.OnGrabPickable, EnableCollider);
        Main.instance.eventManager.UnsubscribeToEvent(GameEvent.OnReleasePickable, DisableCollider);
        
        Destroy(gameObject);
    }
}
