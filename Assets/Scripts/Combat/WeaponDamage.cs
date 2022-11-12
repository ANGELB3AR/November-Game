using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    private void Start()
    {
        print(GetDamage());
    }
    public float GetDamage()
    {
        return GetAdditiveModifiers() * (1 + GetPercentageModifiers() / 100);
    }

    float GetAdditiveModifiers()
    {
        float total = 0;
        foreach (IDamageModifier modifier in GetComponents<IDamageModifier>())
        {
            foreach (float damage in modifier.GetAdditiveModifiers())
            {
                total += damage;
            }
        }
        return total;
    }

    float GetPercentageModifiers()
    {
        float total = 0;
        foreach (IDamageModifier modifier in GetComponents<IDamageModifier>())
        {
            foreach (float damage in modifier.GetAdditiveModifiers())
            {
                total += damage;
            }
        }
        return total;
    }
}
