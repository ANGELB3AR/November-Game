using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] GameObject swordHitbox;

    // All methods are called by Animation Events
    public void EnableSwordHitbox()
    {
        swordHitbox.SetActive(true);
    }

    public void DisableSwordHitbox()
    {
        swordHitbox.SetActive(false);
    }
}
