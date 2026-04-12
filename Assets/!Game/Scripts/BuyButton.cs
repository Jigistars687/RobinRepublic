using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private BuildingConfig _buildingConfig;
    [SerializeField] private BuilderPlacer _builderPlacer;
    [SerializeField] private TMP_Text _moneyText;

    private void OnEnable()
    {
        _button.onClick.AddListener(TryBuy);
        EnsureWalletExists();
        PlayerWallet.Instance.OnMoneyChanged += OnMoneyChanged;
        UpdateMoneyText(PlayerWallet.Instance.Money);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(TryBuy);
        if (PlayerWallet.Instance != null)
            PlayerWallet.Instance.OnMoneyChanged -= OnMoneyChanged;
    }

    private void EnsureWalletExists()
    {
        if (PlayerWallet.Instance == null)
        {
            var go = new GameObject("PlayerWallet");
            go.AddComponent<PlayerWallet>();
        }
    }

    private void OnMoneyChanged(int newAmount)
    {
        UpdateMoneyText(newAmount);
    }

    private void UpdateMoneyText(int amount)
    {
        if (_moneyText != null)
            _moneyText.text = amount.ToString();
    }

    private void TryBuy()
    {
        if (_buildingConfig == null || _builderPlacer == null) return;

        int price = _buildingConfig.Price;
        if (!PlayerWallet.Instance.TrySpend(price))
        {
            return;
        }

        _builderPlacer.CreateBuilding(_buildingConfig, transform.position);
    }
}