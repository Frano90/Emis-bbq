using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ingridient_RaycastPositioningChecker
{
    private Transform _t;
    private bool _isObjectHovering;
    [SerializeField] private ObjectReceiver _currentObjectReceiver;
    private Vector3 originalPos;
    
    public bool IsObjectHovering => _isObjectHovering;
    public ObjectReceiver CurrentObjectReceiver => _currentObjectReceiver;
    

    public Ingridient_RaycastPositioningChecker(Transform t)
    {
        _t = t;
        originalPos = t.transform.position;
    }
    
    public void Check()
    {
        Ray ray = new Ray(_t.position, Vector3.down);

        RaycastHit hit;

        Physics.Raycast(ray, out hit);

        var obj = hit.collider.GetComponent<ObjectReceiver>();

        if (obj != null)
        {
            _isObjectHovering = true;
            _currentObjectReceiver = obj;
            obj.OnDragObjectHover();
        }
        else
        {
            if(_currentObjectReceiver != null) _currentObjectReceiver.OnExitDragObjectHover();
            _currentObjectReceiver = null;
            _isObjectHovering = false;
        }
    }

    public void ReleaseItem()
    {
        if (_currentObjectReceiver != null)
        {
            _currentObjectReceiver.PlaceObjectOnReceiver(_t);
            originalPos = _currentObjectReceiver.PlaceToPutObject.position;
        }
        else
            _t.position = originalPos;
    }
}
