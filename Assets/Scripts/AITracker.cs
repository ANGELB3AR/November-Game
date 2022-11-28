using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITracker : MonoBehaviour
{
    [SerializeField] float radius = 1f;

    Transform target;
    List<EnemyStateMachine> activeAIUnits = new List<EnemyStateMachine>();

    void Start()
    {
        target = FindObjectOfType<PlayerStateMachine>().transform;
        foreach (EnemyStateMachine AI in FindObjectsOfType<EnemyStateMachine>())
        {
            activeAIUnits.Add(AI);
        }
    }

    public Vector3 SurroundTarget()
    {
        for (int i = 1; i < activeAIUnits.Count; i++)
        {
            return new Vector3(
                target.position.x + radius * Mathf.Cos(2 * Mathf.PI * i / activeAIUnits.Count),
                target.position.y,
                target.position.z + radius * Mathf.Sin(2 * Mathf.PI * i / activeAIUnits.Count));
        }
        return target.position;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(target.position, radius);
    }
}
