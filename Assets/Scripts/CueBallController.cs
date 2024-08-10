using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBallController : MonoBehaviour
{
    [SerializeField] private float originalYPosition = 0.3172536f;
    private Vector3 mousePosition;
    private Collider ballCollider;

    private void Start()
    {
        ballCollider = GetComponent<Collider>(); 
    }

    private void Update()
    {
        if (IsMovingBallPhase())
        {
            KeepBallAtOriginalHeight();
            StopBallMovement();
        }
    }

    private void OnMouseDown()
    {
        if (IsMovingBallPhase())
        {
            mousePosition = Input.mousePosition - GetMousePos();
            ballCollider.isTrigger = true;
        }
    }

    private void OnMouseDrag()
    {
        if (IsMovingBallPhase())
        {
            MoveBallWithMouse();
        }
    }

    private void OnMouseUp()
    {
        ballCollider.isTrigger = false;
        ResetBallPosition();
        StopBallMovement();

        if (IsMovingBallPhase())
        {
            GameManager.Instance.AdvancePhase();
        }
    }

    private bool IsMovingBallPhase()
    {
        return GameManager.Instance.gamePhase == GamePhase.movingBall;
    }

    private void KeepBallAtOriginalHeight()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = originalYPosition;
        transform.position = newPosition;
    }

    private void StopBallMovement()
    {
        if (TryGetComponent(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void MoveBallWithMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }

    private void ResetBallPosition()
    {
        transform.position = new Vector3(transform.position.x, originalYPosition, transform.position.z);
    }

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }
}
