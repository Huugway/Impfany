using UnityEngine;

namespace Impfany.Weapons
{
    public sealed class SwingAttackBehaviour : WeaponAttackBehaviour
    {
        [SerializeField]
        private Transform visual;
        [SerializeField, Range(0f, 360f)]
        private float swingAngle = 120f;
        private Quaternion restingRotation;

        private void Awake()
        {
            if (visual != null)
            {
                restingRotation = visual.localRotation;
            }
        }

        public override void BeginAttack(WeaponSettings definition)
        {
            if (visual == null)
            {
                return;
            }

            visual.localRotation = restingRotation * Quaternion.Euler(0f, 0f, swingAngle * 0.5f);
        }

        public override void EndAttack()
        {
            if (visual != null)
            {
                visual.localRotation = restingRotation;
            }
        }
    }
}
