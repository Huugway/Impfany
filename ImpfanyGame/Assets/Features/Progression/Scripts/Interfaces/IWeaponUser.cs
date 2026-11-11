using UnityEngine;

namespace Impfany.Weapons
{
    public interface IWeaponUser
    {
        void ApplyWeaponPhase(
            object source,
            float movementSpeedMultiplier,
            bool lockAim
        );

        void RemoveWeaponEffects(object source);
    }
}
