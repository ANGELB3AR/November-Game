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

    public void DisableAllColliders()
    {
        swordHitbox.SetActive(false);
        spearHitbox.SetActive(false);
        heavyHitbox.SetActive(false);
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

    public void EnableSpearHitbox()
    {
        spearHitbox.SetActive(true);
    }

    public void DisableSpearHitbox()
    {
        spearHitbox.SetActive(false);
    }
    public void EnableHeavyHitbox()
    {
        heavyHitbox.SetActive(true);
    }

    public void DisableHeavyHitbox()
    {
        heavyHitbox.SetActive(false);
    }

}
