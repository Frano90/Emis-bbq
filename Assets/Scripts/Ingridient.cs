using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingridient : MonoBehaviour
{
    private DragableObject _dragableController;

    [SerializeField] private IngridientPlacer _raycastPositioningChecker;
    // Start is called before the first frame update
    void Start()
    {
        _dragableController = GetComponent<DragableObject>();
        _raycastPositioningChecker = new IngridientPlacer(this, Camera.main);
        
        EventSubscription();
    }

    void EventSubscription()
    {
        _dragableController.AddEventOnDragItem(_raycastPositioningChecker.Test);
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
}
