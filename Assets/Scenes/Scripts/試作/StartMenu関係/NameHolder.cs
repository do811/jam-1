using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public static class NameHolder
{
    private static String playerName = "Guest";
    private static
    TextMeshProUGUI
    TextMesh = GameObject.Find("DisplayName")
                ?.GetComponent<TextMeshProUGUI>();
    public static void DisplayName()
    {
        TextMesh.text = "Player:" + playerName;
    }
    public static String PlayerName
    {
        get => playerName;
        set
        {
            playerName = value;
            DisplayName();
        }
    }
}
