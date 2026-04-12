using JetBrains.Annotations;
using System.Linq;
using UnityEngine;

public class SelectebleObject : MonoBehaviour
{
    [SerializeField] private FieldConfig _fieldConfig;
    [SerializeField] private GameObject _gridTexture;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private GameObject _parentObject; [Tooltip("родительский объект на случай отстувия мешрендерера")]

    public int XSize = 2;
    public int ZSize = 2;

    private GameObject[] _gridCells;
    private GameObject _central;
    public bool _isVisible = true;

    private BoxCollider _centralCollider;
    public BoxCollider CentralCollider => _centralCollider;

    public void GridCells()
    {
        if (_meshRenderer == null) 
            _parentObject.SetActive(false);
        else 
            _meshRenderer.enabled = false;

        if (_fieldConfig == null) return;

        int targetNum = XSize * ZSize;
        _gridCells = new GameObject[targetNum];

        Vector3 startOffset = new Vector3((XSize - 1) * 0.5f, 0, (ZSize - 1) * 0.5f) * _fieldConfig.CellSize;

        int index = 0;
        for (int x = 0; x < XSize; x++)
        {
            for (int z = 0; z < ZSize; z++)
            {
                Vector3 offset = new Vector3(x, 0, z) * _fieldConfig.CellSize - startOffset;
                GameObject cell = Instantiate(_gridTexture, transform.position + offset, Quaternion.identity, transform);
                cell.transform.localScale = new Vector3(1, 1, 1) * _fieldConfig.CellSize;
                var existingCollider = cell.GetComponent<Collider>();
                if (existingCollider != null) Destroy(existingCollider);

                _gridCells[index] = cell;
                index++;
                if (!_isVisible) Destroy(cell);
            }
        }

        if (_centralCollider == null)
        {
            _central = new GameObject("GridCentralCollider");
            _central.transform.SetParent(transform, false);
            _central.transform.localPosition = Vector3.zero;
            _central.transform.localRotation = Quaternion.identity;
            _central.layer = 7;

            _centralCollider = _central.AddComponent<BoxCollider>();
            _centralCollider.size = new Vector3(XSize * _fieldConfig.CellSize, _fieldConfig.CellSize, ZSize * _fieldConfig.CellSize);
            _centralCollider.center = new Vector3(0f, _fieldConfig.CellSize * 0.5f, 0f);
            _centralCollider.isTrigger = false;
        }
    }

    public void ClearGrid()
    {
        if (_gridCells == null) return;

        foreach (var cell in _gridCells)
        {
            if (cell != null)
            {
                MeshRenderer cellRenderer = cell.GetComponent<MeshRenderer>();
                if (cellRenderer != null)
                {
                    cellRenderer.enabled = false;
                    Debug.Log($"MeshRenderer отключен для объекта: {cell.name}");
                }
                cell.layer = 0;
            }
        }

        if (_meshRenderer == null) 
            _parentObject.SetActive(true);
        else 
            _meshRenderer.enabled = true;

        if (_central != null)
        {
            _central.layer = 0;
            Debug.Log("Центральный объект обработан.");
        }
    }

    private void Start()
    {
        GridCells();
    }
}
