using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 6;
    public int MaxHealth => maxHealth;
    public int CurrentHealth { get; private set; }
    public event System.Action OnDeath;
    public event System.Action<int, int> OnHealthChanged;

    public void TakeDamage(int damage = 1)
    {
        if (damage <= 0) return;
        if (CurrentHealth <= 0) return;
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        if (CurrentHealth == 0) OnDeath?.Invoke();
    }
    public void Heal(int heal = 1)
    {
        if (heal <= 0) return;
        if (CurrentHealth <= 0) return;
        CurrentHealth += heal;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }
    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }
}
