using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingridient : MonoBehaviour
{
    private DragableObject _dragableController;

    [SerializeField] private IngridientData[] _ingridientStates;
    private int _currentIngridientStateIndex = 0;

    [SerializeField] private Transform modelView;

    [SerializeField] private IngridientPlacer _raycastPositioningChecker;
    public event Action OnGrabbedItem;
    public event Action OnReleaseItem;
    
    void Start()
    {
        _dragableController = GetComponent<DragableObject>();
        _raycastPositioningChecker = new IngridientPlacer(this, Camera.main);

        new GrabbedItemView(this);
        
        _ingridientStates[_currentIngridientStateIndex].Enter(modelView);
        
        EventSubscription();
    }

    void EventSubscription()
    {
        _dragableController.AddEventOnDragItem(_raycastPositioningChecker.TryToPlaceObject);
        _dragableController.AddEventOnReleaseItem(_raycastPositioningChecker.ReleaseItem);
    }

    public void MoveTo(Vector3 newPos)
    {
        transform.position = newPos;
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    public void GrabItem()
    {
        OnGrabbedItem?.Invoke();
    }
    
    public void ReleaseItem()
    {
        OnReleaseItem?.Invoke();
    }

    public void Process()
    {
        _ingridientStates[_currentIngridientStateIndex].Exit(modelView);
        if (_currentIngridientStateIndex + 1 > _ingridientStates.Length)
        {
            Delete();
            return;
        }

        _currentIngridientStateIndex++;
        
        _ingridientStates[_currentIngridientStateIndex].Enter(modelView);
    }
}
