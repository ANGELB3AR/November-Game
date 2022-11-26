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

        stateMachine.Weapon.ActivateWeaponTrail(true);

        stateMachine.Audio.PlayOneShot(attack.SoundEffect);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator) >= 1f)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {
        stateMachine.InputReader.AttackEvent -= OnAttack;

        stateMachine.Weapon.DisableWeaponColliders();

        stateMachine.Weapon.ActivateWeaponTrail(false);
    }

    void TryComboAttack(float normalizedTime)
    {
        if (attack.ComboIndex == -1) { return; }
        if (normalizedTime < attack.ComboAttackTime) { return; }

        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, attack.ComboIndex));
    }

    void OnAttack()
    {
        TryComboAttack(GetNormalizedTime(stateMachine.Animator));
    }
}
