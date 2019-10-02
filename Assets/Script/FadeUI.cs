using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeUI : MonoBehaviour
{
    public Image fadeBG;

    public AnimationCurve curve;

    private void Awake()
    {
        fadeBG.gameObject.SetActive(true);
    }

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f) 
        {
            t -= Time.deltaTime;

            float alpha = curve.Evaluate(t);
            fadeBG.color = new Color(0f, 0f, 0f, alpha);
            yield return 0;      
        }

        fadeBG.gameObject.SetActive(false);
    }

    IEnumerator FadeOut()
    {
        fadeBG.gameObject.SetActive(true);
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime;

            float alpha = curve.Evaluate(t);
            fadeBG.color = new Color(0f, 0f, 0f, alpha);
            yield return 0;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator FadeOut(string scene)
    {
        fadeBG.gameObject.SetActive(true);
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime;

            float alpha = curve.Evaluate(t);
            fadeBG.color = new Color(0f, 0f, 0f, alpha);
            yield return 0;
        }
        SceneManager.LoadScene(scene);
    }

    IEnumerator FadeOut(int scene)
    {        
        fadeBG.gameObject.SetActive(true);
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime;

            float alpha = curve.Evaluate(t);
            fadeBG.color = new Color(0f, 0f, 0f, alpha);
            yield return 0;
        }
        SceneManager.LoadScene(scene);
        Debug.Log("dddddddddd");
    }

    public void FadeTO()
    {
        Time.timeScale = 1;
        StartCoroutine(FadeOut());
    }

    public void FadeTO(string scene)
    {
        Time.timeScale = 1;
        StartCoroutine(FadeOut(scene));
    }

    public void FadeTO(int scene)
    {
        Time.timeScale = 1;
        StartCoroutine(FadeOut(scene));
    }

}
