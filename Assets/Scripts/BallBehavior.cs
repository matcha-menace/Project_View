using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public bool IsMoving { get; set; }
    [SerializeField] private Transform controllerAnchor;
    private float _floorTimer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void HitBall(Vector3 dir, float force)
    {
        IsMoving = true;
        _rigidbody.AddForce(dir * force * 10, ForceMode.Impulse);
        StartCoroutine(SlowDownRoutine());
    }

    private void Update()
    {
        
    }

    private void SlowDown()
    {
        if (!IsMoving) return;
    }

    private IEnumerator SlowDownRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        
        var deccelTimer = 0f;
        while (deccelTimer < 1.5f)
        {
            deccelTimer += Time.deltaTime;
            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, Vector3.zero, deccelTimer / 1.5f);
            _rigidbody.angularVelocity = Vector3.Lerp(_rigidbody.angularVelocity, Vector3.zero, deccelTimer / 1.5f);
            yield return null;
        }
        
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        IsMoving = false;
        controllerAnchor.position = transform.position;
    }
}
