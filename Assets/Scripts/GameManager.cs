using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GamePhase
{
    movingBall,
    aiming, 
    firing, 
    waiting, 
    delay
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int score = 0;
    public GamePhase gamePhase = GamePhase.movingBall;
    [SerializeField] private CueBallController cueBallController;
    [SerializeField] private PoolAiming poolAiming;
    [SerializeField] private PoolFiring poolFiring;
    [SerializeField] private PoolStickController poolStickController;
    [SerializeField] private float delayTime = 1f;
    private GamePhase phaseHolder; 

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
        gamePhase = (GamePhase)(((int)phaseHolder + 1) % Enum.GetNames(typeof(GamePhase)).Length);
        PlayNextPhase(gamePhase); 
    }

    public void DelayAndAdvance()
    {
        phaseHolder = gamePhase; 
        gamePhase = GamePhase.delay;
        StartCoroutine(DelayedAdvance());
    }

    private IEnumerator DelayedAdvance()
    {
        yield return new WaitForSeconds(delayTime);
        AdvancePhase();
    }

    private void PlayNextPhase(GamePhase gamePhase)
    {
        switch (gamePhase)
        {
            case GamePhase.movingBall:
                break;

            case GamePhase.aiming:
                poolAiming.enabled = true;
                poolAiming.StartAiming();
                break;

            case GamePhase.firing:
                poolFiring.enabled = true;
                break;

            case GamePhase.waiting:
                cueBallController.enabled = false;
                poolStickController.enabled = false;
                break;

            case GamePhase.delay:
                break;

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
