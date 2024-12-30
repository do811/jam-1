using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class PrefAccessor
{
    public static void SetLocalPlayerRanking(string name, SortedSet<int> scores)
    {
        foreach (var score in scores)
        {
            RankingUpdate(name, score);
        }
    }
    static SortedSet<int> ParseSortedSetInt(string str, int MaxLength = -1)
    {
        string[] TextArr = str.Split(",");
        int topScores = TextArr.Length;
        SortedSet<int> Sets = new SortedSet<int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));//降順の集合
        for (int i = 0; i < topScores; i++)
        {
            int score = int.Parse(TextArr[i]);
            Sets.Add(score);
            if (Sets.Count > MaxLength && MaxLength != 0)
            {
                Sets.Remove(Sets.Last());//降順、つまりデフォルトと逆なのでMaxが最小値になる。・・・ややこしいですね

            }
        }
        return Sets;
    }
    /**
     *  @do811
     *  @return SortedSet<int>
     */
    public static SortedSet<int> CatchRanking(string rankingKey, int MaxRankingNum = 5)
    {
        string seriarizedRanking = PlayerPrefs.GetString(rankingKey, "0,0,0,0,0");
        return ParseSortedSetInt(seriarizedRanking, MaxRankingNum);
    }

    public static void PlayerRankingLoad(TextMeshProUGUI[] textmeshes)
    {
        SortedSet<int> ranking = CatchRanking("local", 5);
        int i = 1;
        foreach (var score in ranking)
        {
            Debug.Log($"{i}位:{score}");
            textmeshes[i - 1].text = $"{i}位:{score:D3}点";
            i++;
        }
    }
    public static SortedSet<int> PlayerRankingLoad()
    {
        SortedSet<int> ranking = CatchRanking("local", 5);
        int i = 1;
        foreach (var score in ranking)
        {
            Debug.Log($"{i}位:{score}");
            i++;
        }
        return ranking;
    }
    public static void RankingUpdate(string rankingKey, int score = 0, int RankingLength = 5)
    {
        SortedSet<int> sets = CatchRanking(rankingKey, RankingLength);
        sets.Add(score);
        if (sets.Count > RankingLength)
        {
            sets.Remove(sets.Last());
        }
        string uploadRankingText = string.Join(",", sets);
        PlayerPrefs.SetString(rankingKey, uploadRankingText);
    }
}
