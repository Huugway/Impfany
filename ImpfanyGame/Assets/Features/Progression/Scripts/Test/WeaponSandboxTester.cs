using UnityEngine;

namespace Impfany.Weapons
{
    public sealed class WeaponSandboxTester : MonoBehaviour
    {
        [SerializeField]
        private Weapon weapon;

        [ContextMenu("Test/Attack")]
        private void TestAttack()
        {
            if (!Application.isPlaying || weapon == null)
            {
                return;
            }

            weapon.Attack();
        }

        [ContextMenu("Test/End Attack")]
        private void TestEndAttack()
        {
            if (!Application.isPlaying || weapon == null)
            {
                return;
            }

            weapon.EndAttack();
        }
    }
}
