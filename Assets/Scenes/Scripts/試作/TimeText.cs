using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameObjectlib;
public class TimeText : MonoBehaviour
{
    private static Container texts;
    // Start is called before the first frame update
    void Start()
    {
        texts = new(this.gameObject);
        texts &= (1200f, 300f, 0f);
    }


    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(1.5f);
        texts &= (1200f, 300f, 0f);
        time = 0f;
        texts.Obj.SetActive(false);
    }
    double time = 0;
    double span = 0.3f;
    double difference_x = 0;
    const double max_difference_x = -700f;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time < span)
        {
            difference_x = max_difference_x * (time / span);
            texts &= (1200f + (float)difference_x, 300f, 0f);
        }
        else
        {
            StartCoroutine(Disappear());
        }
    }
}
