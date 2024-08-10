using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolStickClick : MonoBehaviour
{
    [SerializeField] private PoolStickController poolStickController;

    private void OnMouseDown()
    {
        if (poolStickController != null)
            poolStickController.StartAiming();
    }
}