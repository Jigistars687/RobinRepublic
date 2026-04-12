using UnityEngine;

[RequireComponent(typeof(Camera))]
public sealed class CameraController : MonoBehaviour
{
    [SerializeField] private CameraConfig config;

    private Transform _cameraTransform;

    private void Awake()
    {
        _cameraTransform = transform;
    }

    private void LateUpdate()
    {
        UpdateCameraTransform();
    }

    private void UpdateCameraTransform()
    {
        Vector3 targetPosition = _cameraTransform.position;

        targetPosition = ProcessPan(targetPosition);
        targetPosition = ProcessZoom(targetPosition);

        _cameraTransform.position = ClampPosition(targetPosition);
    }

    private Vector3 ProcessPan(Vector3 position)
    {
        if (!Input.GetMouseButton(1))
            return position;

        float deltaX = -Input.GetAxis("Mouse X") * config.PanSpeed * Time.deltaTime;
        float deltaZ = Input.GetAxis("Mouse Y") * config.PanSpeed * Time.deltaTime;

        position += Vector3.right * deltaX;
        position -= Vector3.forward * deltaZ;

        return position;
    }

    private Vector3 ProcessZoom(Vector3 position)
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Approximately(scrollInput, 0f))
            return position;

        position -= Vector3.up * (scrollInput * config.ZoomSpeed * Time.deltaTime);

        return position;
    }

    private Vector3 ClampPosition(Vector3 position)
    {
        position.x = Mathf.Clamp(position.x, config.MinX, config.MaxX);
        position.y = Mathf.Clamp(position.y, config.MinY, config.MaxY);
        position.z = Mathf.Clamp(position.z, config.MinZ, config.MaxZ);

        return position;
    }
}
