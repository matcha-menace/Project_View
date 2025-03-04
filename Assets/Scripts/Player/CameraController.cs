using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    // private PlayerControls _controls;
    // private float _inputDir;
    // private Transform _t;
    // private float _rotation;
    // [SerializeField] private float rotationSpeed;

    private void OnEnable()
    {
        // _controls.Room.Enable();
    }

    private void OnDisable()
    {
        // _controls.Room.Disable();
    }

    private void Awake()
    {
        // _controls = new PlayerControls();
        // _controls.Room.CameraRotate.performed += ctx => OnCameraRotate(ctx);
        // _t ??= transform;
    }

    private void OnCameraRotate(InputAction.CallbackContext ctx)
    {
        // _inputDir = ctx.ReadValue<float>();
    }
    
    private void FixedUpdate()
    {
        // _rotation = Mathf.Lerp(_rotation, _inputDir, Time.deltaTime);
        // _t.rotation = 
        //     Quaternion.Euler(_t.eulerAngles + Vector3.up * rotationSpeed * _rotation * Time.deltaTime);
    }
}
