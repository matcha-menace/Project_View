using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GolfController : MonoBehaviour
{
    public static GolfController Instance { get; private set; }
    
    private PlayerControls _controls;
    private float _inputDir;
    private Transform _t;
    private float _rotation;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private Slider strengthSlider;
    [SerializeField] private LineRenderer lineRenderer;
    private bool _isAnticipating;
    private float _anticipationTimer;
    
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
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
            _anticipationTimer += Time.deltaTime;
            strengthSlider.value = Mathf.PingPong(_anticipationTimer, 1);
        }
        else
        {
            strengthSlider.value = 0;
            _anticipationTimer = 0;
        }
    }

    public void SetHittable(bool isHittable)
    {
        lineRenderer.gameObject.SetActive(isHittable);
        strengthSlider.gameObject.SetActive(isHittable);
    }

    private void HitBall()
    {
        if (ball.IsMoving) return;
        ball.HitBall(_t.forward.normalized, strengthSlider.value);
        SetHittable(false);
    }
}
