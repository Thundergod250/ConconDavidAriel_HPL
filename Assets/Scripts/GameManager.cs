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
    public int currentMoves = 0;
    public int moveLimit = 0;
    public GamePhase gamePhase = GamePhase.movingBall;
    [SerializeField] private GameObject whiteBall;
    [SerializeField] private CueBallController cueBallController;
    [SerializeField] private PoolAiming poolAiming;
    [SerializeField] private PoolFiring poolFiring;
    [SerializeField] private float delayTime = 1f;
    [SerializeField] private float stopThreshold = 0.01f;
    private GamePhase phaseHolder;
    private Rigidbody whiteBallRb;

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

    private void Start()
    {
        whiteBallRb = whiteBall.GetComponent<Rigidbody>();
    }

    public void AdvancePhase()
    {
        if (phaseHolder != GamePhase.waiting)
            gamePhase = (GamePhase)(((int)phaseHolder + 1) % Enum.GetNames(typeof(GamePhase)).Length);
        else
            gamePhase = (GamePhase)(((int)phaseHolder + 2) % Enum.GetNames(typeof(GamePhase)).Length);
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
                StartCoroutine(WaitForBallToStop());
                break;

            case GamePhase.delay:
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private IEnumerator WaitForBallToStop()
    {
        while (whiteBallRb.velocity.magnitude > stopThreshold)
        {
            whiteBallRb.velocity *= 0.95f; 
            yield return null; 
        }
        whiteBallRb.velocity = Vector3.zero;
        DelayAndAdvance();
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
