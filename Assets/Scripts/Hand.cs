using System;
using UnityEngine;


public class Hand : MonoBehaviour
{
    private Camera _myCam;

    private IPickable _currentPickable;

    private PickableReceiver _currentPickableReceiver;
    private void Awake()
    {
        _myCam = Camera.main;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            GrabPickable();
        }

        if (Input.GetKey(KeyCode.Mouse0) && _currentPickable != null)
        {
            TryToPlaceObject();
        }
        
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            ReleasePickable();
        }
    }

    private void GrabPickable()
    {
        Ray ray = _myCam.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            _currentPickable = GetPickableFromCollider(hit.collider);
            
            if(_currentPickable != null) _currentPickable.PickUp();
        }
    }

    private void ReleasePickable()//Esto no le va a gustar al memi
    {
        if( _currentPickable != null) _currentPickable.Release();
        
        if (_currentPickableReceiver != null)
        {
            if(_currentPickable.GetCurrentReceiver() != null) _currentPickable.GetCurrentReceiver().OnRemoveIngredient();
            _currentPickableReceiver.OnReceiveIngredient(_currentPickable);
        }

        _currentPickableReceiver = null;
        _currentPickable = null;
    }

    public void TryToPlaceObject()
    {
        Ray ray = _myCam.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            var posiblePlace = hit.collider.GetComponent<PickableReceiver>();

            if (posiblePlace != null)
            {
                _currentPickableReceiver = posiblePlace;
                ParabolicShooter.DrawPath(_currentPickable.GetPosition(), _currentPickableReceiver.PlaceToPutObject.position); posiblePlace.OnDragObjectHover();
            }
            else
            {
                if(_currentPickableReceiver != null) _currentPickableReceiver.OnExitDragObjectHover();
                
                _currentPickableReceiver = null;
            }    
        }
        
    }
    
    #region AuxMethods

    IPickable GetPickableFromCollider(Collider col)
    {
        foreach (var component in col.GetComponents<MonoBehaviour>())
        {
            if (component is IPickable)
            {
                return component as IPickable;
            }
        }
        return null;
    }

        #endregion
    
}
    
    
    
    

    
