using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] float radius;
    [Range(0,360)]
    [SerializeField] float angle;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstructionMask;

    Transform target;
    PlayerController player;
    bool canSeeTarget;
    AITracker AITracker;
    EnemyStateMachine stateMachine;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        AITracker = FindObjectOfType<AITracker>();
        stateMachine = GetComponent<EnemyStateMachine>();
    }

    void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            //target = rangeChecks[0].transform;  // Change to foreach loop over rangeChecks when ready for enemies to attack each other

            if (!SearchForPlayer(rangeChecks))
            {
                SearchForOtherTargets(rangeChecks);
            }

            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeeTarget = true;
                    TryJoinGroup();
                    TryLeaveGroup();
                }
                else
                {
                    canSeeTarget = false;
                }
            }
            else
            {
                canSeeTarget = false;
            }
        }
        else if (canSeeTarget)
        {
            canSeeTarget = false;
        }
    }

    bool SearchForPlayer(Collider[] rangeChecks)
    {
        for (int i = 0; i < rangeChecks.Length; i++)
        {
            if (rangeChecks[i].TryGetComponent<Health>(out Health health))
            {
                if (rangeChecks[i].CompareTag("Player"))
                {
                    target = rangeChecks[i].transform;
                    stateMachine.SetCurrentTarget(health);
                    return true;
                }
            }
        }
        return false;
    }

    void SearchForOtherTargets(Collider[] rangeChecks)
    {
        for (int i = 0; i < rangeChecks.Length; i++)
        {
            if (rangeChecks[i].TryGetComponent<Health>(out Health health))
            {
                if (health != this.GetComponent<Health>())
                {
                    if (health.IsAlive())
                    {
                        target = rangeChecks[i].transform;
                        stateMachine.SetCurrentTarget(health);
                        continue;
                    }
                }
            }
        }
    }

    void TryJoinGroup()
    {
        if (!IsPlayerTarget()) { return; }
        AITracker.JoinGroup(GetComponent<EnemyStateMachine>());
    }

    void TryLeaveGroup()
    {
        if (IsPlayerTarget()) { return; }
        AITracker.LeaveGroup(GetComponent<EnemyStateMachine>());
    }

    public bool CanSeeTarget()
    {
        if (target == null || !target.GetComponent<Health>().IsAlive())
        {
            return false;
        }
        return canSeeTarget;
    }

    public bool IsPlayerTarget()
    {
        return target.CompareTag("Player");
    }

    public float GetRadius()
    {
        return radius;
    }

    public float GetAngle()
    {
        return angle;
    }

    public PlayerController GetPlayer()
    {
        return player;
    }

    public Vector3 GetTargetPosition()
    {
        return target.position;
    }
}
