using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fading : MonoBehaviour
{
    [SerializeField] private Image blackImage;
    [SerializeField] private float fadeSpeed = 1f;

    private void Start()
    {
        FadeOut();
    }
    
    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeInCoroutine()

    {
        blackImage.gameObject.SetActive(true);
        Color color = blackImage.color;
        color.a = 1f;
        blackImage.color = color;

        while (color.a > 0f)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            blackImage.color = color;
            yield return null;
        }

        blackImage.gameObject.SetActive(false);
    }

    IEnumerator FadeOutCoroutine()
    {
        blackImage.gameObject.SetActive(true);
        Color color = blackImage.color;
        color.a = 0f;
        blackImage.color = color;

        while (color.a < 1f)
        {
            color.a += fadeSpeed * Time.deltaTime;
            blackImage.color = color;
            yield return null;
        }
    }
}
