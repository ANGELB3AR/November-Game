using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [field:SerializeField] public WeaponConfig CurrentWeapon { get; private set; }
    [field:SerializeField] public Weapon EquippedPrefab { get; private set; }

    [SerializeField] Transform weaponTransform;

    void Start()
    {
        EquipWeapon(CurrentWeapon);
    }

    public void EquipWeapon(WeaponConfig weapon)
    {
        Instantiate(CurrentWeapon.weaponPrefab, weaponTransform);
        EquippedPrefab = GetComponentInChildren<Weapon>();
    }

    public void EnableWeaponColliders()
    {
        EquippedPrefab.EnableHitbox();
    }

    public void DisableWeaponColliders()
    {
        EquippedPrefab.DisableHitbox();
    }
}
