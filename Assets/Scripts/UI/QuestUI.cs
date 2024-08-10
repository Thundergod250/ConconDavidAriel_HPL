using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private GameObject[] moves;
    [SerializeField] private GameObject[] ballsRequired;

    private void Start()
    {
        GameManager.Instance.EvtUIChanged.AddListener(UpdateQuestUI); 
    }

    private void UpdateQuestUI(int moveNumber, int ballsRequiredNumber)
    {
        for (int i = 0; i < moveNumber; i++)
        {
            moves[i].SetActive(true);
        }
        for (int i = 0; i < ballsRequiredNumber; i++)
        {
            ballsRequired[i].SetActive(true);
        }
    }
}
