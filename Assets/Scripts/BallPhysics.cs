using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.mass = 0.17f; 
        rb.drag = 0.1f;
        rb.angularDrag = 0.05f;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        PhysicsMaterial2D mat = new() { bounciness = 0.9f, friction = 0.1f };
    }
}
