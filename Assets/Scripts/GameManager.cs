using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public UnityEvent<int, int> EvtUIChanged;
    public UnityEvent EvtScored;
    public UnityEvent<GamePhase> EvtPhaseChanged;
    public UnityEvent EvtRoundCleared;
    public UnityEvent EvtRoundLost;
    public int BallsRequired = 1; 
    public int CurrentMoves = 0;
    public GamePhase gamePhase = GamePhase.movingBall;
    [SerializeField] private GameObject whiteBall;
    [SerializeField] private CueBallController cueBallController;
    [SerializeField] private PoolAiming poolAiming;
    [SerializeField] private PoolFiring poolFiring;
    [SerializeField] private float delayTime = 1f;
    [SerializeField] private float stopThreshold = 0.01f;
    private int MaxMoves = 5;
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
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        whiteBallRb = whiteBall.GetComponent<Rigidbody>();

        EvtUIChanged?.Invoke(CurrentMoves, BallsRequired); 
    }

    public void AdvancePhase()
    {
        if (phaseHolder != GamePhase.waiting)
            gamePhase = (GamePhase)(((int)phaseHolder + 1) % Enum.GetNames(typeof(GamePhase)).Length);
        else
            gamePhase = (GamePhase)(((int)phaseHolder + 2) % Enum.GetNames(typeof(GamePhase)).Length);
        PlayNextPhase(gamePhase);
        EvtPhaseChanged?.Invoke(gamePhase); 
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
                AddMoves(1); 
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

    public void SetPhase(GamePhase phase) => gamePhase = phase;

    public void PocketBall(int amount)
    {
        BallsRequired -= amount;
        if (BallsRequired < 0)
            BallsRequired = 0; 
        EvtUIChanged?.Invoke(CurrentMoves, BallsRequired);
        EvtScored?.Invoke();

        CheckNextRound(); 
    }

    public void AddMoves(int amount)
    {
        CurrentMoves += amount;
        EvtUIChanged?.Invoke(CurrentMoves, BallsRequired);

        CheckGameOver(); 
    }

    public void SetBallsRequired(int value)
    {
        BallsRequired = value;
        EvtUIChanged?.Invoke(CurrentMoves, BallsRequired);
    }

    private void CheckNextRound()
    {
        if (BallsRequired <= 0)
            EvtRoundCleared?.Invoke();
    }

    private void CheckGameOver()
    {
        if (CurrentMoves >= 5)
            EvtRoundLost?.Invoke();
    }
}
