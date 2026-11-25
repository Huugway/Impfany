using Impfany.Weapons;
using Unity.VisualScripting;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{

    private EnemyMovement movement;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (movement != null && movement.IsStunned) return;
        
        if (collision.gameObject.TryGetComponent(out IPlayerDamageable target))
        {
            target.TakeDamage(1);
        }
    }

    void Update()
    {
        
    }
}
