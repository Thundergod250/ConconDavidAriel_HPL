using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinOrLose : MonoBehaviour
{
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private Image gameOverPanelImage;
    [SerializeField] private float fadeSpeed = 1f;

    private void Start()
    {
        GameManager.Instance.EvtRoundCleared.AddListener(OnRoundCleared);
        GameManager.Instance.EvtRoundLost.AddListener(OnRoundLost);
    }

    private void OnRoundCleared()
    {
        PlayNextScene(); 
    }

    private void OnRoundLost()
    {
        OpenGameOverScreen();
    }

    public void PlayNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more scenes to load!");
        }
    }

    private void OpenGameOverScreen()
    {
        GameOverScreen.SetActive(true);
        StartCoroutine(FadeInGameOverScreen());
    }

    private IEnumerator FadeInGameOverScreen()
    {
        Color color = gameOverPanelImage.color;
        color.a = 0f;
        gameOverPanelImage.color = color;

        while (color.a < 1f)
        {
            color.a += fadeSpeed * Time.deltaTime;
            gameOverPanelImage.color = color;
            yield return null;
        }
    }

    public void PlayStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
