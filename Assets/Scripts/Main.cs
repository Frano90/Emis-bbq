using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private ObjectGrabber _objectGrabber;
    
    
    void Start()
    {
        _objectGrabber = new ObjectGrabber();
        
    }

    // Update is called once per frame
    void Update()
    {
        _objectGrabber.OnUpdate();
    }
}
