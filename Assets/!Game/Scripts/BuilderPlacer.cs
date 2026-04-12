using TMPro;
using UnityEngine;


public class BuilderPlacer : MonoBehaviour
{
    [SerializeField] private Camera _raycastCamera;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private FieldConfig _fieldConfig;


    private Building _currentBuilding = null;

    private void Start()
    {
        if (_raycastCamera == null)
            _raycastCamera = Camera.main;
    }

    private void Update()
    {
        if (_currentBuilding == null) return;

        Ray ray = _raycastCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, _groundMask))
        {
            Vector3 pos = hit.point;

            pos.x = Mathf.Round(pos.x / _fieldConfig.CellSize) * _fieldConfig.CellSize;
            pos.z = Mathf.Round(pos.z / _fieldConfig.CellSize) * _fieldConfig.CellSize;
            pos.y = 0.10f;
            _currentBuilding.transform.position = pos;

            if (Input.GetMouseButtonDown(0))
            {
                SpaceCheck check = _currentBuilding.GetComponentInChildren<SpaceCheck>();
                if (check != null)
                {
                    check.CanPlaceCheck();
                    if (!check.canPlace)
                    {
                    }
                    else
                    {
                        _currentBuilding.Place();
                        _currentBuilding = null;
                    }
                }
                else
                {
                    _currentBuilding.Place();
                    _currentBuilding = null;
                }
            }
        }
    }

    public void CreateBuilding(BuildingConfig buildingConfig, Vector3 startPos)
    {
        GameObject newBuilding = Instantiate(buildingConfig.Prefab, startPos, Quaternion.identity);
        _currentBuilding = newBuilding.GetComponent<Building>();
    }
}