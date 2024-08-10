using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GamePhase
{
    movingBall,
    aiming, 
    firing, 
    waiting
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int score = 0;
    public GamePhase gamePhase = GamePhase.movingBall;

    [SerializeField] private CueBallController cueBallController;
    [SerializeField] private PoolStickController poolStickController;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AdvancePhase()
    {
        gamePhase = (GamePhase)(((int)gamePhase + 1) % Enum.GetNames(typeof(GamePhase)).Length);
        PlayNextPhase(gamePhase); 
    }

    private void PlayNextPhase(GamePhase gamePhase)
    {
        switch (gamePhase)
        {
            /*case GamePhase.movingBall:
                cueBallController.enabled = true;
                poolStickController.enabled = false;
                break;

            case GamePhase.aiming:
                Debug.LogWarning("AIMING MODE"); 
                cueBallController.enabled = false;
                poolStickController.enabled = true;
                break;*/

            /*case GamePhase.firing:
                break;

            case GamePhase.waiting:
                cueBallController.enabled = false;
                poolStickController.enabled = false;
                break;*/

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void SetPhase(GamePhase phase)
    {
        gamePhase = phase;
    }

    public void AddScore(int amount)
    {
        score += amount;
    }
}
