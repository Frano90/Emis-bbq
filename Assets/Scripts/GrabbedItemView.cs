using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbedItemView : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Material originalMaterial;
    [SerializeField] private Material grabbedMaterial;

    
    
    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = _meshRenderer.material;
    }

    private void Start()
    {
        Main.instance.eventManager.SubscribeToEvent(GameEvent.OnGrabIngridient, OnGrabItem);
        Main.instance.eventManager.SubscribeToEvent(GameEvent.OnReleaseIngridient, OnReleaseItem);
    }

    void OnGrabItem()
    {
        _meshRenderer.material = grabbedMaterial;
    }
    
    void OnReleaseItem()
    {
        _meshRenderer.material = originalMaterial;
    }
    
}
