using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CapsuleCollider))]
public class Weapon : MonoBehaviour, IDamageModifier
{
    public event Action OnWeaponHit;

    float baseDamage;
    float percentageBonusDamage;

    [SerializeField] CapsuleCollider hitbox;
    [SerializeField] Collider myCollider;
    [SerializeField] TrailRenderer trail;
    [SerializeField] TimeManipulator time = null;
    [SerializeField] ParticleSystem bloodSplatter;
    [SerializeField] ParticleSystem hitEffect;
    
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

    void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) { return; }

        if (other.TryGetComponent<Health>(out Health health))
        {
            OnWeaponHit?.Invoke();

            health.DealDamage(damageCounter.GetDamage());

            bloodSplatter.Play();
            hitEffect.Play();

            if (time != null)
            {
                time.SlowTime();
            }
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
