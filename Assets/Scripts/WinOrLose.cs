using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinOrLose : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.HasInstance())
        {
            GameManager.Instance.EvtRoundCleared.AddListener(OnRoundCleared);
            GameManager.Instance.EvtRoundLost.AddListener(OnRoundLost);
        }
    }

    private void OnRoundCleared()
    {
        PlayNextScene(); 
    }

    private void OnRoundLost()
    {
        PlayGameOverScene();
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

    private void PlayGameOverScene()
    {
        int lastSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
        SceneManager.LoadScene(lastSceneIndex);
    }
}
