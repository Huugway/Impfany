using UnityEngine;

namespace Impfany.Weapons
{
    public sealed class WeaponSandboxUser : MonoBehaviour, IWeaponUser
    {
        private struct PhaseEffects
        {
            public float MovementSpeedMultiplier;
            public bool LockAim;
        }
        public void ApplyWeaponPhase(
            object source, float movementSpeedMultiplier, bool lockAim)
        {
        }

        public void RemoveWeaponEffects(object source)
        {
        }
    }
}