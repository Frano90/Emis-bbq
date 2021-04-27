using System;
using UnityEngine;
public class DragableObject : MonoBehaviour

{
    private Vector3 mOffset;
    private float mZCoord;
    
    public event Action OnDragItem;
    public event Action OnReleaseItem;
    
    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        //mYCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).y;
        
        // Store offset = gameobject world pos - mouse world pos

        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
    }

    private void OnMouseUp()
    {
        OnReleaseItem?.Invoke();
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)

        Vector3 mousePoint = Input.mousePosition;

        
        
        // z coordinate of game object on screen

        mousePoint.z = mZCoord;
        
        // Convert it to world points

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        //transform.position = GetMouseAsWorldPoint() + mOffset;
        OnDragItem?.Invoke();
    }


    public void AddEventOnDragItem(Action callback) => OnDragItem += callback;
    public void AddEventOnReleaseItem(Action callback) => OnReleaseItem += callback;
    public void RemoveEventOnDragItem(Action callback) => OnDragItem -= callback;
    public void REMOVE_EVENT_OnReleaseItem(Action callback) => OnReleaseItem -= callback;

}