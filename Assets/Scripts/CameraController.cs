using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;      // Speed of the panning movement
    public float rotationSpeed = 100f; // Speed of the camera rotation
    public Vector3 panLimitMin;       // Minimum limit for panning
    public Vector3 panLimitMax;       // Maximum limit for panning

    private Vector3 initialPosition;

    void Start()
    {
        // Save the initial position of the camera
        initialPosition = transform.position;
    }

    void Update()
    {
        HandlePanning();
        HandleRotation();
    }

    void HandlePanning()
    {
        if (Input.GetMouseButton(0)) // Left mouse button
        {
            float moveX = Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
            float moveZ = Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;

            Vector3 newPosition = transform.position + transform.right * moveX + transform.forward * moveZ;
            newPosition.x = Mathf.Clamp(newPosition.x, panLimitMin.x, panLimitMax.x);
            newPosition.y = Mathf.Clamp(newPosition.y, panLimitMin.y, panLimitMax.y);
            newPosition.z = Mathf.Clamp(newPosition.z, panLimitMin.z, panLimitMax.z);

            transform.position = newPosition;
        }
    }

    void HandleRotation()
    {
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

            // Rotate the camera around its Y-axis (left-right)
            transform.Rotate(0, rotationX, 0, Space.World);
        }
    }
}
