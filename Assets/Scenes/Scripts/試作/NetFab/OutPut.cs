using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class OutPut
{
    public static void Display(string TextObjName, string str)
    {
        TextMeshProUGUI textmesh = GameObject.Find(TextObjName).GetComponent<TextMeshProUGUI>();
        textmesh.text = str;
    }


}
