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
        if (nameField.text.CompareTo("") == 0)
        {
            //pass
        }
        else
        {
            NameHolder.PlayerName = nameField.text;
            nameField.text = "";
        }
    }
}
