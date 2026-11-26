using Impfany.Modifiers;
using UnityEngine;

// Item in der Welt: Läuft der Player rein, bekommt er dauerhaft einen Buff.
// Boots: Stat = MoveSpeed, Multiplier = 1.15 (+15 %)
// Herz:  Multiplier = 1, Max Health Bonus = 1 (+1 Herz)
// Braucht einen Collider2D mit "Is Trigger" an.
[RequireComponent(typeof(Collider2D))]
public class ItemBuff : MonoBehaviour
{
    [SerializeField] private Stat stat = Stat.MoveSpeed;
    [SerializeField, Min(0f)] private float multiplier = 1.15f;
    [Tooltip("Zusätzliche Max-HP (Herzen). 0 = kein HP-Bonus.")]
    [SerializeField, Min(0)] private int maxHealthBonus = 0;

    public event System.Action<ItemBuff> OnPickedUp;
    private bool pickedUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (pickedUp || !other.CompareTag("Player")) return;

        IModifiable target = other.GetComponentInParent<IModifiable>();
        if (target == null)
        {
            Debug.LogError("Player hat kein StatModifiers-Script.", other);
            return;
        }

        // Quelle = dieses Item, nicht cleansable -> bleibt für immer und stapelt mit weiteren Items
        // Multiplier 1 = kein Stat-Buff (z. B. reines HP-Item)
        if (!Mathf.Approximately(multiplier, 1f))
            target.Apply(this, stat, multiplier, cleansable: false);

        if (maxHealthBonus > 0)
        {
            PlayerHealth health = other.GetComponentInParent<PlayerHealth>();
            if (health != null) health.AddMaxHealth(maxHealthBonus);
        }

        Debug.Log($"Item eingesammelt: {stat} x{multiplier}, +{maxHealthBonus} Max-HP");
        pickedUp = true;
        OnPickedUp?.Invoke(this);
        Destroy(gameObject);
    }
}
