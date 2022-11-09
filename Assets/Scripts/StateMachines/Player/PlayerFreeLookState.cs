using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

    const float crossFadeDuration = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Free Look State");

        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.InputReader.AttackEvent += OnAttack;

        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, crossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(CalculateMovement(), stateMachine.FreeLookMovementSpeed, deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero) 
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, 0.1f, deltaTime);
            return;
        }
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, 0.1f, deltaTime);
        stateMachine.transform.rotation = Quaternion.LookRotation(CalculateMovement());
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.TargetEvent -= OnTarget;
        stateMachine.InputReader.AttackEvent -= OnAttack;
    }

    void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }

    void OnTarget()
    {
        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

    void OnAttack()
    {
        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
    }
}
