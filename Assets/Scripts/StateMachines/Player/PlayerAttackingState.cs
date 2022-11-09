using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    Attack attack;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        Debug.Log("Attacking State");

        stateMachine.InputReader.AttackEvent += OnAttack;

        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
        stateMachine.Weapon.SetAttack(attack.Damage);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime() >= 1f)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {
        stateMachine.InputReader.AttackEvent -= OnAttack;
    }

    float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        if (stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }

        if (!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }

        return 0f;
    }

    void TryComboAttack(float normalizedTime)
    {
        if (attack.ComboIndex == -1) { return; }
        if (normalizedTime < attack.ComboAttackTime) { return; }

        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, attack.ComboIndex));
    }

    void OnAttack()
    {
        TryComboAttack(GetNormalizedTime());
    }

    void ReturnToLocomotion()
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
}
