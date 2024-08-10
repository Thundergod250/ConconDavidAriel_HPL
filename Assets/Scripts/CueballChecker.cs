using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CueballChecker : MonoBehaviour
{
    public UnityEvent EvtBallOutsideRange;
    [SerializeField] private GameObject ball;
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private Transform ballOrigin; 

    private void Update()
    {
        float distance = Vector3.Distance(ball.transform.position, ballOrigin.position);
        if (distance > maxDistance)
        {
            ball.transform.position = ballOrigin.position;
            Rigidbody ballRb = ball.GetComponent<Rigidbody>(); 
            ballRb.velocity = Vector3.zero;
            ballRb.rotation = Quaternion.identity;
            EvtBallOutsideRange?.Invoke();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
