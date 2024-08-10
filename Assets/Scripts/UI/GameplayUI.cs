using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mainText;

    private void Start()
    {
        mainText.text = "Click and drag the White Ball";
        GameManager.Instance.EvtPhaseChanged.AddListener(OnPhaseChange); 
    }

    private void OnPhaseChange(GamePhase gamePhase)
    {
        if (gamePhase == GamePhase.movingBall)
        {
            mainText.text = "Click and drag the White Ball";
        }
        else if (gamePhase == GamePhase.aiming)
        {
            mainText.text = "Up and Down your mouse to Aim";
        }
        else if (gamePhase == GamePhase.firing)
        {
            mainText.text = "Up and Down your mouse to change Power";
        }
    }
}
