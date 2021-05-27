using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientWindows : MonoBehaviour
{
    private ClientQueue _clientQueue;

    [SerializeField] private float secsWaitToStart;

    public bool IsOpen { get; private set; }
    
    private void Start()
    {
        Main.instance.eventManager.SubscribeToEvent(GameEvent.StartNewDay, OpenForBussines);

        _clientQueue = FindObjectOfType<ClientQueue>();
    }

    void OpenForBussines()
    {
        StartCoroutine(WaitToStart());
    }

    public void CloseToBussines()
    {
        IsOpen = false;
    }

    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(secsWaitToStart);

        IsOpen = true;
        NextClient();
    }
    
    void NextClient()
    {
        var nextCustomer = _clientQueue.GetNextCostumer();

        if (nextCustomer == null)
        {
            CloseToBussines();
            Main.instance.eventManager.TriggerEvent(GameEvent.NoMoreCustomers);
            Debug.Log("no hay mas clientes");
            return;
        }

        nextCustomer.OnFinishRecieveOrderFeedback += ClearWindow;
        nextCustomer.GoToWindow(transform);
    }

    void ClearWindow()
    {
        NextClient();
    }
}

