using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public static class PenaltyGauge
{
    private static Image gauge = GameObject.Find("Image").GetComponent<Image>();

    public static bool isPenalty = false;
    private static IEnumerator forcePenalty()
    {
        isPenalty = true;
        yield return new WaitForSeconds(5f);
        isPenalty = false;
    }

    private static IEnumerator GaugeDecrease()
    {
        isPenalty = true;
        for (; ; )
        {
            gauge.fillAmount -= 0.02f;
            if (gauge.fillAmount < 0.01f)
            {
                isPenalty = false;
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
