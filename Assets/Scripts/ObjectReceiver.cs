using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReceiver : MonoBehaviour
{

    [SerializeField] private ParticleSystem onHoverParticles_FB;
    
    [SerializeField] private Transform placeToPutObject;

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
    
    public void PlaceObjectOnReceiver(Transform obj)
    {
        Debug.Log("voy y lo pongo");
        obj.position = placeToPutObject.position;
    }
}
