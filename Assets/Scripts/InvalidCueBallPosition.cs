using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvalidCueBallPosition : MonoBehaviour
{
    public UnityEvent EvtBallInvalidPosition;
    [SerializeField] private Transform ballOrigin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CueBallController cueBallController))
        {
            cueBallController.gameObject.transform.position = ballOrigin.position;
            cueBallController.GetComponent<Rigidbody>().velocity = Vector3.zero;
            EvtBallInvalidPosition?.Invoke();
        }
    }
}
