using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public static class PenaltyGauge
{
    private static Image gauge = GameObject.Find("Image").GetComponent<Image>();


    private static IEnumerator GaugeDecrease()
    {
        for (; ; )
        {
            gauge.fillAmount -= 0.02f;
            if (gauge.fillAmount < 0.01f)
            {
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    public static void Start(MonoBehaviour Caller)
    {
        gauge.fillAmount = 1f;
        Caller.StartCoroutine(GaugeDecrease());
    }
}
