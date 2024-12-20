using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private GameObject ManagerObj;
    private GameObject DisplayObj;
    private GameObject SpeakerObj;
    private MeshRenderer textures;
    private Image display;
    // Start is called before the first frame update
    void Start()
    {
        ManagerObj = GameObject.Find("EventSystem");
        DisplayObj = GameObject.Find("phonedisplay");
        SpeakerObj = GameObject.Find("Speaker");
        textures = ManagerObj.GetComponent<MeshRenderer>();
        display = DisplayObj.GetComponent<Image>();
        display.material = textures.materials[0];
        SpeakerObj.SetActive(false);
    }

    public void StartButtonOnClick()
    {
        display.material = textures.materials[1];
        SpeakerObj.SetActive(true);
        StartCoroutine(GoingScene());
    }

    private IEnumerator GoingScene()
    {
        yield return new WaitForSeconds(3f/*とりあえずこの値*/);
        SceneManager.LoadScene("main_1");
    }
}
