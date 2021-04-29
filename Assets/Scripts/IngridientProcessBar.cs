using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class IngridientProcessBar
{

    [SerializeField] private Image bar; 
    
    public void RefreshUI(float current, float max)
    {
        bar.fillAmount = current / max;
    }
    
}
