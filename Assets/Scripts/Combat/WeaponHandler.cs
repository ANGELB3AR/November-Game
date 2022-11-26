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
        EquippedPrefab.SetDamageStats(CurrentWeapon.baseDamage, CurrentWeapon.percentageBonusDamage);
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
