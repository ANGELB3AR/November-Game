using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITracker : MonoBehaviour
{
    public List<EnemyStateMachine> activeAIUnits = new List<EnemyStateMachine>();

    void Start()
    {
        foreach (EnemyStateMachine AI in FindObjectsOfType<EnemyStateMachine>())
        {
            activeAIUnits.Add(AI);
        }
    }
}
