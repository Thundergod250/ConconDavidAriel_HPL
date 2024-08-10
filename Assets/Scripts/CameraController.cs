using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLookCamera;
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float zoomSmoothTime = 0.2f;
    [SerializeField] private float minZoom = 2f;
    [SerializeField] private float maxZoom = 15f;
    [SerializeField] private Vector3 topViewPosition = new(0, 30, 0);

    private Quaternion topViewRotation;
    private float targetZoom;
    private float zoomVelocity = 0f;
    private bool isTopView = false;

    private void Start()
    {
        targetZoom = freeLookCamera.m_Orbits[1].m_Radius;
        topViewRotation = Quaternion.Euler(90, 0, 0);
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            freeLookCamera.m_XAxis.m_InputAxisName = "Mouse X";
            freeLookCamera.m_YAxis.m_InputAxisName = "Mouse Y";
        }
        else
        {
            freeLookCamera.m_XAxis.m_InputAxisName = "";
            freeLookCamera.m_YAxis.m_InputAxisName = "";
        }

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            targetZoom -= scrollInput * zoomSpeed;
            targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        }

        float currentZoom = freeLookCamera.m_Orbits[1].m_Radius;
        float smoothZoom = Mathf.SmoothDamp(currentZoom, targetZoom, ref zoomVelocity, zoomSmoothTime);

        for (int i = 0; i < freeLookCamera.m_Orbits.Length; i++)
        {
            freeLookCamera.m_Orbits[i].m_Radius = smoothZoom;
        }
    }

    private void SetTopView()
    {
        freeLookCamera.gameObject.SetActive(false);
        transform.position = topViewPosition;
        transform.rotation = topViewRotation;
    }

    private void RestoreFreeLook()
    {
        freeLookCamera.gameObject.SetActive(true);
        transform.position = freeLookCamera.transform.position;
        transform.rotation = freeLookCamera.transform.rotation;
    }
}
