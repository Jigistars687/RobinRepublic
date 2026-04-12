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

        position.x += deltaX;
        position.y += deltaZ;

        return position;
    }

    private Vector3 ProcessZoom(Vector3 position)
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Approximately(scrollInput, 0f))
            return position;

        position.z -= scrollInput * config.ZoomSpeed * Time.deltaTime;

        return position;
    }

    private Vector3 ClampPosition(Vector3 position)
    {
        position.x = Mathf.Clamp(position.x, config.MinX, config.MaxX);
        position.z = Mathf.Clamp(position.y, config.MinY, config.MaxY);
        position.y = Mathf.Clamp(position.z, config.MinZ, config.MaxZ);

        return position;
    }
}

[System.Serializable]
public class CameraConfig
{
    [SerializeField] private float panSpeed = 20f;
    [SerializeField] private float zoomSpeed = 250f;

    [SerializeField] private float minX = -50f;
    [SerializeField] private float maxX = 50f;
    [SerializeField] private float minZ = -50f;
    [SerializeField] private float maxZ = 50f;

    [SerializeField] private float minY = 5f;
    [SerializeField] private float maxY = 40f;

    public float PanSpeed => panSpeed;
    public float ZoomSpeed => zoomSpeed;
    public float MinX => minX;
    public float MaxX => maxX;
    public float MinZ => minZ;
    public float MaxZ => maxZ;
    public float MinY => minY;
    public float MaxY => maxY;
}