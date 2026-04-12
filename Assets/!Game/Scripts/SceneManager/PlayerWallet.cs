using System;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public static PlayerWallet Instance { get; private set; }

    [SerializeField] private int _startingMoney = 3000;
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
    }

    // Попытаться списать сумму — возвращает true если удалось
    public bool TrySpend(int amount)
    {
        if (amount <= 0) return true;
        if (Money < amount) return false;
        Money -= amount;
        return true;
    }

    public void Add(int amount)
    {
        if (amount <= 0) return;
        Money += amount;
    }

    // Принудительная установка (например для отладки)
    public void Set(int amount)
    {
        Money = amount;
    }
}