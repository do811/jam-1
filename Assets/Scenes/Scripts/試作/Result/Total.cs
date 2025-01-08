using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameObjectlib;
using PlayFab.ClientModels;
using TMPro;
using OnePlay;
using System.Linq;
using UnityEngine.SceneManagement;
public class Total : MonoBehaviour
{
    private static Container texts;
    // Start is called before the first frame update
    void Start()
    {
        texts = new(this.gameObject);
        texts &= (-200f, 200f, 0f);

    }


    double time = 0;
    double span = 0.5f;
    double difference_x = 0;
    const double max_difference_x = 800f;
    int TotalScore;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time < span)
        {
            difference_x = max_difference_x * (time / span);
            texts &= (-200f + (float)difference_x, 200f, 0f);
        }
        else
        {
            time = 0;
            TotalScore = ScoreHolder<List<int>>.one;
            OutPut.Display("Total", "トータルスコア：" + TotalScore.ToString());
            ComparerRanking();
            StartCoroutine(BackToMenu());
            enabled = false;
        }
    }

    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("main_1");
        SceneManager.LoadScene("StartMenu");
    }

    void ComparerRanking()
    {
        var scores = PrefAccessor.CatchRanking("local", 5);
        PrefAccessor.RankingUpdate("local", TotalScore);
        if (TotalScore > scores.First())
        {
            //自己べ更新テキストとかあれば
        }

    }
}
