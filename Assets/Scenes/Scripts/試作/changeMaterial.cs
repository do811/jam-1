using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using GameObjectlib;
using System.Threading;
using System.Diagnostics;
public class changeMaterial : MonoBehaviour
{
    private GameObject phoneObj;
    private GameObject manager;
    private int watingtime;
    private MeshRenderer mesh;
    private int index = 0;
    private MeshRenderer colors;
    private TextMesh TimeText;

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

    int maxcycle = 2;//最大で着信が来ない回数
    int currentcycle = 0;
    int watingsum = 0;
    //時間制御なんでIEnumeratorによるコルーチン
    IEnumerator waitCall()
    {
        for (; ; currentcycle++)
        {
            watingtime = Random.Range(1, 6);
            if (watingtime <= 2 && watingsum >= 8)
            {
                mesh.material = colors.materials[index = 0];//ここで赤色にする
                yield return new WaitForSeconds(watingtime);
                StopWatch.StopAndGetTime();
                BackInitialPositoin(phoneObj);
                BackInitialRotate(phoneObj);
                currentcycle = 0;
            }
            else
            {
                //TODO:失敗したとき処理
                mesh.material = colors.materials[index = 1];
                watingsum += watingtime;
                yield return new WaitForSeconds(watingtime);
            }
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        phoneObj = this.gameObject;
        manager = GameObject.Find("manager");
        mesh = phoneObj.GetComponent<MeshRenderer>();
        colors = manager.GetComponent<MeshRenderer>();
        mesh.material = colors.materials[0];
        StartCoroutine(waitCall());
    }
}
