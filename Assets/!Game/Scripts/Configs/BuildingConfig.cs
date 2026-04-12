using UnityEngine;

[CreateAssetMenu(menuName = "Game/Building Config")]
public class BuildingConfig : ScriptableObject
{
    public int Price;
    public GameObject Prefab;
    public GameObject PreviewPrefab;
}