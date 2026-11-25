using UnityEngine;

namespace Impfany.Weapons
{
    public readonly struct WeaponHit
    {
        public readonly float Damage;
        public readonly float KnockbackDistance;
        public readonly float StunDuration;
        public readonly Vector2 Direction;

        public WeaponHit(float damage, float knockbackDistance, float stunDuration, Vector2 direction)
        {
            Damage = damage;
            KnockbackDistance = knockbackDistance;
            StunDuration = stunDuration;
            Direction = direction;
        }
    }
}
