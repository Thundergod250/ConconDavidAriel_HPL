using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BilliardBall>() != null)
        {
            other.gameObject.SetActive(false);
            GameManager.Instance.PocketBall(1); 
        }
    }
}
