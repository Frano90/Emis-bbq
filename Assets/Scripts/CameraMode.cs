using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMode : MonoBehaviour
{
    [SerializeField] private Button camera_btt;
    [SerializeField] private Camera myCam;
    
    // Start is called before the first frame update
    void Start()
    {
        myCam = FindObjectOfType<Camera>();
        camera_btt.onClick.AddListener(SwitchCameraMode);
    }

    void SwitchCameraMode()
    {
        myCam.orthographic = !myCam.orthographic;
    }
    
}
