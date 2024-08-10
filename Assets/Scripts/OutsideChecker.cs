using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BilliardBall>() != null)
        {
            other.gameObject.SetActive(false);
        }
    }
}
