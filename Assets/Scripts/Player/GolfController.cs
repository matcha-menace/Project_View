using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GolfController : MonoBehaviour
{
    private PlayerControls _controls;
    private float _inputDir;
    private Transform _t;
    private float _rotation;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private Slider strengthSlider;
    private bool _isAnticipating;
    
    [SerializeField] private BallBehavior ball;

    private void OnEnable()
    {
        _controls.Golf.Enable();
    }

    private void OnDisable()
    {
        _controls.Golf.Disable();
    }

    private void Awake()
    {
        _controls = new PlayerControls();
        _controls.Golf.AimX.performed += ctx => OnCameraRotate(ctx);
        _controls.Golf.Anticipate.performed += ctx => { _isAnticipating = true; };
        _controls.Golf.Anticipate.canceled += ctx => { _isAnticipating = false; HitBall(); };
        _t ??= transform;
    }

    private void OnCameraRotate(InputAction.CallbackContext ctx)
    {
        _inputDir = ctx.ReadValue<float>();
    }
    
    private void FixedUpdate()
    {
        // _rotation = Mathf.Lerp(_rotation, _inputDir, Time.deltaTime);
        _t.rotation = 
            Quaternion.Euler(_t.eulerAngles + Vector3.up * rotationSpeed * _inputDir * Time.deltaTime);

        BuildUpStrength();
    }

    private void BuildUpStrength()
    {
        if (_isAnticipating)
        {
            var value = strengthSlider.value < 0.9f ? 0.1f : -0.1f;
            strengthSlider.value += value;
        }
        else
        {
            strengthSlider.value -= 0.1f;
        }
    }

    private void HitBall()
    {
        if (ball.IsMoving) return;
        ball.HitBall(_t.forward.normalized, strengthSlider.value);
    }
}
