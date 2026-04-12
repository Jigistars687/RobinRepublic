using UnityEngine;
using UnityEngine.UI;

public class CategoryButtons : MonoBehaviour
{
    [SerializeField] private Button _buildingsCategory;
    [SerializeField] private Button _artObjectsCategory;
    [SerializeField] private Button _natureCategory;
    [SerializeField] private Button _backButton;

    [SerializeField] private RectTransform _buildings;
    [SerializeField] private RectTransform _artObjects;
    [SerializeField] private RectTransform _nature;
    [SerializeField] private RectTransform _base;

    [SerializeField] private float _minusY;
    [SerializeField] private float _time;

    private RectTransform _lastCategory;

    private void Start()
    {
        SetActiveCategory(_base);
    }

    private void OnEnable()
    {
        _buildingsCategory.onClick.AddListener(() => SetActiveCategory(_buildings));
        _artObjectsCategory.onClick.AddListener(() => SetActiveCategory(_artObjects));
        _natureCategory.onClick.AddListener(() => SetActiveCategory(_nature));
        _backButton.onClick.AddListener(() => SetActiveCategory(_base));
    }

    private void OnDisable()
    {
        _buildingsCategory.onClick.RemoveAllListeners();
        _artObjectsCategory.onClick.RemoveAllListeners();
        _natureCategory.onClick.RemoveAllListeners();
        _backButton.onClick.RemoveAllListeners();
    }

    private void SetActiveCategory(RectTransform category)
    {
        if (_lastCategory == category) return;

        if (_lastCategory != null)
        {
            SetInactiveLastCategory(_lastCategory);
        }

        category.gameObject.SetActive(true);

        LeanTween.cancel(category.gameObject);
        LeanTween.moveY(category, 0, _time);

        _lastCategory = category;

        _backButton.gameObject.SetActive(category != _base);
    }

    private void SetInactiveLastCategory(RectTransform lastCategory)
    {
        LeanTween.cancel(lastCategory.gameObject);
        LeanTween.moveY(lastCategory, _minusY, _time)
            .setOnComplete(() => lastCategory.gameObject.SetActive(false));
    }
}