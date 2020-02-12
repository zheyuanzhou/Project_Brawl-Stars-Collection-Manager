using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public Image blackImage;
    private float alpha;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string _sceneName)
    {
        StartCoroutine(Fadeout(_sceneName));
    }

    IEnumerator FadeIn()
    {
        alpha = 1;
        while(alpha > 0)
        {
            alpha -= Time.deltaTime;
            blackImage.color = new Color(0, 0, 0, alpha);//000 is black
            yield return new WaitForSeconds(0);
        }
    }

    IEnumerator Fadeout(string _sceneName)
    {
        alpha = 0;

        while(alpha < 1)
        {
            alpha += Time.deltaTime;
            blackImage.color = new Color(0, 0, 0, alpha);//if 111 is white
            yield return new WaitForSeconds(0);
        }

        SceneManager.LoadScene(_sceneName);
    }

}
