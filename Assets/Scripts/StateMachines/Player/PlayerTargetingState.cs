using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");
    readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");

    const float crossFadeDuration = 0.1f;
    Vector3 movement;

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnCancel;
        stateMachine.InputReader.CycleTargetLeftEvent += OnCycleTargetLeft;
        stateMachine.InputReader.CycleTargetRightEvent += OnCycleTargetRight;
        stateMachine.InputReader.AttackEvent += OnAttack;

        stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, crossFadeDuration);

        stateMachine.Targeter.SelectClosestTarget();

        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }

    public override void Tick(float deltaTime)
    {
        Move(CalculateMovement(), stateMachine.TargetingMovementSpeed, deltaTime);
        UpdateAnimator(deltaTime);
        FaceTarget();
        
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnCancel;
        stateMachine.InputReader.CycleTargetLeftEvent -= OnCycleTargetLeft;
        stateMachine.InputReader.CycleTargetRightEvent -= OnCycleTargetRight;
        stateMachine.InputReader.AttackEvent -= OnAttack;
    }

    void OnCancel()
    {
        stateMachine.Targeter.Cancel();

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    void UpdateAnimator(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.y == 0)
        {
            stateMachine.Animator.SetFloat(TargetingForwardHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingForwardHash, value, 0.1f, deltaTime);
        }

        if (stateMachine.InputReader.MovementValue.x == 0)
        {
            stateMachine.Animator.SetFloat(TargetingRightHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingRightHash, value, 0.1f, deltaTime);
        }
    }

    void FaceTarget()
    {
        Target target = stateMachine.Targeter.CurrentTarget;

        if (target == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }

        Vector3 lookDirection = target.transform.position - stateMachine.transform.position;
        lookDirection.y = 0f;
        stateMachine.transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    void OnCycleTargetLeft()
    {
        stateMachine.Targeter.CycleTargetLeft();
    }

    void OnCycleTargetRight()
    {
        stateMachine.Targeter.CycleTargetRight();
    }

    void OnAttack()
    {
        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
    }
}
