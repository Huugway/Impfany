using Impfany.Modifiers;
using UnityEngine;

// Item in der Welt: Läuft der Player rein, bekommt er dauerhaft einen Buff.
// Boots: Stat = MoveSpeed, Multiplier = 1.15 (+15 %)
// Braucht einen Collider2D mit "Is Trigger" an.
[RequireComponent(typeof(Collider2D))]
public class ItemBuff : MonoBehaviour
{
    [SerializeField] private Stat stat = Stat.MoveSpeed;
    [SerializeField, Min(0f)] private float multiplier = 1.15f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        IModifiable target = other.GetComponentInParent<IModifiable>();
        if (target == null)
        {
            Debug.LogError("Player hat kein StatModifiers-Script.", other);
            return;
        }

        // Quelle = dieses Item, nicht cleansable -> bleibt für immer und stapelt mit weiteren Items
        target.Apply(this, stat, multiplier, cleansable: false);
        Debug.Log($"Item eingesammelt: {stat} x{multiplier}");
        Destroy(gameObject);
    }
}
