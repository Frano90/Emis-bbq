using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickableReceiver : MonoBehaviour
{

    [SerializeField] protected Image processBar; 
    [SerializeField] protected GameObject uiPanel; 
    
    [SerializeField] protected ParticleSystem onHoverParticles_FB;
    
    [SerializeField] private Transform placeToPutObject;

    [SerializeField]protected IPickable currentKitchenItemHolding;

    protected float _count;

    public void Start()
    {
        //Esto es un arreglo cabeza de un bug choto que no pude resolver facil
        Main.instance.eventManager.SubscribeToEvent(GameEvent.OnReleasePickable, () => onHoverParticles_FB.Stop());
    }

    public Transform PlaceToPutObject => placeToPutObject;
    public void OnDragObjectHover() {if(!onHoverParticles_FB.isPlaying) onHoverParticles_FB.Play();}

    public void OnExitDragObjectHover(){ onHoverParticles_FB.Stop();}
    
    public virtual void OnReceiveIngredient(IPickable pickable){}

    public void RemovePickable()
    {
        _count = 0;
        currentKitchenItemHolding = null;
    }
}
