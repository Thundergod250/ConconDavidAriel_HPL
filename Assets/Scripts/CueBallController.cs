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
        Vector3 newPosition = transform.position;
        newPosition.y = originalYPosition;
        transform.position = newPosition;
        if (TryGetComponent(out Rigidbody rb))
            rb.velocity = Vector3.zero;

        if (GameManager.Instance.gamePhase == GamePhase.movingBall)
        {
            
        }
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.gamePhase == GamePhase.movingBall)
        {
            mousePosition = Input.mousePosition - GetMousePos();
            ballCollider.isTrigger = true;
        }
    }

    private void OnMouseDrag()
    {
        if (GameManager.Instance.gamePhase == GamePhase.movingBall)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
        }

    }

    private void OnMouseUp()
    {
        ballCollider.isTrigger = false;
        transform.position = new Vector3(transform.position.x, originalYPosition, transform.position.z);
        if (TryGetComponent(out Rigidbody rb))
            rb.velocity = Vector3.zero;

        if (GameManager.Instance.gamePhase == GamePhase.movingBall)
            GameManager.Instance.AdvancePhase();
    }

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }
}
