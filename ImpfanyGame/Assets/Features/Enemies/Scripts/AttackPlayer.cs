using Impfany.Weapons;
using Unity.VisualScripting;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IPlayerDamageable target))
        {
            target.TakeDamage(1);
        }
    }

    void Update()
    {
        
    }
}
