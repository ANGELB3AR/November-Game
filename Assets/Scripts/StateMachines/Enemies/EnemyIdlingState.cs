using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdlingState : EnemyBaseState
{
    readonly int idleHash = Animator.StringToHash("Idle");

    const float crossFadeDuration = 0.1f;

    public EnemyIdlingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(idleHash, crossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(Vector3.zero, stateMachine.MovementSpeed, deltaTime);

        if (stateMachine.FieldOfView.CanSeePlayer())
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }
}
