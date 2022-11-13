using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [field:SerializeField] public WeaponConfig CurrentWeapon { get; private set; }
    [field:SerializeField] public WeaponDamage WeaponDamage { get; private set; }
    [field:SerializeField] public WeaponHitbox WeaponSlot { get; private set; }
    [SerializeField] Transform swordTransform;
    [SerializeField] Transform spearTransform;
    [SerializeField] Transform heavyTransform;


    void Start()
    {
        EquipWeapon(CurrentWeapon);
    }

    void EquipWeapon(WeaponConfig weapon)
    {
        Instantiate(weapon.weaponPrefab, SelectWeaponTransform(weapon));
    }

    Transform SelectWeaponTransform(WeaponConfig weapon)
    {
        switch(weapon.weaponClass)
        {
            case WeaponClass.Sword:
                return swordTransform;
            case WeaponClass.Spear:
                return spearTransform;
            case WeaponClass.Heavy:
                return heavyTransform;
            default:
                return null;
        }
    }
}
