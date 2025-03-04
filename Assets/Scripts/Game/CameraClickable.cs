using UnityEngine;

public class CameraClickable : MonoBehaviour
{
    private Camera objectCamera;
    public float delayTime = 3f;

    private Camera[] allCameras;


    void Start()
    {
        objectCamera = GetComponentInChildren<Camera>();
        allCameras = Camera.allCameras;
    }

    void OnMouseDown()
    {
        if (objectCamera != null)
        {
            // Disable other cameras
            foreach (Camera cam in allCameras)
            {
                cam.enabled = false;
            }

            // Enable the clicked object's camera
            objectCamera.enabled = true;

            // set a n second timer to switch back
            Invoke("SwitchBackToMainCamera", delayTime);
        }
        else
        {
            Debug.LogWarning("No Camera found on " + gameObject.name);
        }
    }

    void SwitchBackToMainCamera(){
        Debug.Log("Switching back");
        objectCamera.enabled = false;
        allCameras[0].enabled = true;
    }
}