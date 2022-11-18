using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Create New Weapon")]
public class WeaponConfig : ScriptableObject, IDamageModifier
{
    public WeaponClass weaponClass;
    public Weapon weaponPrefab;
    public float baseDamage;
    public float percentageBonusDamage;

    public IEnumerable<float> GetAdditiveModifiers()
    {
        yield return baseDamage;
    }

    public IEnumerable<float> GetPercentageModifiers()
    {
        yield return percentageBonusDamage;
    }
}

public enum WeaponClass
{
    Sword,
    Spear,
    Heavy
}

