using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public static class NameHolder
{
    private static String playerName = "Guest";
    private static TextMeshProUGUI textMesh;
    public static void DisplayName()
    {
        TextMeshProUGUI
        TextMesh = GameObject.Find("DisplayName")
                    ?.GetComponent<TextMeshProUGUI>();
        TextMesh.text = "Player:" + playerName;
    }
    public static String PlayerName
    {
        get => playerName;
        set
        {
            Debug.Log(value);
            playerName = value;
            DisplayName();
        }
    }
}
