using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    //[SerializeField] Collider myCollider;

    //float damage;

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other == myCollider) { return; }

    //    if (other.TryGetComponent<Health>(out Health health))
    //    {
    //        health.DealDamage(damage);
    //    }
    //}

    //float GetAdditiveModifiers()
    //{
    //    float total = 0;
    //    foreach (IDamageModifier modifier in GetComponents<IDamageModifier>())
    //    {
    //        foreach (float damage in modifier.GetAdditiveModifiers())
    //        {
    //            total += damage;
    //        }
    //    }
    //    return total;
    //}

    //float GetPercentageModifiers()
    //{
    //    float total = 0;
    //    foreach (IDamageModifier modifier in GetComponents<IDamageModifier>())
    //    {
    //        foreach (float damage in modifier.GetAdditiveModifiers())
    //        {
    //            total += damage;
    //        }
    //    }
    //    return total;
    //}

    //public float GetDamage()
    //{
    //    return GetAdditiveModifiers() * (1 + GetPercentageModifiers() / 100);
    //}
}
