using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private GameObject[] moves;
    [SerializeField] private GameObject[] ballsRequired;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float fadeDuration = 1f;

    private void Start()
    {
        scoreText.text = string.Empty;
        if (GameManager.HasInstance())
        {
            GameManager.Instance.EvtUIChanged.AddListener(OnUpdateQuestUI);
            GameManager.Instance.EvtScored.AddListener(OnScored);
        }
    }

    private void OnScored()
    {
        scoreText.text = "Scored!";
        StartCoroutine(FadeOutScoreText());
    }

    private IEnumerator FadeOutScoreText()
    {
        float elapsedTime = 0f;
        Color tempColor = scoreText.color;

        while (elapsedTime < fadeDuration)
        {
            tempColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            scoreText.color = tempColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        scoreText.text = string.Empty;
    }

    private void OnUpdateQuestUI(int moveNumber, int ballsRequiredNumber)
    {
        foreach (GameObject move in moves)
            move.SetActive(false);
        foreach (GameObject ball in ballsRequired)
            ball.SetActive(false);

        for (int i = 0; i < moveNumber; i++)
            moves[i].SetActive(true);
        for (int i = 0; i < ballsRequiredNumber; i++)
            ballsRequired[i].SetActive(true);
    }
}
