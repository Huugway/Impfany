using UnityEngine;

namespace Impfany.Weapons
{
    [DisallowMultipleComponent]
    public sealed class Weapon : MonoBehaviour
    {
        [SerializeField]
        private WeaponSettings settings;

        [SerializeField]
        private WeaponAttackBehaviour attackBehaviour;

        private bool isAttacking;

        public WeaponSettings Settings => settings;

        public void BeginAttack()
        {
            if (isAttacking || !isActiveAndEnabled)
            {
                return;
            }

            if (settings == null || attackBehaviour == null)
            {
                Debug.LogError(
                    "Weapon needs a definition and an attack behaviour.",
                    this);
                return;
            }

            isAttacking = true;
            attackBehaviour.BeginAttack(settings);
        }

        public void EndAttack()
        {
            if (!isAttacking)
            {
                return;
            }

            isAttacking = false;

            if (attackBehaviour != null)
            {
                 attackBehaviour.EndAttack();
            }
        }

        private void OnDisable()
        {
            EndAttack();
        }
    }
}