using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerRanking : MonoBehaviour
{

    // Start is called before the first frame update 
    void Start()
    {
        TextMeshProUGUI[] textmeshes = {
            GameObject.Find("RankingText1").GetComponent<TextMeshProUGUI>(),
            GameObject.Find("RankingText2").GetComponent<TextMeshProUGUI>(),
            GameObject.Find("RankingText3").GetComponent<TextMeshProUGUI>(),
            GameObject.Find("RankingText4").GetComponent<TextMeshProUGUI>(),
            GameObject.Find("RankingText5").GetComponent<TextMeshProUGUI>()
        };
        PrefAccessor.PlayerRankingLoad();
        PrefAccessor.RankingUpdate("local", 50);
        Debug.Log("updateRanking");
        PrefAccessor.PlayerRankingLoad(textmeshes);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
