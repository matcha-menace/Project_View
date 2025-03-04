using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public bool IsMoving { get; set; }
    [SerializeField] private Transform controllerAnchor;
    private float _floorTimer;
    private float curSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void HitBall(Vector3 dir, float force)
    {
        IsMoving = true;
        _rigidbody.AddForce(dir * force * 10, ForceMode.Impulse);
    }

    private void Update()
    {
        // Check if the ball is contacting the floor
        if (_rigidbody.velocity.y <= 0.1f && Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.5f))
        {
            _floorTimer += Time.deltaTime;
            curSpeed = GetBallSpeed(_floorTimer, _rigidbody.velocity, _rigidbody.angularVelocity);
            SetBallSpeed(curSpeed);
        }
        else
        {
            _floorTimer = 0f;
        }

        // Stop the ball if it's moving too slow
        if (_rigidbody.velocity.magnitude < 0.1f)
        {
            _rigidbody.velocity = Vector3.zero;
            IsMoving = false;
        }
    }

    private void SetBallSpeed(float speed)
    {
        if (_rigidbody.velocity.magnitude > 0.01f)
        {
            Vector3 direction = _rigidbody.velocity.normalized;
            _rigidbody.velocity = direction * speed; 
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
            IsMoving = false;
        }
    }

    private float GetBallSpeed(float contactTime, Vector3 velocity, Vector3 angularVelocity)
    {
        // Linear speed magnitude
        float linearSpeed = velocity.magnitude;

        // Rotational speed contribution
        float ballRadius = 1f;
        float rotationalSpeed = angularVelocity.magnitude * ballRadius;

        // (linear + rotational)
        float speed = linearSpeed + rotationalSpeed;

        // Apply friction over time if in contact with the floor
        float frictionFactor = Mathf.Max(0, 1f - (contactTime * 0.1f)); // Adjust slowdown rate as needed
        return speed * frictionFactor;
    }
}