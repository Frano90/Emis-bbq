﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IngridientPlacer
{
    private Ingridient ingridient;
    [SerializeField] private ObjectReceiver _currentObjectReceiver;
    private Vector3 originalPos;

    private Camera _myCam;

    private ParabolicShooter parabolic;
    public IngridientPlacer(Ingridient ingridient, Camera myCam)
    {
        this.ingridient = ingridient;
        _myCam = myCam;
        originalPos = ingridient.transform.position;
        parabolic = new ParabolicShooter(ingridient.transform);
    }
    
    public void Check()
    {
        Ray ray = new Ray(ingridient.transform.position, Vector3.down);
        
        
        RaycastHit hit;

        Physics.Raycast(ray, out hit);

        var posiblePlace = hit.collider.GetComponent<ObjectReceiver>();

        if (posiblePlace != null)
        {
            _currentObjectReceiver = posiblePlace;
            posiblePlace.OnDragObjectHover();
        }
        else
        {
            if(_currentObjectReceiver != null) _currentObjectReceiver.OnExitDragObjectHover();
            _currentObjectReceiver = null;
        }
        
        Main.instance.eventManager.TriggerEvent(GameEvent.OnGrabIngridient);
    }

    public void Test()
    {
        Ray ray = _myCam.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            var posiblePlace = hit.collider.GetComponent<ObjectReceiver>();

            if (posiblePlace != null)
            {
                _currentObjectReceiver = posiblePlace;
                parabolic.DrawPath(originalPos, _currentObjectReceiver.PlaceToPutObject.position);
                posiblePlace.OnDragObjectHover();
            }
            else
            {
                if(_currentObjectReceiver != null) _currentObjectReceiver.OnExitDragObjectHover();
                
                parabolic.DrawPath(originalPos, Input.mousePosition);
                _currentObjectReceiver = null;
            }    
        }
        
        Main.instance.eventManager.TriggerEvent(GameEvent.OnGrabIngridient);
        
    }

    public void ReleaseItem()
    {
        if (_currentObjectReceiver != null)
        {
            ingridient.MoveTo(_currentObjectReceiver.PlaceToPutObject.position);
            _currentObjectReceiver.OnReceiveItem(ingridient);
            originalPos = _currentObjectReceiver.PlaceToPutObject.position;
        }
        else
            ingridient.transform.position = originalPos;
        
        
        Main.instance.eventManager.TriggerEvent(GameEvent.OnReleaseIngridient);
    }
}
