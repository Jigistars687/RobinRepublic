using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private BuildingConfig _buildingConfig;
    [SerializeField] private BuilderPlacer _builderPlacer;
    [SerializeField] int _moneyInfo = 3000;
    [SerializeField] private TMP_Text _moneyText;

    private void OnEnable()
    {
        _button.onClick.AddListener(TryBuy);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(TryBuy);
    }

    private void TryBuy()
    {
        if (_moneyInfo < _buildingConfig.Price) return;//2222222222222222222222222222222222222222222222222222222
        else
        {
            _builderPlacer.CreateBuilding(_buildingConfig, transform.position);
            _moneyInfo += _buildingConfig.Price;
            _moneyText.text = _moneyInfo.ToString();
            Debug.Log(_moneyInfo);  
        }
    }
}
