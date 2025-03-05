using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform parent;  // Assign the ball object
    public float maxRotationSpeed = 90f; // Degrees per second

    private Rigidbody _rigidbody;

    private GameObject ball;

    private BallBehavior ballBehavior;

    [SerializeField] private float lerpSpeed = 0.2f;

    private void Awake()
    {
        ball = GameObject.FindGameObjectWithTag("Player");
        ballBehavior = ball.GetComponent<BallBehavior>();
        _rigidbody = ballBehavior._rigidbody;

    }

    private void LateUpdate()
    {
        if (parent == null) return;
        
        transform.position = parent.position;

        // Get the current rotation difference
        Quaternion targetRotation = parent.rotation;
        float maxDegreesDelta = maxRotationSpeed * Time.deltaTime;

        // Smoothly rotate the child towards the parent with a speed limit
        if (_rigidbody.velocity.magnitude > ballBehavior.stopBallVelocityThreshold)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxDegreesDelta);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lerpSpeed);
        }

    }
}
