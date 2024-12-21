using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        SortedSet<int> Sets = new SortedSet<int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
        for (int i = 0; i < topScores; i++)
        {
            int score = int.Parse(TextArr[i]);
            Sets.Add(score);
            if (Sets.Count > MaxLength && MaxLength != 0)
            {
                Sets.Remove(Sets.Min);
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
        return ParseSortedSetInt(seriarizedRanking, 5);
    }

    public static void GlobalRankingLoad()
    {
        SortedSet<int> ranking = CatchRanking("Ranking", 5);
        int i = 1;
        foreach (var score in ranking)
        {
            Debug.Log($"{i}位:{score}");
            i++;
        }
    }

    public static void RankingUpdate(string rankingKey, int score = 0, int RankingLength = 5)
    {
        SortedSet<int> sets = CatchRanking(rankingKey, RankingLength);
        sets.Add(score);
        if (sets.Count > RankingLength)
        {
            sets.Remove(sets.Min);
        }
        string uploadRankingText = string.Join(",", sets);
        PlayerPrefs.SetString(rankingKey, uploadRankingText);
    }
}
