using UnityEngine;

namespace Impfany.Weapons
{
    public abstract class WeaponVisual : MonoBehaviour
    {
        protected Weapon Weapon { get; private set; }

        protected virtual void Awake()
        {
            Weapon = GetComponentInParent<Weapon>();

            if (Weapon == null)
            {
                Debug.LogError("WeaponVisual needs a Weapon in its parents.", this);
                enabled = false;
                return;
            }

            Weapon.PhaseChanged += OnPhaseChanged;
        }

        protected virtual void OnDestroy()
        {
            if (Weapon != null)
            {
                Weapon.PhaseChanged -= OnPhaseChanged;
            }
        }

        protected abstract void OnPhaseChanged(WeaponPhase phase);
    }
}