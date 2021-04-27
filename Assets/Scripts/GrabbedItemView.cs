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



    public GrabbedItemView(Ingridient myIngridient)
    {
        //_meshRenderer = myIngridient.GetComponent<MeshRenderer>();
        //originalMaterial = _meshRenderer.material;

        //grabbedMaterial = Resources.Load<Material>("GrabbedItem");
        
        myIngridient.OnGrabbedItem += EnableFeedback;
        myIngridient.OnReleaseItem += DisableFeedback;
    }

    void EnableFeedback()
    {
        //_meshRenderer.material = grabbedMaterial;
    }
    
    void DisableFeedback()
    {
        //_meshRenderer.material = originalMaterial;
    }
    
}
