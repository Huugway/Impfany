using UnityEngine;

namespace Impfany.Weapons
{
    public interface IEnemyDamageable
    {
        float ApplyHit(WeaponHit hit);
    }
}
