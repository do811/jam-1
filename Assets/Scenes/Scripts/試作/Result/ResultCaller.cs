using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResultHandler
{
    static GameObject Image = GameObject.Find("Background");
    static GameObject texts = GameObject.Find("Results");
    public static void fadeResult()
    {
        Image.SetActive(false);
    }
    public static void DisplayResult()
    {
        Image.SetActive(true);
    }
}
