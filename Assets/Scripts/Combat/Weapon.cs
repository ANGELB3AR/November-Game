using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CapsuleCollider))]
public class Weapon : MonoBehaviour, IDamageModifier
{
    float baseDamage;
    float percentageBonusDamage;

    [SerializeField] CapsuleCollider hitbox;
    [SerializeField] Collider myCollider;
    [SerializeField] TrailRenderer trail;
    
    DamageCounter damageCounter;

    void Start()
    {
        damageCounter = GetComponentInParent<DamageCounter>();
        hitbox = GetComponent<CapsuleCollider>();
        myCollider = GetComponentInParent<CharacterController>();

        DisableHitbox();
    }

    public void SetDamageStats(float baseDamage, float percentageBonusDamage)
    {
        this.baseDamage = baseDamage;
        this.percentageBonusDamage = percentageBonusDamage;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) { return; }

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damageCounter.GetDamage());
        }
    }

    public IEnumerable<float> GetAdditiveModifiers()
    {
        yield return baseDamage;
    }

    public IEnumerable<float> GetPercentageModifiers()
    {
        yield return percentageBonusDamage;
    }

    public void ActivateWeaponTrail(bool status)
    {
        trail.emitting = status;
    }


    // Called by Animation Events
    public void EnableHitbox()
    {
        hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }
}
