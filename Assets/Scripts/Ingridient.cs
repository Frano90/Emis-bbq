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

    [SerializeField] private IngridientProcessBar _processBar = new IngridientProcessBar();
    [SerializeField] private IngridientPlacer _ingredientPlacer;
    
    
    public event Action OnGrabbedItem;
    public event Action OnReleaseItem;

    public IngridientData CurrentIngridientData => _ingridientStates[_currentIngridientStateIndex];
    
    void Start()
    {
        _dragableController = GetComponent<DragableObject>();
        _ingredientPlacer = new IngridientPlacer(this, Camera.main);

        new GrabbedItemView(this);
        
        EventSubscription();
        
        _ingridientStates[_currentIngridientStateIndex].Enter(modelView);
    }

    void EventSubscription()
    {
        _dragableController.AddEventOnDragItem(_ingredientPlacer.TryToPlaceObject);
        _dragableController.AddEventOnReleaseItem(_ingredientPlacer.ReleaseItem);

        foreach (var state in _ingridientStates)
        {
            state.AddEventOnFinishProcessTime(Process);
            state.AddEventOnRefreshElapsedProcessTime(_processBar.RefreshUI);
        }
    }

    public void TryToPlaceObject()
    {
        _ingredientPlacer.TryToPlaceObject();
    }

    public void PlaceObject()
    {
        
        _ingredientPlacer.ReleaseItem();
    }

    private void Update()
    {
        if(_ingridientStates[_currentIngridientStateIndex] != null) _ingridientStates[_currentIngridientStateIndex].Update();
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
        _ingridientStates[_currentIngridientStateIndex].RemoveEventOnFinishProcessTime(Process); //esto no le gusta al memi
        _ingridientStates[_currentIngridientStateIndex].RemoveEventOnRefreshElapsedProcessTime(_processBar.RefreshUI); // esto tampoco
        _ingridientStates[_currentIngridientStateIndex].Exit(modelView);
        
        if (_currentIngridientStateIndex + 1 >= _ingridientStates.Length)
        {
            
            Delete();
            return;
        }

        _currentIngridientStateIndex++;
        
        _ingridientStates[_currentIngridientStateIndex].Enter(modelView);
    }
}
