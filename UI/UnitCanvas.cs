using UnityEngine;

public class UnitCanvas : MonoBehaviour
{
    private Camera currentCamera;
    
    private void Awake()
    {
        currentCamera = Camera.main;
    }
    
    private void LateUpdate()
    {
        transform.forward = currentCamera.transform.forward;
    }
}
