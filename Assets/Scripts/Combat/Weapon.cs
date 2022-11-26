using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CapsuleCollider))]
public class Weapon : MonoBehaviour, IDamageModifier
{
    float baseDamage;
    float percentageBonusDamage;
    float knockback;

    [SerializeField] CapsuleCollider hitbox;
    [SerializeField] Collider myCollider;
    [SerializeField] TrailRenderer trail;
    [SerializeField] TimeManipulator time = null;
    [SerializeField] ParticleSystem bloodSplatter;
    
    DamageCounter damageCounter;

    void Start()
    {
        damageCounter = GetComponentInParent<DamageCounter>();
        hitbox = GetComponent<CapsuleCollider>();
        myCollider = GetComponentInParent<CharacterController>();
        time = GetComponentInParent<TimeManipulator>();

        DisableHitbox();
    }

    public void SetDamageStats(float baseDamage, float percentageBonusDamage)
    {
        this.baseDamage = baseDamage;
        this.percentageBonusDamage = percentageBonusDamage;
    }

    public void SetKnockback(float knockback)
    {
        this.knockback = knockback;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) { return; }

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damageCounter.GetDamage());

            bloodSplatter.Play();

            if (time != null)
            {
                time.SlowTime();
            }
        }

        if (other.TryGetComponent<KnockbackReceiver>(out KnockbackReceiver knockbackReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            knockbackReceiver.ReceiveKnockback(direction * knockback);
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
