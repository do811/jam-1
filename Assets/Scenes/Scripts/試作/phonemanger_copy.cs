using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using GameObjectlib;
using System.Threading;
using System.Diagnostics;
public class phoneManager : MonoBehaviour
{
    private GameObject phoneObj;
    private GameObject manager;
    private int watingtime;
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

    int maxcycle = 2;//�ő�Œ��M�����Ȃ���
    int currentcycle = 0; //�d�b�������񐔁H
    int watingsum = 0;
    int phonecalltime = 0;
    //���Ԑ���Ȃ��IEnumerator�ɂ��R���[�`��
    IEnumerator waitCall()
    {
        for (; ; currentcycle++)
        {
            if (phonecalltime >= 2)
            {
                break;
            }
            watingtime = Random.Range(1, 6);
            if (watingtime <= 2 && watingsum >= 8)
            {
                mesh.material = colors.materials[index = 0];//�����ŐԐF�ɂ���
                yield return new WaitForSeconds(watingtime);
                StopWatch.StopAndGetTime();
                BackInitialPositoin(phoneObj);
                BackInitialRotate(phoneObj);
                currentcycle = 0; //����Ŗ������[�v�H
                phonecalltime += 1;
            }
            else
            {
                //TODO:���s�����Ƃ�����
                mesh.material = colors.materials[index = 1];
                watingsum += watingtime;
                yield return new WaitForSeconds(watingtime);
                phonecalltime += 1;
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

