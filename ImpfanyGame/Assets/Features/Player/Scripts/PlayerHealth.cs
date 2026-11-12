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
        PublishHealth();
        if (CurrentHealth == 0) OnDeath?.Invoke();
    }
    public void Heal(int heal = 1)
    {
        if (heal <= 0) return;
        if (CurrentHealth <= 0) return;
        CurrentHealth += heal;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        PublishHealth();
    }
    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void PublishHealth()
    {
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }
}
