using UnityEngine;
using UnityEngine.InputSystem;

// Placeholder: mit E 10 Münzen ausgeben, um 1 HP zu heilen.
[RequireComponent(typeof(PlayerWallet), typeof(PlayerHealth))]
public class PlayerCoinHeal : MonoBehaviour
{
    [SerializeField] private int coinCost = 10;
    [SerializeField] private int healAmount = 1;

    private PlayerWallet wallet;
    private PlayerHealth health;

    private void Awake()
    {
        wallet = GetComponent<PlayerWallet>();
        health = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        // Placeholder-Input: später durch eine InputActionReference ersetzen
        if (Keyboard.current == null || !Keyboard.current.eKey.wasPressedThisFrame) return;
        if (health.CurrentHealth >= health.MaxHealth) return;
        if (!wallet.TrySpend(coinCost)) return;

        health.Heal(healAmount);
    }
}