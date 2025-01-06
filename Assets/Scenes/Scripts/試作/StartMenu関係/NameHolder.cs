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
    public static void DisplayName(string name)
    {
        TextMeshProUGUI
        TextMesh = GameObject.Find("DisplayName")
                    ?.GetComponent<TextMeshProUGUI>();
        TextMesh.text = "Player:" + name;
    }
    public static String PlayerName
    {
        get
        {
            if (PlayerPrefs.GetString("name").CompareTo("") == 0)
            {
                return "Guest";
            }
            return PlayerPrefs.GetString("name");
        }
        set
        {
            PlayerPrefs.SetString("name", playerName = value);
            Debug.Log(playerName);
            DisplayName(playerName);
        }
    }
}
