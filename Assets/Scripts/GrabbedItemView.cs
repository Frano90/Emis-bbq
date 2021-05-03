using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GrabbedItemView
{
    private MeshRenderer _meshRenderer;
    private Material originalMaterial;
    [SerializeField] private Material grabbedMaterial;



    public GrabbedItemView(Transform myModelView)
    { 
        _meshRenderer = myModelView.GetComponent<MeshRenderer>();
        originalMaterial = _meshRenderer.material;

        grabbedMaterial = Resources.Load<Material>("GrabbedItem");
    }

    public void EnablePickUpFeedback()
    {
        _meshRenderer.material = grabbedMaterial;
    }
    
    public void DisablePickUpFeedback()
    {
        _meshRenderer.material = originalMaterial;
    }
    
}
