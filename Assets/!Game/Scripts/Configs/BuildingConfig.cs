using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Game/Building Config")]
public class BuildingConfig : ScriptableObject
{
    public int Price;
    public string Description;
    public string Category;

    public Image Icon;

    public GameObject Prefab;
    public GameObject PreviewPrefab;
}