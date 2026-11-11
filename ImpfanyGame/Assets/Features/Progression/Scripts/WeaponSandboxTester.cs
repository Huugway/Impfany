using UnityEngine;

namespace Impfany.Weapons
{
    public sealed class WeaponSandboxTester : MonoBehaviour
    {
        [SerializeField]
        private Weapon weapon;

        [ContextMenu("Test/Begin Attack")]
        private void TestBeginAttack()
        {
            if (!Application.isPlaying || weapon == null)
            {
                return;
            }

            weapon.BeginAttack();
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
