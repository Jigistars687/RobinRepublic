using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuilderPlacer : MonoBehaviour
{
    [SerializeField] private Camera _raycastCamera;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private FieldConfig _fieldConfig;


    [SerializeField] private Button _undoButton;

    private Building _currentBuilding = null;
    private int _currentBuildingPrice = 0;

    private CommandHistory _commandHistory = new CommandHistory();
    void OnEnable()
    {
        _undoButton.onClick.AddListener(HandleUndo);
    }
    private void OnDisable()
    {
        _undoButton.onClick.RemoveAllListeners();
    }
    private void Start()
    {
        if (_raycastCamera == null)
            _raycastCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            HandleUndo();
        }

        if (_currentBuilding == null) return;

        Ray ray = _raycastCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, _groundMask))
        {
            Vector3 pos = hit.point;

            pos.x = Mathf.Round(pos.x / _fieldConfig.CellSize) * _fieldConfig.CellSize;
            pos.z = Mathf.Round(pos.z / _fieldConfig.CellSize) * _fieldConfig.CellSize;
            pos.y = 0.03f;
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
                        ConfirmPlacement();
                    }
                }
                else
                {
                    ConfirmPlacement();
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateBuilding(_currentBuilding.transform);
            }
        }
    }

    private void ConfirmPlacement()
    {
        ICommand placeCommand = new PlaceBuildingCommand(_currentBuilding, _currentBuildingPrice);
        _commandHistory.ExecuteCommand(placeCommand);

        _currentBuilding = null;
        _currentBuildingPrice = 0;
    }

    private void HandleUndo()
    {
        if (_currentBuilding != null)
        {
            Destroy(_currentBuilding.gameObject);
            _currentBuilding = null;
            PlayerWallet.Instance.Add(_currentBuildingPrice);
            _currentBuildingPrice = 0;
        }
        else
        {
            _commandHistory.Undo();
        }
    }

    public void CreateBuilding(BuildingConfig buildingConfig, Vector3 startPos)
    {
        if (_currentBuilding != null)
        {
            Destroy(_currentBuilding.gameObject);
            PlayerWallet.Instance.Add(_currentBuildingPrice);
        }

        GameObject newBuilding = Instantiate(buildingConfig.Prefab, startPos, Quaternion.identity);
        _currentBuilding = newBuilding.GetComponent<Building>();
        _currentBuildingPrice = buildingConfig.Price;
    }

    private void RotateBuilding(Transform buildingTransform)
    {
        if (LeanTween.isTweening(buildingTransform.gameObject))
            return;
        Vector3 targetRotation = buildingTransform.eulerAngles + new Vector3(0, 90, 0);
        LeanTween.rotate(buildingTransform.gameObject, targetRotation, 0.2f)
                 .setEase(LeanTweenType.easeInOutSine);
    }
}