using UnityEngine;

namespace Impfany.Weapons
{
    public sealed class WeaponDamageDummy : MonoBehaviour, IEnemyDamageable
    {
        public void TakeDamage(float amount)
        {
            Debug.Log($"Received {amount} damage.", this);
        }
    }
}