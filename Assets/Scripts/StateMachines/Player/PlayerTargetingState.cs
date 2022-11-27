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

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnCancel;
        stateMachine.InputReader.CycleTargetLeftEvent += OnCycleTargetLeft;
        stateMachine.InputReader.CycleTargetRightEvent += OnCycleTargetRight;
        stateMachine.InputReader.AttackEvent += OnAttack;
        stateMachine.InputReader.DodgeEvent += OnDodge;

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
        UpdateAnimator(TargetingForwardHash, TargetingRightHash, deltaTime);
        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnCancel;
        stateMachine.InputReader.CycleTargetLeftEvent -= OnCycleTargetLeft;
        stateMachine.InputReader.CycleTargetRightEvent -= OnCycleTargetRight;
        stateMachine.InputReader.AttackEvent -= OnAttack;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
    }

    void OnCancel()
    {
        stateMachine.Targeter.Cancel();

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
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

    void OnDodge()
    {
        if (Time.time - stateMachine.PreviousDodgeTime < stateMachine.DodgeCooldown) { return; }
        stateMachine.SwitchState(new PlayerDodgingState(stateMachine));
    }
}
