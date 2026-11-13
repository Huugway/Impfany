using System;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] private int coins;
    public event Action<int> OnCoinsChanged;

    internal void AddCoin()
    {
        coins++;
        PublishCoins();
    }

    public void PublishCoins()
    {
        OnCoinsChanged?.Invoke(coins);
    }

    public bool TrySpend(int amount)
    {
        if (amount <= 0 || coins < amount) return false;
        coins -= amount;
        PublishCoins();
        return true;
    }
}
