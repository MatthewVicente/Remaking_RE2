using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    AnimatorManager animatorManager;
    WeaponLoaderSlot weaponLoaderSlot;

    [Header("Current Equipment")]
    public WeaponItem weapon;
    //public SubWeaponItem sunWeapon; // Knife, Pistol, etc.

    private void Awake()
    {
        LoadWeaponLoaderSlots();
        animatorManager = GetComponent<AnimatorManager>();
    }

    private void Start()
    {
        LoadCurrentWeapon();
    }

    private void LoadWeaponLoaderSlots()
    {
        //BackSlot
        //HipSlot
        weaponLoaderSlot = GetComponentInChildren<WeaponLoaderSlot>();
    }

    private void LoadCurrentWeapon()
    {
        weaponLoaderSlot.LoadWeaponModel(weapon);
        // LOAD THE WEAPON ONTO OUR PLAYERS HAND
        animatorManager.animator.runtimeAnimatorController = weapon.weaponAnimator;
    }
}
