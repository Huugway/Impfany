using UnityEngine;

namespace Impfany.Weapons
{
    public sealed class WeaponDebugAttackBehaviour : WeaponAttackBehaviour
    {
        public override void BeginAttack(WeaponSettings settings)
        {
            Debug.Log("attack started", this);
        }

        public override void EndAttack()
        {
            Debug.Log("Attack ended", this);
        }
    }
}
