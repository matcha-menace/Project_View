using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public bool IsMoving { get; set; }
    private float floorTimer;
    private float curSpeed;

    [SerializeField] private float contactTimePenalty = 0.005f;
    [SerializeField] private float vertSpeedThreshold = 0.01f;

    [SerializeField] private float stopBallVelocityThreshold = 0.005f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        // TODO: temp
        GameManager.Instance.SavePosition(transform.position);
    }

    public void HitBall(Vector3 dir, float force)
    {
        IsMoving = true;
        _rigidbody.AddForce(dir * force * 50, ForceMode.Impulse);
        floorTimer = 0f;
        GolfController.Instance.SetHittable(false);
    }

    private void Update()
    {
        // Check if the ball is contacting the floor
        if (_rigidbody.velocity.y <= vertSpeedThreshold && Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.5f))
        {
            // Debug.Log(curSpeed);
            floorTimer += Time.deltaTime;
            curSpeed = GetBallSpeed(floorTimer, _rigidbody.velocity, _rigidbody.angularVelocity);
            SetBallSpeed(curSpeed);
        }
        else
        {
            floorTimer = 0f;
        }

        // Stop the ball if it's moving too slow
        // TODO: Optimize
        if (_rigidbody.velocity.magnitude < stopBallVelocityThreshold)
        {
            Debug.Log(_rigidbody.velocity.magnitude);
            _rigidbody.velocity = Vector3.zero;
            IsMoving = false;
            Invoke(nameof(SetHittable), 1f);

        }
        else
        {
            GolfController.Instance.SetHittable(false);
            IsMoving = true;
        }
    }

    private void SetHittable()
    {
        if (!IsMoving)
        {
            GolfController.Instance.SetHittable(true);
        }
    }

    public void Reset()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        IsMoving = false;
    }

    private void SetBallSpeed(float speed)
    {
        if (_rigidbody.velocity.magnitude > stopBallVelocityThreshold)
        {
            Vector3 direction = _rigidbody.velocity.normalized;
            _rigidbody.velocity = direction * speed; 
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }

    private float GetBallSpeed(float contactTime, Vector3 velocity, Vector3 angularVelocity)
    {
        // Linear speed magnitude
        float linearSpeed = velocity.magnitude;
        // Apply friction over time if in contact with the floor
        float frictionFactor = Mathf.Max(0, 1f - (contactTime * contactTimePenalty)); // Adjust slowdown rate as needed
        // Debug.Log("Friction Factor" + frictionFactor);

        return linearSpeed * frictionFactor;
    }
}