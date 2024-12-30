using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

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

    private static IEnumerator GaugeDecrease(TextMeshProUGUI text)
    {
        isPenalty = true;
        for (; ; )
        {
            gauge.fillAmount -= 0.02f;
            if (gauge.fillAmount < 0.01f)
            {
                isPenalty = false;
                text.gameObject.SetActive(false);
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    public static void Start(MonoBehaviour Caller, TextMeshProUGUI text)
    {
        gauge.fillAmount = 1f;
        Caller.StartCoroutine(GaugeDecrease(text));
    }
}
