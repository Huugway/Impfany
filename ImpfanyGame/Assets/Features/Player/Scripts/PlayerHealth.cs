using Impfany.Weapons;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IPlayerDamageable
{
    [SerializeField] private int maxHealth = 6;
    public int MaxHealth => maxHealth;
    public int CurrentHealth { get; private set; }
    public event System.Action OnDeath;
    public event System.Action<int, int> OnHealthChanged;

    
    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;
        if (CurrentHealth <= 0) return;
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        PublishHealth();
        if (CurrentHealth == 0) Die();
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
        PublishHealth();
    }
    private void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
    public void PublishHealth()
    {
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }
}
