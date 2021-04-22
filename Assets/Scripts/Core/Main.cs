using System.Collections;
using System.Collections.Generic;
using FranoW.DevelopTools;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main instance;
    public EventManager eventManager;
    void Awake()
    {
        if (instance == null) instance = this;


        eventManager = new EventManager();


    }
}
