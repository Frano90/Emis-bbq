﻿using System;
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
        Cursor.visible = false;
        currentItemGrabbed_Image.gameObject.SetActive(false);
    }

    private void GrabView()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            var item = hit.collider.GetComponent<IngredientDispatcher>();

            if (item != null)
            {
                //currentItemGrabbed_Image.sprite = item.firstStateIngridientData.grabbedImage;
                currentItemGrabbed_Image.gameObject.SetActive(true);
            }
        }
        
        currenthand_Image.sprite = grabHand_image;
        GrabbedItemView();
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
            GrabView();
        }
        
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ReleaseView();
        }
        
        currenthand_Image.transform.position = currentItemGrabbed_Image.transform.position = Input.mousePosition;
    }
    
    public void GrabbedItemView()
    {
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
