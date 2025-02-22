using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameObjectlib;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using System;
using UnityEngine.Android;
using Unity.VisualScripting;
using TMPro;


public class Player : MonoBehaviour
{
    private GameObject phoneObj;
    private GameObject phoneDisplay;
    private MeshRenderer phoneStat;
    private MeshRenderer status;
    private GameObject maincamera;
    private int index = 0;
    [SerializeField]
    private TextMeshProUGUI timetext;
    private TextMeshProUGUI penaltytext;


    private void BackInitialPositoin(GameObject phone)
    {
        Container target = new(phone);
        target &= (-4, 0, 10);//電話の位置、だいたいこの辺
    }
    private void MoveTakenPosition(GameObject phone)
    {
        Container target = new(phone);
        target &= (3f, 3.5f, 9.5f);//持ったときの位置、今の視点だとこの辺
    }

    private void RotateTakenAngle(GameObject phone)
    {
        Transform phoneTrs = phone.GetComponent<Transform>();
        phoneTrs.eulerAngles = new Vector3(50, 220, -40);
    }
    private void TakePhone(GameObject phone)
    {
        MoveTakenPosition(phone);
        RotateTakenAngle(phone);
        phoneStat.material = status.materials[index = 1];
    }

    // Start is called before the first frame update
    void Start()
    {
        phoneObj = GameObject.Find("phone");
        phoneDisplay = GameObject.Find("display");
        phoneStat = phoneDisplay.GetComponent<MeshRenderer>();
        status = gameObject.GetComponent<MeshRenderer>();//managerオブジェクトのRendererに格納されてるマテリアル使用のため。
        maincamera = GameObject.Find("Sight");
        timetext = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();
        penaltytext = GameObject.Find("PenaltyText").GetComponent<TextMeshProUGUI>();
        timetext.gameObject.SetActive(false);
        penaltytext.gameObject.SetActive(false);
        ResultHandler.fadeResult();
    }
    // Update is called once per frame
    void Update()
    {
        if (phoneStat.material.color == status.materials[0].color)
        {
            StopWatch.Start();
            if (Input.GetMouseButtonDown(0) && !PenaltyGauge.isPenalty)
            {
                SoundPlayer.StopSound();
                var time = StopWatch.StopAndGetTime();
                timetext.gameObject.SetActive(true);
                timetext.text = $"{time.Seconds}.{time.Milliseconds:D3}秒";
                TakePhone(phoneObj);
                Debug.Log("Took!");
            }
        }
        else if (Input.GetMouseButtonDown(0) && !PenaltyGauge.isPenalty)
        {
            Debug.Log("pushed out of call");
            penaltytext.gameObject.SetActive(true);
            PenaltyGauge.Start(this, penaltytext);
        }
    }
}
