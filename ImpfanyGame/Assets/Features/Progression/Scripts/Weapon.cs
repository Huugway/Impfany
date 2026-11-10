using UnityEngine;

namespace Impfany.Weapons
{
    [DisallowMultipleComponent]
    public sealed class Weapon : MonoBehaviour
    {
        [SerializeField]
        private WeaponDefinition definition;

        public WeaponDefinition Definition => definition;
    }
}