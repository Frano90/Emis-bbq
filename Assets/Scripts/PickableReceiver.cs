using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickableReceiver : MonoBehaviour
{

    [SerializeField] private Image processBar; 
    [SerializeField] private GameObject processBarCanvas; 
    
    [SerializeField] private ParticleSystem onHoverParticles_FB;
    
    [SerializeField] private Transform placeToPutObject;

    private Ingridient _currentIngredient;

    private float _count;

    public Transform PlaceToPutObject => placeToPutObject;
    public void OnDragObjectHover()
    {
        Debug.Log("lo tengo arriba");
        if(!onHoverParticles_FB.isPlaying) onHoverParticles_FB.Play();
        
    }

    public void OnExitDragObjectHover()
    {
        if(onHoverParticles_FB.isPlaying) onHoverParticles_FB.Stop();
    }
    
    public virtual void OnReceiveIngredient(IPickable pickable)
    {
        if (pickable is Ingridient)
        {
            _currentIngredient = pickable as Ingridient;
            _currentIngredient.MoveTo(this);
        
            if(onHoverParticles_FB.isPlaying) onHoverParticles_FB.Stop();
        }
        
    }

    private void Update()
    {
        if(_currentIngredient != null) ProcessIngredient();
        
        processBarCanvas.SetActive(_currentIngredient != null);
    }

    void ProcessIngredient()
    {
        var data = _currentIngredient.CurrentIngridientData;
        
        
        _count += Time.deltaTime;

        if (_count >= data.processTime)
        {
            _currentIngredient.Process();
            _count = 0;
        }

        processBar.fillAmount = _count / data.processTime;
    }
    
    public void RemoveIngredient()
    {
        _count = 0;
        _currentIngredient = null;
    }
    
    
    // public void AddEventOnFinishProcessTime(Action callback) => OnFinishProcessTime += callback;
    // public void RemoveEventOnFinishProcessTime(Action callback) => OnFinishProcessTime -= callback;
    //
    // public void AddEventOnRefreshElapsedProcessTime(Action<float, float> callback) => OnRefreshElapsedProcessTime += callback;
    // public void RemoveEventOnRefreshElapsedProcessTime(Action<float, float> callback) => OnRefreshElapsedProcessTime -= callback;
}
