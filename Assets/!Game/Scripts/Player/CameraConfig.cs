using UnityEngine;

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
