using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ClickableCamera : MonoBehaviour
{
    private CinemachineVirtualCamera objectCamera;
    public float delayTime = 3f;
    
    private void Start()
    {
        objectCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void OnMouseOver()
    {
        gameObject.layer = LayerMask.NameToLayer("Clickable Highlighted");
    }

    private void OnMouseExit()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void OnMouseDown()
    {
        if (objectCamera != null)
        {
            objectCamera.enabled = true;
            Invoke(nameof(SwitchBackToMainCamera), delayTime);
        }
    }
    
    private void SwitchBackToMainCamera()
    {
        objectCamera.enabled = false;
    }
}
