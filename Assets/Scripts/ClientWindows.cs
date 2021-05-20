using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientWindows : MonoBehaviour
{
    private ClientQueue _clientQueue;

    private void Start()
    {
        Main.instance.eventManager.SubscribeToEvent(GameEvent.StartNewDay, NextClient);

        _clientQueue = FindObjectOfType<ClientQueue>();
    }

    void NextClient()
    {
        var nextCustomer = _clientQueue.GetNextCostumer();

        if (nextCustomer == null)
        {
            Main.instance.eventManager.TriggerEvent(GameEvent.FinishDay);
            Debug.Log("no hay mas clientes");
            return;
        }

        nextCustomer.OnReceiveOrder += ClearWindow;
        nextCustomer.GoToWindow(transform);
    }

    void ClearWindow(bool orderSuccesfull, Client customer)
    {
        NextClient();
    }
}

