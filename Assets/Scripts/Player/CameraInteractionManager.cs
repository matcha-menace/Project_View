using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInteractionManager : MonoBehaviour
{
    [SerializeField] private GameObject pointerImage;
    
    private void Update()
    {
        CameraInteractionRay();
    }
    
    private void CameraInteractionRay()
    {
        var ray = transform.forward * 1000f;
        Debug.DrawRay(transform.position, ray, Color.red, 1f);
        if (Physics.Raycast(transform.position, ray, out RaycastHit hit, 1000f))
        {
            if (hit.collider.CompareTag("Clickable"))
            {
                pointerImage.SetActive(true);
            }
            else
            {
                pointerImage.SetActive(false);
            }
        }
    }
}
