using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Displayname : MonoBehaviour
{
    private TextMeshProUGUI PNtext;
    // Start is called before the first frame update
    void Start()
    {
        PNtext = gameObject.GetComponent<TextMeshProUGUI>();
        PNtext.text = "Player:" + NameHolder.PlayerName;

    }
}
