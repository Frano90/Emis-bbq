using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cashier : MonoBehaviour
{
    [SerializeField] private int earnMoney;
    [SerializeField] private int spentMoney;

    public int currentMoney; 
    [SerializeField] private int startingMoney;

    [SerializeField] private List<Money> costosAlInicioDelDia = new List<Money>();

    [SerializeField] private TMP_Text currentMoney_text;
    [SerializeField] private TMP_Text spentDay_text;
    
    [SerializeField] private Button startGame_btt;
    [SerializeField] private GameObject startPanel;

    [SerializeField] private float regularOrderScaler;
    [SerializeField] private float goodOrderScaler;

    [SerializeField] private ClientWindows[] windows;
    
    public int CustomerAmount { get; private set; }
    private void Start()
    {
        Main.instance.eventManager.SubscribeToEvent(GameEvent.ClientDonePurchase, ComputeScore);
        Main.instance.eventManager.SubscribeToEvent(GameEvent.BuyIngredient, SpentMoney);
        Main.instance.eventManager.SubscribeToEvent(GameEvent.FinishDay, FinishDay);
        Main.instance.eventManager.SubscribeToEvent(GameEvent.NoMoreCustomers, CheckCustomersInWindows);

        windows = FindObjectsOfType<ClientWindows>();
        
        startGame_btt.onClick.AddListener(StartNewDay);

        currentMoney = startingMoney;
        
        OpenStartDayPanel();
    }

    private void CheckCustomersInWindows()
    {
        for (int i = 0; i < windows.Length; i++)
            if (windows[i].IsOpen) return;
        
        Main.instance.eventManager.TriggerEvent(GameEvent.FinishDay);
    }

    void OpenStartDayPanel()
    {
        startPanel.SetActive(true);
        currentMoney_text.text = "Tenes ahorrado:  " + currentMoney;
        spentDay_text.text = "Te cuesta abrir:  " + StartDayCost();
    }

    int StartDayCost()
    {
        int total = 0;
        for (int i = 0; i < costosAlInicioDelDia.Count; i++)
        {
            total += costosAlInicioDelDia[i].value;
        }

        return total;
    }

    void GastoManutenciónPorDia()
    {
        int startDayCost = StartDayCost();        
        
        currentMoney -= startDayCost;

        if (currentMoney < 0)
        {
            Debug.Log("Perdiste");
            SceneManager.LoadScene(0);
        }
    }
    
    void StartNewDay()
    {
        GastoManutenciónPorDia();
        
        startPanel.SetActive(false);
        
        Main.instance.eventManager.TriggerEvent(GameEvent.StartNewDay);
    }

    void FinishDay()
    {
        int balanceMoney = earnMoney - spentMoney;
        
        currentMoney += balanceMoney;

        earnMoney = 0;
        spentMoney = 0;
        
        OpenStartDayPanel();
    }
    
    private void SpentMoney(object[] parametercontainer)
    {
        var ingredientCost = (int)parametercontainer[0];
        spentMoney += ingredientCost;
    }

    private void ComputeScore(object[] parametercontainer)
    {
        Client.OrderStatus status = (Client.OrderStatus)parametercontainer[0];
        Recipe recipeDone = (Recipe)parametercontainer[1];

        int totalCost = recipeDone.GetTotalCost();
        spentMoney += totalCost;

        if (status == Client.OrderStatus.Bad)
        {
            //Ya perdio plata por comprar las cosas. Por ahora lo dejo vacio
            return;
        }else if (status == Client.OrderStatus.Regular)
        {
            earnMoney += CalculateMoneyEarned(totalCost, regularOrderScaler);    
        }
        else
        {
            earnMoney += CalculateMoneyEarned(totalCost, goodOrderScaler);
        }

    }

    int CalculateMoneyEarned(int recipeCost, float scaler)
    {
        return Mathf.RoundToInt(recipeCost * scaler);
    }
}


