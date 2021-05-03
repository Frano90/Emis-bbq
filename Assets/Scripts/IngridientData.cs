using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingridient", menuName = "Ingridient/New")]
public class IngridientData : ScriptableObject
{
    public Sprite grabbedImage;
    public Material grabbedMaterial;
    public GameObject viewModel;
    public float processTime;
    private float _count;

    public event Action OnFinishProcessTime; 
    public event Action<float, float> OnRefreshElapsedProcessTime; 
    
    public void Exit(Transform modelView)
    {
        modelView.GetComponent<MeshFilter>().mesh = null;
        modelView.GetComponent<MeshRenderer>().material = null;
    }

    public void Enter(Transform modelView)
    {
        _count = 0;
        
        modelView.GetComponent<MeshFilter>().mesh = viewModel.GetComponent<MeshFilter>().sharedMesh;
        modelView.GetComponent<MeshRenderer>().material = viewModel.GetComponent<MeshRenderer>().sharedMaterial;
    }


}
