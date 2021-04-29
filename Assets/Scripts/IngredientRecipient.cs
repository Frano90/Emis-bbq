using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientRecipient : MonoBehaviour
{
    //Repito cosas que hace el ingredient placer. Hay una manera de unificar?
    
    
    [SerializeField] private Ingridient ingridient_prefab;

    public IngridientData firstStateIngridientData;
    private ObjectReceiver _currentObjectReceiver;
    private ParabolicShooter parabolic;
    private DragableObject _dragableObject;

    private Camera _myCam;
    
    private void Awake()
    {
        _dragableObject = GetComponent<DragableObject>();
        parabolic = new ParabolicShooter(transform);
        _myCam = Camera.main;
    }

    private void Start()
    {
        _dragableObject.AddEventOnDragItem(OnDragItem);
        _dragableObject.AddEventOnReleaseItem(ReleaseItem);
    }

    void OnDragItem()
    {
        TryToPlaceObject();
    }
    
    public void TryToPlaceObject()
    {
        Ray ray = _myCam.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            var posiblePlace = hit.collider.GetComponent<ObjectReceiver>();
            
            if (posiblePlace != null)
            {
                _currentObjectReceiver = posiblePlace;
                parabolic.DrawPath(transform.position, _currentObjectReceiver.PlaceToPutObject.position);
                posiblePlace.OnDragObjectHover();
            }
            else
            {
                if(_currentObjectReceiver != null) _currentObjectReceiver.OnExitDragObjectHover();
                
                parabolic.DrawPath(transform.position, Input.mousePosition);
                _currentObjectReceiver = null;
            }    
        }
    }
    
    public void ReleaseItem()
    {
        if (_currentObjectReceiver != null)
        {
            Ingridient ingridient = Instantiate<Ingridient>(ingridient_prefab, _currentObjectReceiver.PlaceToPutObject.position,Quaternion.identity);
            
            ingridient.MoveTo(_currentObjectReceiver.PlaceToPutObject.position);
            _currentObjectReceiver.OnReceiveItem(ingridient);
        }
        
    }
    
    
    
}
