using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Weapon : MonoBehaviour
{
    [SerializeField] DamageCounter damageCounter;
    [SerializeField] CapsuleCollider hitbox;
    [SerializeField] Collider myCollider;

    void Start()
    {
        damageCounter = GetComponentInParent<DamageCounter>();
        hitbox = GetComponent<CapsuleCollider>();
        myCollider = GetComponentInParent<CharacterController>();

        DisableHitbox();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) { return; }

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damageCounter.GetDamage());
            Debug.Log($"Weapon hit with {damageCounter.GetDamage()} damage");
        }
    }

    public void EnableHitbox()
    {
        hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }
}
