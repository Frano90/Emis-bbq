using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingridient : MonoBehaviour
{
    private DragableObject _dragableController;

    [SerializeField] private Ingridient_RaycastPositioningChecker _raycastPositioningChecker;
    // Start is called before the first frame update
    void Start()
    {
        _dragableController = GetComponent<DragableObject>();
        _raycastPositioningChecker = new Ingridient_RaycastPositioningChecker(transform);
        
        EventSubscription();
    }

    void EventSubscription()
    {
        _dragableController.ADD_EVENT_OnDragItem(_raycastPositioningChecker.Check);
        _dragableController.ADD_EVENT_OnReleaseItem(_raycastPositioningChecker.ReleaseItem);
    }
    
}
