using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeakerGauge : MonoBehaviour
{
    private float[] gaugeFill = { 0.6f, 0.75f, 0.845f, 1f };
    private int gaugeindex = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpeakerGaugeLoop());
    }

    private IEnumerator SpeakerGaugeLoop()
    {
        while (true)
        {

            this.GetComponent<Image>().fillAmount = gaugeFill[gaugeindex];
            gaugeindex++;
            if (gaugeindex > 3)
            {
                gaugeindex = 0;
            }
            yield return new WaitForSeconds(0.8f);
        }
    }

}
