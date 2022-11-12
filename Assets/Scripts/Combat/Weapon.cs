using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IDamageModifier
{
    [SerializeField] WeaponConfig weaponSO;

    float baseDamage;
    float percentageBonusDamage;

    private void Awake()
    {
        baseDamage = weaponSO.baseDamage;
        percentageBonusDamage = weaponSO.percentageBonusDamage;
    }

    public IEnumerable<float> GetAdditiveModifiers()
    {
        yield return baseDamage;
    }

    public IEnumerable<float> GetPercentageModifiers()
    {
        yield return percentageBonusDamage;
    }
}
