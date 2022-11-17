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
        Debug.Log("Enemy Chasing State");

        stateMachine.Animator.CrossFadeInFixedTime(ChasingBlendTreeHash, crossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        MoveToPlayer(deltaTime);

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
        stateMachine.Agent.SetDestination(stateMachine.FieldOfView.GetPlayer().transform.position);
        stateMachine.Controller.Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed * deltaTime);

        stateMachine.Agent.velocity = stateMachine.Controller.velocity;
    }
}
