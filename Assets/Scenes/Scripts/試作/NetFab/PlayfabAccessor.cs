using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using TMPro;
using System.Linq;

public abstract class PlayfabAccessor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetUserName(NameHolder.PlayerName);
        SubmitScore(ScoreHolder.Max);
        GetRanking();
    }

    void Update()
    {
    }


    // Update is called once per frame
    void GetRanking()
    {
        // PlayFabに送信するリクエストを作成する
        var request = new GetLeaderboardRequest
        {
            // 統計情報名の指定（LeaderBoards(Legace)で設定した名前）
            StatisticName = "TestRanking",
            // 何位以降のランキングを取得するか指定する
            // 1位から取得する場合は0を代入
            StartPosition = 0,
            // 何件分のランキングデータを取得するか指定する
            // 最大は100
            MaxResultsCount = 10
        };

        // PlayFabにリクエストを送信する
        PlayFabClientAPI.GetLeaderboard(request, OnGetRankingSuccess, OnGetRankingFailure);

        // 送信成功時の処理
    }

    protected abstract void UseRanking(PlayerLeaderboardEntry item);
        void OnGetRankingSuccess(GetLeaderboardResult leaderboardResult)
        {
            // ランキングを表示するコード
            foreach (var item in leaderboardResult.Leaderboard)
            {
                UseRanking(item);
            }
        }

        // 送信失敗時の処理
        void OnGetRankingFailure(PlayFabError error)
        {
            Debug.Log("ランキングの取得に失敗しました");
        }

    void GetRankingAroundPlayer()
    {
        // PlayFabに送信するリクエストを作成する
        var request = new GetLeaderboardAroundPlayerRequest
        {
            // 統計情報名の指定（LeaderBoards(Legace)で設定した名前）
            StatisticName = "TestRanking",
            // 自分と+-5位をあわせて合計11件を取得
            MaxResultsCount = 11
        };

        // PlayFabにリクエストを送信する
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetRankingAroundPlayerSuccess, OnGetRankingAroundPlayerFailure);

        // 送信成功時の処理
    }
        void OnGetRankingAroundPlayerSuccess(GetLeaderboardAroundPlayerResult leaderboardResult)
        {
            // ランキングを表示する仮コード
            foreach (var item in leaderboardResult.Leaderboard)
            {
                // Positionは順位。0から始まるので+1して表示する
                // intの最大値から取得したスコアを引いて、本来のスコアを出力する
                Debug.Log($"{item.Position + 1}位　プレイヤー名：{item.DisplayName}　スコア：{int.MaxValue - item.StatValue}");
            }
        }

        // 送信失敗時の処理
        void OnGetRankingAroundPlayerFailure(PlayFabError error)
        {
            Debug.Log("ランキングの取得に失敗しました");
        }

    void SetUserName(string name)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = name
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSetUserNameSuccess, OnSetUserNameFailure);

        // 送信成功時の処理
        void OnSetUserNameSuccess(UpdateUserTitleDisplayNameResult result)
        {
            Debug.Log("プレイヤー名の変更に成功しました");
        }

        // 送信失敗時の処理
        void OnSetUserNameFailure(PlayFabError error)
        {
            Debug.Log("プレイヤー名の変更に失敗しました");
        }
    }

    void SubmitScore(int score)
    {
        var statisticUpdate = new StatisticUpdate
        {
            StatisticName = "TestRanking",
            Value = score,
        };
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>{
                statisticUpdate
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSubmitScoreSuccess, OnSubmitScoreFailure);
        void OnSubmitScoreSuccess(UpdatePlayerStatisticsResult result)
        {
            Debug.Log("スコアの送信に成功しました");
        }

        // 送信失敗時の処理
        void OnSubmitScoreFailure(PlayFabError error)
        {
            Debug.Log("スコアの送信に失敗しました");
        }
    }
    public void Display(string TextObjName, string str)
    {
        TextMeshProUGUI textmesh = GameObject.Find(TextObjName).GetComponent<TextMeshProUGUI>();
        textmesh.text = str;
    }
}
