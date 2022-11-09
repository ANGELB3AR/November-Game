using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] Collider myCollider;

    float damage;

    void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) { return; }

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
        }
    }

    public void SetAttack(float damage)
    {
        this.damage = damage;
    }
}
