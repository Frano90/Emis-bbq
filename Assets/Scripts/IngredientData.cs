using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingridient", menuName = "Ingridient/New")]
public class IngredientData : ScriptableObject
{
    public Sprite grabbedImage;
    public Material grabbedMaterial;
    public GameObject viewModel;
    public float processTime;
    public int cost;
    private float _count;
    
    
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
