using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCounter : MonoBehaviour
{
    public float GetDamage()
    {
        return GetAdditiveModifiers() * (1 + GetPercentageModifiers() / 100);
    }

    float GetAdditiveModifiers()
    {
        float total = 0;
        foreach (IDamageModifier modifier in GetComponentsInChildren<IDamageModifier>())
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
        foreach (IDamageModifier modifier in GetComponentsInChildren<IDamageModifier>())
        {
            foreach (float damage in modifier.GetAdditiveModifiers())
            {
                total += damage;
            }
        }

        return total;
    }
}
