using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpaceCheck : MonoBehaviour
{
    [SerializeField] Vector3 _boxSize;
    [SerializeField] LayerMask _obstacleLayer;

    [SerializeField] private LayerMask ignoreLayerMask;
    [SerializeField] private BuildingConfig _buildingConfig;

    [SerializeField] private Color invalidColor = Color.red;

    public bool canPlace = true;

    private List<MeshRenderer> _renderers;
    private List<Color> _originalColors;

    // Ссылка на центральный collider, если найден
    private BoxCollider _centralCollider;

    private void Start()
    {
        // По умолчанию берем локальную масштабировку объекта (на случай, если центрального коллайдера нет)
        _boxSize = transform.localScale;
        CacheRenderersAndColors();
        TryGetCentralCollider();
    }

    private void TryGetCentralCollider()
    {
        if (_centralCollider != null) return;

        var select = GetComponentInParent<SelectebleObject>();
        if (select != null && select.CentralCollider != null)
            _centralCollider = select.CentralCollider;
    }

    private void CacheRenderersAndColors()
    {
        _renderers = new List<MeshRenderer>();
        _originalColors = new List<Color>();

        var building = GetComponentInParent<Building>();
        if (building != null)
            _renderers.AddRange(building.GetComponentsInChildren<MeshRenderer>(true));
        else
            _renderers.AddRange(GetComponentsInChildren<MeshRenderer>(true));

        foreach (var r in _renderers)
        {
            if (r != null && r.material != null)
                _originalColors.Add(r.material.color);
            else
                _originalColors.Add(Color.white);
        }
    }

    public void CanPlaceCheck()
    {
        canPlace = true;
        TryGetCentralCollider();

        Vector3 center;
        Vector3 halfExtents;
        Quaternion orientation;

        if (_centralCollider != null && _centralCollider.enabled)
        {
            // Размер в мировых координатах учитывая lossyScale
            Vector3 worldSize = Vector3.Scale(_centralCollider.size, _centralCollider.transform.lossyScale);
            halfExtents = worldSize / 2f;
            center = _centralCollider.transform.TransformPoint(_centralCollider.center);
            orientation = _centralCollider.transform.rotation;
        }
        else
        {
            center = transform.position;
            halfExtents = _boxSize / 2f;
            orientation = transform.rotation;
        }

        Collider[] colliders = Physics.OverlapBox(center, halfExtents, orientation, _obstacleLayer);

        if (colliders.Length > 0)
        {
            foreach (var col in colliders)
            {
                if ((ignoreLayerMask.value & (1 << col.gameObject.layer)) == 0)
                {
                    canPlace = false;
                    break;
                }
            }
        }

        if (!canPlace)

        {
            ApplyPreviewColor(invalidColor);
            for (int i = 0; i < colliders.Length; i++) Debug.LogErrorFormat($"Cathc a {colliders[i].name}");
        }
        else
        {
            RestoreOriginalColors();
            //_moneyInfo -= _buildingConfig.Price;
            PreviewMarker preview;
            if ((preview = GetComponentInChildren<PreviewMarker>()) != null)
            Destroy(preview.gameObject);
        }

    } 

    private void ApplyPreviewColor(Color color)
    {
        if (_renderers == null || _originalColors == null)
            CacheRenderersAndColors();

        for (int i = 0; i < _renderers.Count; i++)
        {
            var r = _renderers[i];
            if (r == null) continue;

            if (r.material != null)
            {
                r.material.color = color;
            }
        }
    }

    private void RestoreOriginalColors()
    {
        if (_renderers == null || _originalColors == null) return;

        for (int i = 0; i < _renderers.Count; i++)
        {
            var r = _renderers[i];
            if (r == null) continue;

            if (r.material != null && i < _originalColors.Count)
            {
                r.material.color = _originalColors[i];
            }
        }
    }

    private void OnDisable()
    {
        RestoreOriginalColors();
    }

    private void OnDestroy()
    {
        RestoreOriginalColors();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = canPlace ? new Color(0, 1, 0, 0.25f) : new Color(1, 0, 0, 0.25f);

        // Пытаемся получить центр/размер от центрального collider'а
        try
        {
            if (_centralCollider == null)
                TryGetCentralCollider();

            if (_centralCollider != null && _centralCollider.enabled)
            {
                Vector3 worldSize = Vector3.Scale(_centralCollider.size, _centralCollider.transform.lossyScale);
                Gizmos.matrix = Matrix4x4.TRS(_centralCollider.transform.TransformPoint(_centralCollider.center), _centralCollider.transform.rotation, Vector3.one);
                Gizmos.DrawCube(Vector3.zero, worldSize);
                return;
            }
        }
        catch { /* не критично для редактора */ }

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawCube(Vector3.zero, _boxSize);
    }
}