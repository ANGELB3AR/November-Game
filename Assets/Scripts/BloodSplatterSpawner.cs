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
        ParticlePhysicsExtensions.GetCollisionEvents(bloodParticles, other, collisionEvents);
        int safeLength = bloodParticles.GetSafeCollisionEventSize();
        int count = collisionEvents.Count;

        if (count < safeLength)
        {
            collisionEvents = new List<ParticleCollisionEvent>(safeLength);
        }

        for (int i = 0; i < count; i++)
        {
            GameObject bloodStain = Instantiate(bloodDecal, collisionEvents[i].intersection, Quaternion.identity) as GameObject;
            bloodStain.transform.SetParent(bloodStainHolder, true);
            Debug.Log($"Blood stain instantiated at {collisionEvents[i].intersection}");
        }
    }
}
