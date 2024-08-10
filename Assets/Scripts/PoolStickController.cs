using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolStickController : MonoBehaviour
{
    [SerializeField] private Transform whiteBall;         
    [SerializeField] private Transform poolStick;         
    [SerializeField] private float rotationSpeed = 100f;  
    [SerializeField] private float maxPullBackDistance = 1.5f; 
    [SerializeField] private float hitForceMultiplier = 10f;
    [SerializeField] private float sensitivity;

    private bool isAiming = false;
    private bool isAimingActive = false;
    private Vector3 initialStickPosition;
    private Quaternion initialStickRotation;
    private float pullBackDistance = 0f;
    private Vector3 originalPivotPosition;
    private Quaternion originalPivotRotation;

    private void Start()
    {
        originalPivotPosition = transform.position;
        originalPivotRotation = transform.rotation;
        initialStickPosition = poolStick.localPosition;
        initialStickRotation = poolStick.localRotation;
    }

    private void Update()
    {
        if (isAiming)
        {
            AimAndAdjustPower();
            if (Input.GetMouseButtonDown(0))
            {
                FireCueBall();
            }
        }
    }

    public void StartAiming()
    {
        if (!isAimingActive)
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

    private void CancelAiming()
    {
        transform.position = originalPivotPosition;
        transform.rotation = originalPivotRotation;
        poolStick.localPosition = initialStickPosition;
        poolStick.localRotation = initialStickRotation;
        isAiming = false;
        pullBackDistance = 0f;
        isAimingActive = false;
    }

    private void AimAndAdjustPower()
    {
        float rotationInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        transform.RotateAround(whiteBall.position, Vector3.up, rotationInput);
        float pullBackInput = Input.GetAxis("Mouse Y");
        pullBackDistance = Mathf.Clamp(pullBackDistance - pullBackInput * sensitivity, 0f, maxPullBackDistance);
        Vector3 pullBackDirection = poolStick.forward * -1;
        poolStick.localPosition = initialStickPosition + pullBackDirection * pullBackDistance;
    }

    private void FireCueBall()
    {
        Vector3 direction = (whiteBall.position - poolStick.position).normalized;
        Rigidbody whiteBallRigidbody = whiteBall.GetComponent<Rigidbody>();
        whiteBallRigidbody.AddForce(direction * hitForceMultiplier * pullBackDistance, ForceMode.Impulse);
        CancelAiming();
    }
}
