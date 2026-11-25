using UnityEngine;

namespace Impfany.Weapons
{
    public sealed class WeaponDamageDummy : MonoBehaviour, IEnemyDamageable
    {
        public float ApplyHit(WeaponHit hit)
        {
            Debug.Log($"Hit: {hit.Damage} dmg, knockback {hit.KnockbackDistance}, stun {hit.StunDuration}, dir {hit.Direction}", this);
            return hit.Damage;
        }
    }
}