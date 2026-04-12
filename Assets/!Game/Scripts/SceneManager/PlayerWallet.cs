using System;
using TMPro;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public static PlayerWallet Instance { get; private set; }

    [SerializeField] private int _startingMoney = 3000;
    [SerializeField] private TMP_Text _moneyText;
    private int _money;

    public int Money
    {
        get => _money;
        private set
        {
            _money = value;
            OnMoneyChanged?.Invoke(_money);
        }
    }

    public event Action<int> OnMoneyChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Money = _startingMoney;
        _moneyText.text = Money.ToString();
    }
    public bool TrySpend(int amount)
    {
        if (amount <= 0) return true;
        if (Money < amount) return false;
        Money -= amount;
        _moneyText.text = Money.ToString();
        return true;
    }

    public void Add(int amount)
    {
        if (amount <= 0) return;
        Money += amount;
        _moneyText.text = Money.ToString();
    }
    public void Set(int amount)
    {
        Money = amount;
        _moneyText.text = Money.ToString();
    }
}