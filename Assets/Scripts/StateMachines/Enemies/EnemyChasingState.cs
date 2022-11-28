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
        UpdateAnimator();

        if (InAttackRange())
        {
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine, 0));
        }

        if (!stateMachine.FieldOfView.CanSeePlayer())
        {
            stateMachine.SwitchState(new EnemyIdlingState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
    }

    void MoveToPlayer(float deltaTime)
    {
        stateMachine.Agent.SetDestination(stateMachine.AITracker.SurroundTarget());
        Move(stateMachine.Agent.desiredVelocity.normalized, stateMachine.MovementSpeed, deltaTime);

        stateMachine.Agent.velocity = stateMachine.Controller.velocity;
    }

    void FacePlayer()
    {
        Vector3 lookDirection = stateMachine.Player.transform.position - stateMachine.transform.position;
        lookDirection.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    private void UpdateAnimator()
    {
        if (stateMachine.Controller.velocity == Vector3.zero)
        {
            stateMachine.Animator.SetFloat(ChasingSpeedHash, 0);
        }
        else
        {
            stateMachine.Animator.SetFloat(ChasingSpeedHash, 1);
        }
    }
}
