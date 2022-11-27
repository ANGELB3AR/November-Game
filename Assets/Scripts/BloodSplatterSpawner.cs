using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatterSpawner : MonoBehaviour
{
    [SerializeField] GameObject bloodDecal;

    void OnParticleCollision(GameObject other)
    {
        Instantiate(bloodDecal, other.transform.position, Quaternion.identity);
    }
}
