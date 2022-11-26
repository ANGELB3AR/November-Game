using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    Attack attack;

    public EnemyAttackingState(EnemyStateMachine stateMachine, int attackIndex) : base(stateMachine) 
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
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);

        stateMachine.Weapon.SetKnockback(attack.Knockback);

        stateMachine.Weapon.ActivateWeaponTrail(true);

        stateMachine.Audio.PlayOneShot(attack.SoundEffect);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator) >= 1f)
        {
            if (stateMachine.FieldOfView.CanSeePlayer() && InAttackRange())
            {
                stateMachine.SwitchState(new EnemyAttackingState(stateMachine, attack.ComboIndex));
            }
            else
            {
                stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            }
        }
    }

    public override void Exit()
    {
        stateMachine.Weapon.DisableWeaponColliders();
        stateMachine.Weapon.ActivateWeaponTrail(false);
    }
}
