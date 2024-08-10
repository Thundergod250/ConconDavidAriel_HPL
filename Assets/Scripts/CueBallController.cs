using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBallController : MonoBehaviour
{
    [SerializeField] private float liftHeight = 1f;
    [SerializeField] private LayerMask ballLayerMask;
    [SerializeField] private LayerMask tableLayerMask;
    [SerializeField] private float minDistanceFromBall = 0.5f;
    [SerializeField] private float tableBoundaryMargin = 0.2f;

    private Vector3 initialPosition;
    private Vector3 mousePosition;
    private bool isDragging = false;
    private Collider ballCollider;

    private void Start()
    {
        ballCollider = GetComponent<Collider>(); 
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePos();
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minDistanceFromBall);
    }
}
