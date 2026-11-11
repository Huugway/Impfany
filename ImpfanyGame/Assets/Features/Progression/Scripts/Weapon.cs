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
        private float phaseElapsed;
        public WeaponSettings Settings => settings;
        public WeaponPhase CurrentPhase { get; private set; } = WeaponPhase.Idle;


        private void Update()
        {
            if (CurrentPhase == WeaponPhase.Idle)
            {
                return;
            }

            phaseElapsed += Time.deltaTime;

            if (CurrentPhase ==  WeaponPhase.Charge && phaseElapsed >= settings.Charge.Duration)
            {
                EnterPhase(WeaponPhase.Attack);
                attackBehaviour.BeginAttack(settings);
            }

            if (CurrentPhase == WeaponPhase.Attack && phaseElapsed >= settings.Attack.Duration)
            {
                attackBehaviour.EndAttack();
                EnterPhase(WeaponPhase.Recover);
            }

            if (CurrentPhase == WeaponPhase.Recover && phaseElapsed >= settings.Recover.Duration)
            {
                EndAttack();
            }
        }

        private void EnterPhase(WeaponPhase phase)
        {
            CurrentPhase = phase;
            phaseElapsed = 0f;
        }

        public void Attack()
        {
            if (CurrentPhase != WeaponPhase.Idle || !isActiveAndEnabled)
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

            EnterPhase(WeaponPhase.Charge);
        }

        public void EndAttack()
        {
            if (CurrentPhase == WeaponPhase.Idle)
            {
                return;
            }


            if (CurrentPhase == WeaponPhase.Attack && attackBehaviour != null)
            {
                 attackBehaviour.EndAttack();
            }

            EnterPhase(WeaponPhase.Idle);
        }

        private void OnDisable()
        {
            EndAttack();
        }
    }
}
