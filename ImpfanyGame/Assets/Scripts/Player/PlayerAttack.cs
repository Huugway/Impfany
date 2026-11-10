using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform[] weaponSlots;
    [SerializeField] private int attackPhase;
    private GameObject curentWeapon;


    private GameObject GetWeapon(int slotIndex)
    {
        Transform slot = weaponSlots[slotIndex];
        return slot.childCount > 0 ? slot.GetChild(0).gameObject : null;
    }

    private void GetAllWeapons()
    {
        for (int slot = 0; slot < 2; slot++)
        {
            GetWeapon(slot);
        }
    }

    void Awake()
    {
        GetAllWeapons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Attack()
    {
        var weapon = curentWeapon;
        switch (attackPhase)
        {
            //case 0: GetWeapon(attackPhase).attack();
            //case 1: GetWeapon(attackPhase).attack();
            //case 2: GetWeapon(attackPhase).attack();
        }
    }
}
