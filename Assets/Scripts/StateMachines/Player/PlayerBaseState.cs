using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y
            + right * stateMachine.InputReader.MovementValue.x;
    }

    protected Vector3 CalculateMovement(Vector2 dodgeDirection, float remainingDodgeTime)
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        if (remainingDodgeTime > 0)
        {
            forward += stateMachine.transform.forward * dodgeDirection.y * stateMachine.DodgeDistance / stateMachine.DodgeDuration;
            right += stateMachine.transform.right * dodgeDirection.x * stateMachine.DodgeDistance / stateMachine.DodgeDuration;
        }

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y
            + right * stateMachine.InputReader.MovementValue.x;
    }


    protected void Move(Vector3 direction, float speed, float deltaTime)
    {
        if (!stateMachine.Controller.isGrounded)
        {
            direction = ApplyGravity(direction, deltaTime);
        }

        stateMachine.Controller.Move(direction * speed * deltaTime);
    }

    Vector3 ApplyGravity(Vector3 direction, float deltaTime)
    {
        direction.y -= stateMachine.Gravity * deltaTime;
        return direction;
    }

    protected void ReturnToLocomotion()
    {
        if (stateMachine.Targeter.CurrentTarget != null)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }

    protected void UpdateAnimator(int forwardHash, int rightHash, float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.y == 0)
        {
            stateMachine.Animator.SetFloat(forwardHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(forwardHash, value, 0.1f, deltaTime);
        }

        if (stateMachine.InputReader.MovementValue.x == 0)
        {
            stateMachine.Animator.SetFloat(rightHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(rightHash, value, 0.1f, deltaTime);
        }
    }

    protected void FaceTarget()
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
}
