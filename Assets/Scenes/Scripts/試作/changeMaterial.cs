using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using GameObjectlib;
using System.Threading;
public class changeMaterial : MonoBehaviour
{
    private GameObject phoneObj;
    private GameObject manager;
    private int watingtime;
    private MeshRenderer mesh;
    private int index = 0;
    private MeshRenderer colors;

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

    int i = 0;
    //時間制御なんでIEnumeratorによるコルーチン
    IEnumerator waitCall()
    {
        for (; ; i++)
        {
            watingtime = Random.Range(1, 7);
            if (watingtime <= 2 && i > 3)
            {
                Debug.Log("oned");
                mesh.material = colors.materials[index = 0];
                yield return new WaitForSeconds(watingtime);
                BackInitialPositoin(phoneObj);
                BackInitialRotate(phoneObj);
                i = 0;
            }
            else
            {
                mesh.material = colors.materials[index = 1];
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
