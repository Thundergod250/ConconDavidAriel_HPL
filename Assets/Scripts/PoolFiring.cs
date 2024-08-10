using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolFiring : MonoBehaviour
{
    [SerializeField] private Transform whiteBall;
    [SerializeField] private Transform poolStick;
    [SerializeField] private float maxPullBackDistance = 1.5f;
    [SerializeField] private float hitForceMultiplier = 10f;
    [SerializeField] private float sensitivity;

    private float pullBackDistance = 0f;
    private Vector3 originalPivotPosition;
    private Quaternion originalPivotRotation;
    private Vector3 initialStickPosition;
    private Quaternion initialStickRotation;
    private bool isAiming = false;
    private bool isAimingActive = false;

    private void OnEnable()
    {
        originalPivotPosition = transform.position;
        originalPivotRotation = transform.rotation;
        initialStickPosition = poolStick.localPosition;
        initialStickRotation = poolStick.localRotation;
    }

    private void Update()
    {
        if (IsFiringPhase())
        {
            AdjustPower();
            if (Input.GetMouseButtonDown(0))
            {
                FireCueBall();
            }
        }
    }

    public void FireCueBall()
    {
        if (IsFiringPhase())
        {
            Vector3 direction = (whiteBall.position - poolStick.position).normalized;
            Rigidbody whiteBallRigidbody = whiteBall.GetComponent<Rigidbody>();
            whiteBallRigidbody.AddForce(direction * hitForceMultiplier * pullBackDistance, ForceMode.Impulse);
            CancelAiming();
        }
    }

    private void AdjustPower()
    {
        if (IsFiringPhase())
        {
            float pullBackInput = Input.GetAxis("Mouse Y");
            pullBackDistance = Mathf.Clamp(pullBackDistance - pullBackInput * sensitivity, 0f, maxPullBackDistance);
            Vector3 pullBackDirection = poolStick.forward * -1;
            poolStick.localPosition = initialStickPosition + pullBackDirection * pullBackDistance;
        }
    }

    private void CancelAiming()
    {
        if (IsFiringPhase())
        {
            transform.position = originalPivotPosition;
            transform.rotation = originalPivotRotation;
            poolStick.localPosition = initialStickPosition;
            poolStick.localRotation = initialStickRotation;
            isAiming = false;
            pullBackDistance = 0f;
            isAimingActive = false;
        }

        EndPhase(); 
    }

    private void EndPhase()
    {
        GameManager.Instance.DelayAndAdvance();
        this.enabled = false; 
    }

    private bool IsFiringPhase()
    {
        return GameManager.Instance.gamePhase == GamePhase.firing;
    }
}