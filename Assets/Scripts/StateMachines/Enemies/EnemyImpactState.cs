using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    readonly int impactHash = Animator.StringToHash("Impact");

    const float crossFadeDuration = 0.1f;
    float duration;

    public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(impactHash, crossFadeDuration);

        duration = stateMachine.ImpactDuration;

        stateMachine.Audio.PlayOneShot(stateMachine.ImpactSounds[Random.Range(0, 2)]);
    }

    public override void Tick(float deltaTime)
    {
        duration -= deltaTime;

        if (duration <= 0)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
    }

    public override void Exit() { }
}
