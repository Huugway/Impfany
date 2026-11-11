using UnityEngine;

namespace Impfany.Weapons
{
    public sealed class WeaponDamageDummy : MonoBehaviour, IDamageable
    {
        public void TakeDamage(float amount)
        {
            Debug.Log($"Received {amount} damage.", this);
        }
    }
}