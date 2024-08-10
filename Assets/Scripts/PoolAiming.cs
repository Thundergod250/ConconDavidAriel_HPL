using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolAiming : MonoBehaviour
{
    [SerializeField] private Transform whiteBall;
    [SerializeField] private float rotationSpeed = 100f;

    private bool isAiming = false;
    private bool isAimingActive = false;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.DelayAndAdvance(); 
            this.enabled = false;
        }

        if (isAiming && IsAimingPhase())
        {
            Aim();
        }
    }

    public void StartAiming()
    {
        if (!isAimingActive && IsAimingPhase())
        {
            transform.position = whiteBall.position;
            isAimingActive = true;
            StartCoroutine(StartAimingWithDelay(0.1f));
        }
    }

    private IEnumerator StartAimingWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAiming = true;
    }

    private void Aim()
    {
        if (IsAimingPhase())
        {
            float rotationInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.RotateAround(whiteBall.position, Vector3.up, rotationInput);
        }
    }

    private bool IsAimingPhase()
    {
        return GameManager.Instance.gamePhase == GamePhase.aiming;
    }
}
