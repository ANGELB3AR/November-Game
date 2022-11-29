using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatterSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] bloodDecals;
    [SerializeField] ParticleSystem bloodParticles;
    [SerializeField] Transform bloodStainHolder;

    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    void Start()
    {
        bloodStainHolder = FindObjectOfType<BloodStainManager>().transform;
    }

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
            GameObject bloodStain = Instantiate(bloodDecals[Random.Range(0, bloodDecals.Length)], collisionEvents[i].intersection, Quaternion.identity) as GameObject;
            bloodStain.transform.SetParent(bloodStainHolder, true);
        }
    }
}
