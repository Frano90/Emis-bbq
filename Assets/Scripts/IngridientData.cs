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

    public void Update()
    {
        _count += Time.deltaTime;

        if (_count >= processTime)
        {
            _count = 0;
            OnFinishProcessTime?.Invoke();
        }
        
        OnRefreshElapsedProcessTime?.Invoke(_count, processTime);
    }
    
    public void AddEventOnFinishProcessTime(Action callback) => OnFinishProcessTime += callback;
    public void RemoveEventOnFinishProcessTime(Action callback) => OnFinishProcessTime -= callback;
    
    public void AddEventOnRefreshElapsedProcessTime(Action<float, float> callback) => OnRefreshElapsedProcessTime += callback;
    public void RemoveEventOnRefreshElapsedProcessTime(Action<float, float> callback) => OnRefreshElapsedProcessTime -= callback;
}
