using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterButton : MonoBehaviour
{
    public TMP_InputField nameField;
    void Start()
    {
        nameField = GameObject.Find("InputField")
                    .GetComponent<TMP_InputField>();
    }

    public void EnterOn()
    {
        Debug.Log("enter on by" + NameHolder.PlayerName);
        if (nameField.text.CompareTo("") == 0)
        {
            //
        }
        else
        {
            NameHolder.PlayerName = nameField.text;
            nameField.text = "";
        }
    }
}
