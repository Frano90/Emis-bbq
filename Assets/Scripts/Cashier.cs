using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cashier : MonoBehaviour
{
    [SerializeField] private int earnMoney;
    [SerializeField] private int spentMoney;


    [SerializeField] private Button startGame_btt;
    [SerializeField] private GameObject startPanel;
    private void Start()
    {
        Main.instance.eventManager.SubscribeToEvent(GameEvent.ClientDonePurchase, ComputeScore);
        Main.instance.eventManager.SubscribeToEvent(GameEvent.BuyIngredient, SpentMoney);
        Main.instance.eventManager.SubscribeToEvent(GameEvent.FinishDay, FinishDay);
              
        startGame_btt.onClick.AddListener(StartNewDay);
    }

    void StartNewDay()
    {
        earnMoney = 0;
        spentMoney = 0;
        
        startPanel.SetActive(false);
        
        Main.instance.eventManager.TriggerEvent(GameEvent.StartNewDay);
    }

    void FinishDay()
    {
        startPanel.SetActive(true);
    }
    
    private void SpentMoney(object[] parametercontainer)
    {
        var ingredientCost = (int)parametercontainer[0];
        spentMoney += ingredientCost;
    }

    private void ComputeScore(object[] parametercontainer)
    {
        var value = (float)parametercontainer[0];
    }
}
