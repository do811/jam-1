using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResultCaller
{
    static GameObject Image = GameObject.Find("Background");

    public static void fadeResult()
    {
        Image.SetActive(false);
    }
    public static void DisplayResult()
    {
        Image.SetActive(true);
    }
}
