using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public void RewardButton()
    {
        GameManager.instance.money += 500;
        GameManager.instance.gem += 20;
        GameManager.instance.playerExp += 50;
    }

    public void ReturnButton()
    {
        FindObjectOfType<SceneFader>().FadeTo("00_Menu");//SceneManager.LoadScene("00_Menu");
    }

}
