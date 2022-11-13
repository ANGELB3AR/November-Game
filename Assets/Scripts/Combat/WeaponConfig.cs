using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Create New Weapon")]
public class WeaponConfig : ScriptableObject
{
    public WeaponClass weaponClass;
    public GameObject weaponPrefab;
    public float baseDamage;
    public float percentageBonusDamage;
}

public enum WeaponClass
{
    Sword,
    Spear,
    Heavy
}

