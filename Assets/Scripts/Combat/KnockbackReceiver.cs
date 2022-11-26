using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KnockbackReceiver : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] NavMeshAgent agent = null;

    public void ReceiveKnockback(Vector3 knockbackForce)
    {
        if (agent != null)
        {
            agent.enabled = false;
        }

        knockbackForce.y = 0;
        if (controller.enabled == true)
        {
            controller.Move(knockbackForce);
        }

        if (agent != null)
        {
            agent.enabled = true;
        }
    }
}
