using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] DamageCounter damageCounter;
    [SerializeField] GameObject hitbox;
    [SerializeField] Collider myCollider;

    void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) { return; }

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damageCounter.GetDamage());
            Debug.Log($"Weapon hit with {damageCounter.GetDamage()} damage");
        }
    }

    // All methods below are called by Animation Events
    public void EnableHitbox()
    {
        hitbox.SetActive(true);
    }

    public void DisableHitbox()
    {
        hitbox.SetActive(false);
    }
}
