using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [field:SerializeField] public WeaponConfig CurrentWeapon { get; private set; }
    [field:SerializeField] public Weapon EquippedPrefab { get; private set; }

    [SerializeField] Transform weaponTransform;

    internal void EquipWeapon(UnityEngine.Object @object)
    {
        throw new NotImplementedException();
    }

    void Start()
    {
        EquipWeapon(CurrentWeapon);
    }

    public void EquipWeapon(WeaponConfig weapon)
    {
        CurrentWeapon = weapon;
        Instantiate(CurrentWeapon.weaponPrefab, weaponTransform);
        EquippedPrefab = GetComponentInChildren<Weapon>();
        EquippedPrefab.SetDamageStats(CurrentWeapon.baseDamage, CurrentWeapon.percentageBonusDamage);
    }

    // Not currently being called
    public void UnequipWeapon()
    {
        CurrentWeapon = null;
        DropWeapon();
        EquippedPrefab = null;
    }

    public void EnableWeaponColliders()
    {
        EquippedPrefab.EnableHitbox();
    }

    public void DisableWeaponColliders()
    {
        EquippedPrefab.DisableHitbox();
    }

    public void DropWeapon()
    {
        Rigidbody rb = EquippedPrefab.GetComponent<Rigidbody>();

        rb.isKinematic = false;
        rb.useGravity = true;
        EquippedPrefab.transform.parent = null;
    }

    public void ActivateWeaponTrail(bool status)
    {
        EquippedPrefab.ActivateWeaponTrail(status);
    }
}
