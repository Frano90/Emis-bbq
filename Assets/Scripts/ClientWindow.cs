using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientWindow : MonoBehaviour
{
    [SerializeField] private List<Transform> clientPosInWorld = new List<Transform>();
    
    private List<ClientPosition> clientOrderPositions = new List<ClientPosition>();

    [SerializeField] private ClientQueue _clientQueue = new ClientQueue();

    [SerializeField] private int clientesEnElDia;
    private void Start()
    {
        Main.instance.eventManager.SubscribeToEvent(GameEvent.ClientDonePurchase, NextClient);
        
        _clientQueue.Init(this, clientesEnElDia);

        RegisterWindowPositions();

        NextClient();
    }

    void NextClient()
    {
        var nextCustomer = _clientQueue.GetNextCostumer();

        if (nextCustomer == null)
        {
            Main.instance.eventManager.TriggerEvent(GameEvent.NoMoreCustomers);
            Debug.Log("no hay mas clientes");
            return;
        }

        for (int i = 0; i < clientOrderPositions.Count; i++)
        {
            if(clientOrderPositions[i].isOcupied) continue;

            nextCustomer.transform.position = clientOrderPositions[i].transform.position;
            nextCustomer.transform.rotation = clientOrderPositions[i].transform.rotation;
            clientOrderPositions[i].isOcupied = true;
            nextCustomer.MyWindow = i;
            nextCustomer.OnReceiveOrder += ClearWindow;
            return;
        }
        
        Debug.Log("No encontre lugar!");
    }

    void ClearWindow(bool orderSuccesfull, Client customer)
    {
        clientOrderPositions[customer.MyWindow].isOcupied = false;
        
        NextClient();
    }
    
    void RegisterWindowPositions()
    {
        foreach (var pos in clientPosInWorld)
        {
            ClientPosition newPos = new ClientPosition();
            newPos.transform = pos;
            newPos.isOcupied = false;
            
            clientOrderPositions.Add(newPos);
        }
    }
}

public class ClientPosition
{
    public Transform transform;
    public bool isOcupied;

}
