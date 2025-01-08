using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backgroud : MonoBehaviour
{
    Image material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Image>();
        material.color = new Color(0.0f, 0.0f, 0.0f, 0f);
    }

    private float delta = 0f;
    private float span = 0.8f;
    private Color color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    private float maxClearness = 1f;
    void Update()
    {
        delta += Time.deltaTime;
        if (delta < span)
        {
            color.a = maxClearness * (delta / span);
            material.color = color;
        }
        else
        {
            ShowScores();
        }
    }

    void ShowScores()
    {
        GameObject results = GameObject.Find("Results");
        results.SetActive(true);
        
    }
}
