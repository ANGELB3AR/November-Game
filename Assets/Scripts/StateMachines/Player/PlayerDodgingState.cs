using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgingState : PlayerBaseState
{
    readonly int DodgingBlendTreeHash = Animator.StringToHash("DodgingBlendTree");
    readonly int DodgingForwardHash = Animator.StringToHash("DodgingForward");
    readonly int DodgingRightHash = Animator.StringToHash("DodgingRight");

    const float crossFadeDuration = 0.1f;
    Vector2 dodgingDirection;
    float remainingDodgeTime;

    public PlayerDodgingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(DodgingBlendTreeHash, crossFadeDuration);

        stateMachine.SetDodgeTime();
        remainingDodgeTime = stateMachine.DodgeDuration;

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            dodgingDirection = new Vector2(0, -1);
        }
        else
        {
            dodgingDirection = stateMachine.InputReader.MovementValue;
        }
    }

    public override void Tick(float deltaTime)
    {
        remainingDodgeTime = Mathf.Max(remainingDodgeTime - deltaTime, 0f);

        Move(CalculateMovement(dodgingDirection, remainingDodgeTime), stateMachine.DodgeSpeed, deltaTime);
        UpdateAnimator(DodgingForwardHash, DodgingRightHash, deltaTime);

        if (stateMachine.Targeter.CurrentTarget != null)
        {
            FaceTarget();
        }

        if (remainingDodgeTime == 0)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {
        
    }
}
