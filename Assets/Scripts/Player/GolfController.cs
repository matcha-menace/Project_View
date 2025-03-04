using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GolfController : MonoBehaviour
{
    public static GolfController Instance { get; private set; }
    
    private PlayerControls playerControls;
    private float inputDir;
    private Transform t;
    private float rotation;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private Slider strengthSlider;
    [SerializeField] private LineRenderer lineRenderer;
    private bool isAnticipating;
    private float anticipationTimer;
    
    [SerializeField] private BallBehavior ball;

    private void OnEnable()
    {
        playerControls.Golf.Enable();
    }

    private void OnDisable()
    {
        playerControls.Golf.Disable();
    }

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Golf.AimX.performed += ctx => OnCameraRotate(ctx);
        playerControls.Golf.Anticipate.performed += ctx => { isAnticipating = true; };
        playerControls.Golf.Anticipate.canceled += ctx => { isAnticipating = false; HitBall(); };
        t ??= transform;
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
        inputDir = ctx.ReadValue<float>();
    }
    
    private void FixedUpdate()
    {
        // _rotation = Mathf.Lerp(_rotation, _inputDir, Time.deltaTime);
        t.rotation = 
            Quaternion.Euler(t.eulerAngles + Vector3.up * rotationSpeed * inputDir * Time.deltaTime);

        BuildUpStrength();
    }

    private void BuildUpStrength()
    {
        if (isAnticipating)
        {
            anticipationTimer += Time.deltaTime;
            strengthSlider.value = Mathf.PingPong(anticipationTimer, 1);
        }
        else
        {
            strengthSlider.value = 0;
            anticipationTimer = 0;
        }
    }

    public void SetHittable(bool isHittable)
    {
        lineRenderer.gameObject.SetActive(isHittable);
        strengthSlider.gameObject.SetActive(isHittable);
        t.position = ball.transform.position;
    }

    private void HitBall()
    {
        if (ball.IsMoving) return;
        ball.HitBall(t.forward.normalized, strengthSlider.value);
        SetHittable(false);
    }
}
