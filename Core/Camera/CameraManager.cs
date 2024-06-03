using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject cameraTarget;
    [SerializeField] private Vector3 offset;

    public void SetTarget(GameObject target)
    {
        cameraTarget = target;
    }

    private void LateUpdate()
    {
        if (cameraTarget == null) return;

        transform.position = cameraTarget.transform.position + offset;
    }
}
