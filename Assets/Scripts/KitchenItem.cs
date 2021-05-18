﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenItem : MonoBehaviour, IPickable
{
    private PickableReceiver _currentReceiver;
    public event Action OnMoveToAnotherPlace;
    
    [SerializeField] protected Transform modelView;

    [SerializeField]private GrabbedItemView _grabbedItemView;


    protected virtual void Start()
    {
        _grabbedItemView = new GrabbedItemView(modelView);
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
        Destroy(gameObject);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public virtual Sprite GetGrabImage()
    {
        throw new System.NotImplementedException();
    }
}