using UnityEngine;

namespace Impfany.Weapons
{
    public abstract class WeaponAttackBehaviour : MonoBehaviour
    {
        public abstract void BeginAttack(WeaponSettings definition);

        public abstract void EndAttack();
    }
}