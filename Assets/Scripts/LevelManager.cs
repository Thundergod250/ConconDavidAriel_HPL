using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int requiredBalls = 1;

    private void Start()
    {
        GameManager.Instance.SetBallsRequired(requiredBalls);
    }
}
