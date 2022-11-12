using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [field:SerializeField] public Weapon CurrentWeapon { get; private set; }
    [field:SerializeField] public WeaponDamage WeaponDamage { get; private set; }
    [field:SerializeField] public WeaponHitbox WeaponSlot { get; private set; }

    public float GetDamage()
    {
        return WeaponDamage.GetDamage();
    }
}
