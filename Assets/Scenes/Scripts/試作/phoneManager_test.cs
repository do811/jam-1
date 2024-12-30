using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using GameObjectlib;
using System.Threading;
using System.Diagnostics;
using local;
using TMPro;
using System;
using UnityEditor;
using OnePlay;
public class phoneManager_test : MonoBehaviour
{
    private GameObject phoneObj;
    private GameObject manager;
    private int dice;
    private MeshRenderer mesh;
    private int index = 0;
    private MeshRenderer colors;
    private TextMesh TimeText;
    private local.StopWatch stopwath;
    private void BackInitialPositoin(GameObject phone)
    {
        Container target = new(phone);
        target &= (-4, 0, 10);
    }
    private void BackInitialRotate(GameObject phone)
    {
        Transform phoneTrs = phone.GetComponent<Transform>();
        phoneTrs.eulerAngles = new Vector3(0, 90, 0);
    }
    private void Calcurate()
    {
        score = -Math.Log((double)phonetimes[phonecalltime] + 0.101F, 1.02F) + 50;
        if (score > 0 && score <= 100)
        {
            Intscore[phonecalltime] = (int)Math.Round(score);
        }
        else if (score < 0)
        {
            Intscore[phonecalltime] = 0;
            noresult = true;
        }
        else//score > 100のとき
        {
            Intscore[phonecalltime] = 100;
        }
    }
    private void Total()
    {
        foreach (var one in Intscore)
        {
            totalscore += one;
        }
    }
    private static int timeCountSize = 5;
    int maxcycle = 2;//最大で着信が来ない回数
    int currentcycle = 0; //電話が来た回数？
    int watingsum = 0;
    int phonecalltime = 0;
    double score = 0;
    int totalscore = 0;
    bool noresult = false;
    int[] Intscore = new int[timeCountSize];
    private double[] phonetimes = new double[timeCountSize];
    bool isCallAble = true;
    //時間制御なんでIEnumeratorによるコルーチン
    IEnumerator waitCall()
    {
        yield return new WaitForSeconds(2f);
        watingsum += 2;
        for (; ; )
        {
            dice = UnityEngine.Random.Range(1, 6);
            if ((dice <= 2 || watingsum >= 8) && isCallAble)
            {
                mesh.material = colors.materials[index = 0];//ここで赤色にする
                isCallAble = false;
                watingsum = 0;
                SoundPlayer.PlaySound();
                yield return new WaitForSeconds(3f);

                SoundPlayer.StopSound();
                StopWatch.StopAndGetTime();
                BackInitialPositoin(phoneObj);
                BackInitialRotate(phoneObj);
                var time = StopWatch.StopAndGetTime();

                double totalTime = (time.Seconds) + (time.Milliseconds / 1000F);//ストップウォッチの値取得
                phonetimes[phonecalltime] = totalTime;
                Calcurate();
                UnityEngine.Debug.Log(totalTime);
                UnityEngine.Debug.Log(Intscore[phonecalltime]);
                ScoreHolder<List<int>>.Add(Intscore[phonecalltime]);
                if (noresult)
                {
                    OutPut.Display("Time" + (phonecalltime + 1)
                , (phonecalltime + 1).ToString() + "回目:記録なし");
                    noresult = false;
                }
                else
                {
                    OutPut.Display("Time" + (phonecalltime + 1)
                    , $"Time{phonecalltime + 1}: {phonetimes[phonecalltime]:F3}秒  {Intscore[phonecalltime]}点");
                }
                currentcycle = 0; //これで無限ループ？
                phonecalltime += 1;
                if (phonecalltime == timeCountSize)
                {
                    UnityEngine.Debug.Log("Finish!");
                    Total();
                    ScoreHolder<List<int>>.one = totalscore;
                    UnityEngine.Debug.Log(totalscore);
                    ResultHandler.DisplayResult();
                    break;
                }
            }
            else
            {
                //TODO:失敗したとき処理
                isCallAble = true;
                mesh.material = colors.materials[index = 1];
                watingsum += dice;
                yield return new WaitForSeconds(dice);
            }
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        phoneObj = this.gameObject;
        manager = GameObject.Find("manager");
        mesh = GameObject.Find("display").GetComponent<MeshRenderer>();
        colors = manager.GetComponent<MeshRenderer>();
        mesh.material = colors.materials[1];

        StartCoroutine(waitCall());
    }
}
