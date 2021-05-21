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
        bool result = (bool)parametercontainer[1];

        if (result)
        {
            log.text = "PEDIDO CORRECTO";
        }
        else
        {
            log.text = "PEDIDO INCORRECTO";
        }

        StartCoroutine(CleanLog());
    }

    IEnumerator CleanLog()
    {
        yield return new WaitForSeconds(1f);

        log.text = "";
    }
}
