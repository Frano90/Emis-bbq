using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MouseView : MonoBehaviour
{
    [SerializeField] private Sprite releaseHand_image; 
    [SerializeField] private Sprite grabHand_image; 
    [SerializeField] private Image currenthand_Image;

    [SerializeField] private Image currentItemGrabbed_Image;

    private void Awake()
    {
        //Cursor.visible = false;
        currentItemGrabbed_Image.gameObject.SetActive(false);
    }

    private void Start()
    {
        Main.instance.eventManager.SubscribeToEvent(GameEvent.StartNewDay, LockCursor);
        Main.instance.eventManager.SubscribeToEvent(GameEvent.FinishDay, UnlockCursor);
    }

    void LockCursor()
    {
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.visible = true;
    }

    private void ReleaseView()
    {
        currentItemGrabbed_Image.gameObject.SetActive(false);
        currenthand_Image.sprite = releaseHand_image;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GrabbedItemView();
        }
        
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ReleaseView();
        }
        
        currenthand_Image.transform.position = currentItemGrabbed_Image.transform.position = Input.mousePosition;
    }
    
    public void GrabbedItemView()
    {
        currenthand_Image.sprite = grabHand_image;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            var item = GetPickableFromCollider(hit.collider);

            if (item != null)
            {
                currentItemGrabbed_Image.sprite = item.GetGrabImage();
                currentItemGrabbed_Image.gameObject.SetActive(true);
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
