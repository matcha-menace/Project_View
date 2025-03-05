using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform parent;  // Assign the ball object
    public float maxRotationSpeed = 90f; // Degrees per second

    private void LateUpdate()
    {
        if (parent == null) return;
        
        transform.position = parent.position;

        // Get the current rotation difference
        Quaternion targetRotation = parent.rotation;
        float maxDegreesDelta = maxRotationSpeed * Time.deltaTime;

        // Smoothly rotate the child towards the parent with a speed limit
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxDegreesDelta);
    }
}
