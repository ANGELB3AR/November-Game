using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatterSpawner : MonoBehaviour
{
    [SerializeField] GameObject bloodDecal;
    [SerializeField] ParticleSystem bloodParticles;
    [SerializeField] Transform bloodStainHolder;

    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Particle collision occurred");
        ParticlePhysicsExtensions.GetCollisionEvents(bloodParticles, other, collisionEvents);

        int count = collisionEvents.Count;

        for (int i = 0; i < count; i++)
        {
            GameObject bloodStain = Instantiate(bloodDecal, collisionEvents[i].intersection, Quaternion.identity) as GameObject;
            bloodStain.transform.SetParent(bloodStainHolder, true);
        }
    }
}
