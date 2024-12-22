using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankingButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public void SwitchRanking()
    {
        SceneManager.LoadScene("PlayerRanking");
    }

    public void SwitchGlobalRanking()
    {
        SceneManager.LoadScene("GlobaRanking");
    }

    public void SwitchPlayerRanking()
    {
        SceneManager.LoadScene("PlayerRanking");
    }
    public void BackStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
