using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameObjectlib;
using PlayFab.ClientModels;
using TMPro;
using OnePlay;
public class Results : MonoBehaviour
{
    private static Container texts;
    private RectTransform texT;
    GameObject Totaltext;
    // Start is called before the first frame update
    void Start()
    {
        Totaltext = GameObject.Find("Total");
        texts = new(this.gameObject);
        texT = texts.Obj.GetComponent<RectTransform>();
        texT.position = new Vector3(-200, 0, 0);
    }


    double time = 0;
    double span = 0.3f;
    double difference_x = 0;
    const double max_difference_x = 800f;
    Vector3 targetPos = new Vector3();
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time < span)
        {
            difference_x = max_difference_x * (time / span);
            targetPos.x = -200f + (float)difference_x;
            targetPos.z = 0f;
            targetPos.y = 200f;
            texT.position = targetPos;
        }
        else
        {
            TextMeshProUGUI[] Results = {
                GameObject.Find("Result1").GetComponent<TextMeshProUGUI>(),
                GameObject.Find("Result2").GetComponent<TextMeshProUGUI>(),
                GameObject.Find("Result3").GetComponent<TextMeshProUGUI>(),
                GameObject.Find("Result4").GetComponent<TextMeshProUGUI>(),
                GameObject.Find("Result5").GetComponent<TextMeshProUGUI>()
            };
            int i = 1;
            foreach (var one in ScoreHolder<List<int>>.Scores)
            {
                Results[i - 1].text = $"{i}コール目  {one}点";
                if (i > 5) break;
                i++;
            }
            Totaltext.SetActive(true);
            enabled = false;
        }
    }
}
