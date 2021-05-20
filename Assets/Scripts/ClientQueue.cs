using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClientQueue : MonoBehaviour
{
    [SerializeField] private int clientInLevel;

    [SerializeField] private Queue<Client> clientesDelDia = new Queue<Client>();


    private void Start()
    {
        Main.instance.eventManager.SubscribeToEvent(GameEvent.StartNewDay, CreateClientPool);
    }

    void CreateClientPool()
    {
        for (int i = 0; i < clientInLevel; i++)
        {
            var prefab = Resources.Load<Client>("Client");
            Client newClient = GameObject.Instantiate<Client>(prefab, transform);
            newClient.gameObject.SetActive(false);

            clientesDelDia.Enqueue(newClient);
        }
    }
    public Client GetNextCostumer()
    {
        if (clientesDelDia.Count <= 0)
        {
            return null;
        }
        
        Client nextClient = clientesDelDia.Dequeue();

        nextClient.gameObject.SetActive(true);
        nextClient.Init();

        return nextClient;
    }
}


