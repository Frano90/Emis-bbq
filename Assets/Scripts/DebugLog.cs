using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugLog : MonoBehaviour
{
    [SerializeField] private TMP_Text log;

    private void Start()
    {
        Main.instance.eventManager.SubscribeToEvent(GameEvent.ClientDonePurchase, PurchaseResult);
        Main.instance.eventManager.SubscribeToEvent(GameEvent.BuyIngredient, BuyIngridient);
        log.text = "";
    }

    private void BuyIngridient(object[] parametercontainer)
    {
        log.text = "Gastaste " + parametercontainer[0];
        StartCoroutine(CleanLog());
    }

    private void PurchaseResult(object[] parametercontainer)
    {
        Client.OrderStatus result = (Client.OrderStatus)parametercontainer[0];

        if (result == Client.OrderStatus.Unacceptable)
        {
            log.text = "PEDIDO INCORRECTO";
        }
        else
        {
            log.text = "PEDIDO CORRECTO";
        }

        StartCoroutine(CleanLog());
    }

    IEnumerator CleanLog()
    {
        yield return new WaitForSeconds(1f);

        log.text = "";
    }
}
