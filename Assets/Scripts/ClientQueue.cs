using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClientQueue
{
    [SerializeField] private int clientInLevel;

    [SerializeField] private Queue<Client> clientesDelDia = new Queue<Client>();


    private ClientWindow windows;


     public void Init(ClientWindow window, int clientInLevel)
    {
        windows = window;
        this.clientInLevel = clientInLevel;
        CreateClientPool();
    }

    void CreateClientPool()
    {
        for (int i = 0; i < clientInLevel; i++)
        {
            var prefab = Resources.Load<Client>("Client");
            Client newClient = GameObject.Instantiate<Client>(prefab, windows.transform);
            newClient.gameObject.SetActive(false);
            //newClient.OnReceiveOrder += RecieveOrder;
            
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


