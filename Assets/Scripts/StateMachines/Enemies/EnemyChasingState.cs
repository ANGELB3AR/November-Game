using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    readonly int ChasingBlendTreeHash = Animator.StringToHash("ChasingBlendTree");
    readonly int ChasingSpeedHash = Animator.StringToHash("ChasingSpeed");

    const float crossFadeDuration = 0.1f;

    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ChasingBlendTreeHash, crossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        MoveToPlayer(deltaTime);
        FacePlayer();
        AvoidOtherAI(deltaTime);

        if (InAttackRange())
        {
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine, 0));
        }

        if (!stateMachine.FieldOfView.CanSeePlayer())
        {
            stateMachine.SwitchState(new EnemyIdlingState(stateMachine));
        }

        if (stateMachine.Controller.velocity == Vector3.zero)
        {
            stateMachine.Animator.SetFloat(ChasingSpeedHash, 0);
        }
        else
        {
            stateMachine.Animator.SetFloat(ChasingSpeedHash, 1);
        }
    }

    public override void Exit()
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
    }

    void MoveToPlayer(float deltaTime)
    {
        stateMachine.Agent.SetDestination(stateMachine.Player.transform.position);
        Move(stateMachine.Agent.desiredVelocity.normalized, stateMachine.MovementSpeed, deltaTime);

        stateMachine.Agent.velocity = stateMachine.Controller.velocity;
    }

    void FacePlayer()
    {
        Vector3 lookDirection = stateMachine.Player.transform.position - stateMachine.transform.position;
        lookDirection.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    void AvoidOtherAI(float deltaTime)
    {
        foreach (EnemyStateMachine AI in stateMachine.AITracker.activeAIUnits)
        {
            if (AI != this.stateMachine)
            {
                if (Vector3.Distance(this.stateMachine.transform.position, AI.transform.position) <= stateMachine.AIAvoidanceDistance)
                {
                    Vector3 direction = (stateMachine.transform.position - AI.transform.position).normalized;
                    Move(direction, stateMachine.MovementSpeed, deltaTime);

                    stateMachine.Agent.velocity = stateMachine.Controller.velocity;
                }
            }
        }
    }
}
