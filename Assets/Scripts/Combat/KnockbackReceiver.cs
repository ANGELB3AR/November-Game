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

        if (controller.enabled == true)
        {
            knockbackForce.y = 0;
            controller.Move(knockbackForce);
        }

        if (agent != null)
        {
            agent.enabled = true;
        }
        Debug.Log($"Applied knockback force of {knockbackForce}");
    }
}
