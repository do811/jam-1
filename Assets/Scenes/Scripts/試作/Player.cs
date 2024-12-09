using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameObjectlib;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class Player : MonoBehaviour
{
    private GameObject phoneObj;
    private MeshRenderer phoneStat;
    private MeshRenderer status;
    private GameObject maincamera;
    private int index = 0;
    private void BackInitialPositoin(GameObject phone)
    {
        Container target = new(phone);
        target &= (-4, 0, 10);
    }
    private void MoveTakenPosition(GameObject phone)
    {
        Container target = new(phone);
        target &= (3f, 3.5f, 9.5f);
    }

    private void RotateTakenAngle(GameObject phone)
    {
        Transform phoneTrs = phone.GetComponent<Transform>();
        phoneTrs.eulerAngles = new Vector3(-70, 90, 30);
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
        phoneStat = phoneObj.GetComponent<MeshRenderer>();
        status = gameObject.GetComponent<MeshRenderer>();
        maincamera = GameObject.Find("Sight");
    }

    // Update is called once per frame
    void Update()
    {
        if (phoneStat.material.color == status.materials[0].color
        && phoneStat.material.mainTexture == status.materials[0].mainTexture)
        {
            Debug.Log("now");
            if (Input.GetKey(KeyCode.I))
            {
                TakePhone(phoneObj);
                Debug.Log("Took!");
            }
        }
    }
}
