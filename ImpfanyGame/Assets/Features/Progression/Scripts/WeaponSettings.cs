using System;
using UnityEngine;

namespace Impfany.Weapons
{
    [Serializable]
    public sealed class WeaponPhaseSettings
    {
        [SerializeField, Min(0f)]
        private float duration = 0.2f;

        [SerializeField, Min(0f)]
        private float movementSpeedMultiplier = 1f;

        [SerializeField]
        private bool lockAim = false;

        public float Duration => duration;
        public float MovementSpeedMultiplier => movementSpeedMultiplier;
        public bool LockAim => lockAim;
    }

    [CreateAssetMenu(
        fileName = "NewWeapon",
        menuName = "Impfany/Weapons/Weapon Definition")]
    public sealed class WeaponSettings : ScriptableObject
    {
        [SerializeField]
        private string displayName = "New Weapon";

        [SerializeField, Min(0f)]
        private float damage = 10f;

        [SerializeField]
        private WeaponPhaseSettings charge = new WeaponPhaseSettings();

        [SerializeField]
        private WeaponPhaseSettings attack = new WeaponPhaseSettings();

        [SerializeField]
        private WeaponPhaseSettings recover = new WeaponPhaseSettings();

        public string DisplayName => displayName;
        public float Damage => damage;
        public WeaponPhaseSettings Charge => charge;
        public WeaponPhaseSettings Attack => attack;
        public WeaponPhaseSettings Recover => recover;
    }
}