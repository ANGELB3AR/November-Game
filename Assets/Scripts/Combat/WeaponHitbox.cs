using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    [SerializeField] WeaponDamage weaponDamage;

    [SerializeField] GameObject swordHitbox;
    [SerializeField] GameObject spearHitbox;
    [SerializeField] GameObject heavyHitbox;

    [SerializeField] Collider myCollider;

    void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) { return; }

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(weaponDamage.GetDamage());
            Debug.Log($"Weapon hit with {weaponDamage.GetDamage()} damage");
        }
    }

    // All methods below are called by Animation Events
    public void EnableSwordHitbox()
    {
        swordHitbox.SetActive(true);
    }

    public void DisableSwordHitbox()
    {
        swordHitbox.SetActive(false);
    }
}
