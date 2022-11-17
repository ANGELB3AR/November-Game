using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    readonly int attackHash = Animator.StringToHash("SpearStab");

    const float crossFadeDuration = 0.1f;

    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine) 
    {

    }


    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(attackHash, crossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator) >= 1f)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }
}
