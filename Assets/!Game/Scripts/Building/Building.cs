using UnityEngine;

public class Building : MonoBehaviour
{
    public virtual void Place()
    {
        PreviewMarker previewMarker = GetComponent<PreviewMarker>();
        MeshRenderer _renderer = GetComponent<MeshRenderer>();
        SelectebleObject grid = GetComponent<SelectebleObject>();
        SpaceCheck check = GetComponentInChildren<SpaceCheck>();
        if ((check != null) && (grid != null))
        {
            check.CanPlaceCheck();
            if (!check.canPlace) return;
            else
            {
                grid.ClearGrid();
                if (_renderer != null) _renderer.enabled = true;
                SetLayerRecursively(gameObject, 0);
            }
        }
        else return;
    }

    private void SetLayerRecursively(GameObject obj, int layer)
    {
        if (obj == null) return;
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}
