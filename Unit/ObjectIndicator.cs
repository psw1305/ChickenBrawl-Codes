using UnityEngine;
using CKB.Core;
using CKB.Utilities;

public class ObjectIndicator : GameStateMachineUser
{
    public GameObject Target { get; set; }

    [Range(0.5f, 0.9f)]
    [SerializeField] private float screenBoundOffset = 0.9f;
    [SerializeField] private GameObject indicator;

    private Camera mainCamera;
    private Vector3 screenCentre;
    private Vector3 screenBounds;

    private void Start()
    {
        indicator.SetActive(false);

        mainCamera = Camera.main;
        screenCentre = new Vector3(Screen.width, Screen.height, 0) / 2;
        screenBounds = screenCentre * screenBoundOffset;
    }

    private void LateUpdate()
    {
        if (Target == null) return;

        Vector3 screenPosition = CMath.GetScreenPosition(mainCamera, Target.transform.position);
        bool isTargetVisible = CMath.IsTargetVisible(screenPosition);

        if (isTargetVisible)
        {
            indicator.SetActive(false);
        }
        else
        {
            indicator.SetActive(true);
            float angle = float.MinValue;
            CMath.GetArrowIndicatorPositionAndAngle(ref screenPosition, ref angle, screenCentre, screenBounds);
            indicator.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        }

        screenPosition.z = 0;
        indicator.transform.position = screenPosition;
    }

    public float GetDistanceFromCamera(Vector3 cameraPosition)
    {
        float distanceFromCamera = Vector3.Distance(cameraPosition, transform.position);
        return distanceFromCamera;
    }
}
