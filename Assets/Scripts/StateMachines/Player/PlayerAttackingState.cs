using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    Attack attack;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
       if (stateMachine.Weapon.CurrentWeapon.weaponClass == WeaponClass.Sword)
        {
            attack = stateMachine.SwordCombo[attackIndex];
        }
       else if (stateMachine.Weapon.CurrentWeapon.weaponClass == WeaponClass.Spear)
        {
            attack = stateMachine.SpearCombo[attackIndex];
        }
       else if (stateMachine.Weapon.CurrentWeapon.weaponClass == WeaponClass.Heavy)
        {
            attack = stateMachine.HeavyCombo[attackIndex];
        }
    }

    public override void Enter()
    {
        stateMachine.InputReader.AttackEvent += OnAttack;

        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
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

        stateMachine.Weapon.DisableWeaponColliders();
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
