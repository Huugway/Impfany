using Impfany.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private Transform[] weaponSlots;
    [SerializeField] private int attackPhase;

    void Update()
    {
        if (attackAction.action.WasPressedThisFrame()) Attack();
    }

    private void Attack()
    {
        var weaponPrefab = GetWeapon(attackPhase);
        if (weaponPrefab == null) Debug.LogError($"Weapon wasn't found in Slot {attackPhase + 1}", this);
        else 
        {
            var weapon = weaponPrefab.GetComponent<Weapon>();
            if (weapon == null) Debug.LogError($"Object in Weapon Slot {attackPhase + 1} does not hold a weapon script", this);
            else weapon.Attack();
        }
        ProgressWeaponChain();
    }

    private GameObject GetWeapon(int slotIndex)
    {
        Transform slot = weaponSlots[slotIndex];
        return slot.childCount > 0 ? slot.GetChild(0).gameObject : null;
    }

    private void ProgressWeaponChain()
    {
        attackPhase++;
        if (attackPhase == weaponSlots.Length) attackPhase = 0;
    }

    private void OnEnable()
    {
        attackAction.action.Enable();
    }
}
