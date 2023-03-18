using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    Attack attack;
    bool shouldMove = true;

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
        stateMachine.Weapon.EquippedPrefab.OnWeaponHit += StopMoving;

        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);

        stateMachine.Weapon.EquippedPrefab.PlaySlashEffect();

        stateMachine.Audio.PlayOneShot(attack.SoundEffect);
    }

    public override void Tick(float deltaTime)
    {
        ApplyForwardMovement(deltaTime);
        ApplyRotationControl();

        if (GetNormalizedTime(stateMachine.Animator) >= 1f)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {
        stateMachine.InputReader.AttackEvent -= OnAttack;
        stateMachine.Weapon.EquippedPrefab.OnWeaponHit -= StopMoving;

        stateMachine.Weapon.DisableWeaponColliders();
    }

    void ApplyForwardMovement(float deltaTime)
    {
        if (!shouldMove) { return; }

        Vector3 direction;

        if (CalculateMovement() == Vector3.zero)
        {
            direction = CalculateMovement();
        }
        else
        {
            direction = stateMachine.transform.forward;
        }

        Move(direction, stateMachine.ForwardAttackSpeed, deltaTime);
    }

    void ApplyRotationControl()
    {
        if (stateMachine.Targeter.CurrentTarget != null) { return; }
        if (!shouldMove) { return; }
        stateMachine.transform.rotation = Quaternion.LookRotation(CalculateMovement());
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

    void StopMoving()
    {
        shouldMove = false;
    }
}
