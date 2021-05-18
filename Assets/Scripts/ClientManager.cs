using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [SerializeField] private int clientInLevel;

    [SerializeField] private Queue<Client> clientesDelDia = new Queue<Client>();

    [SerializeField] private List<Transform> clientPosInWorld = new List<Transform>();
    private List<ClientPosition> clientOrderPositions = new List<ClientPosition>();

    public int score;
    void Start()
    {
        for (int i = 0; i < clientInLevel; i++)
        {
            var prefab = Resources.Load<Client>("Client");
            Client newClient = Instantiate<Client>(prefab, transform);
            newClient.gameObject.SetActive(false);
            newClient.OnReceiveOrder += RecieveOrder;
            
            
            clientesDelDia.Enqueue(newClient);
        }

        foreach (var pos in clientPosInWorld)
        {
            ClientPosition newPos = new ClientPosition();
            newPos.pos = pos;
            newPos.isOcupied = false;
            
            clientOrderPositions.Add(newPos);
        }
        
        NextCustomer();
        
    }

    void NextCustomer()
    {
        //esta version hace que el cliente recien atendido sea quien le dice al siguiente que tiene que avanzar
        Client currentClient = clientesDelDia.Dequeue();

        foreach (var pos in clientOrderPositions)
        {
            if(pos.isOcupied) continue;

            currentClient.transform.position = pos.pos.position;
        }
        
        currentClient.gameObject.SetActive(true);
        currentClient.Init();
    }
    
    void RecieveOrder(bool isSuccesful, Client customer)
    {
        
        if (isSuccesful)
        {
            score++;
        }
        
        
        if (clientesDelDia.Count <= 0)
        {
            Debug.Log("terminaron los clientes");
        }
        else
        {
            NextCustomer();
        }

        
        Destroy(customer.gameObject);
    }


}

public class ClientPosition
{
    public Transform pos;
    public bool isOcupied;

}
