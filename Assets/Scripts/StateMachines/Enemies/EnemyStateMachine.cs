using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    // Component References
    [field:SerializeField] public CharacterController Controller { get; private set; }
    [field:SerializeField] public NavMeshAgent Agent { get; private set; }
    [field:SerializeField] public Animator Animator { get; private set; }
    [field:SerializeField] public FieldOfView FieldOfView { get; private set; }
    [field:SerializeField] public Health Health { get; private set; }
    [field:SerializeField] public WeaponHandler Weapon { get; private set; }
    [field:SerializeField] public DamageCounter Damage { get; private set; }
    [field:SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field:SerializeField] public AudioSource Audio { get; private set; }
    [field:SerializeField] public KnockbackReceiver Knockback { get; private set; }

    // External References
    [field:SerializeField] public PlayerStateMachine Player { get; private set; }

    // Variable References
    [field:SerializeField] public float MovementSpeed { get; private set; }
    [field:SerializeField] public float AttackRange { get; private set; }
    [field:SerializeField] public Attack[] SwordCombo { get; private set; }
    [field:SerializeField] public Attack[] SpearCombo { get; private set; }
    [field:SerializeField] public Attack[] HeavyCombo { get; private set; }
    [field:SerializeField] public float ImpactDuration { get; private set; }
    [field:SerializeField] public float Gravity { get; private set; }
    [field: SerializeField] public AudioClip[] ImpactSounds { get; private set; }
    [field: SerializeField] public AudioClip[] DeathSounds { get; private set; }


    void Awake()
    {
        Player = FieldOfView.GetPlayer();
    }

    void OnEnable()
    {
        Health.OnDamageReceived += HandleImpact;
        Health.OnDeath += HandleDeath;
    }

    void Start()
    {
        SwitchState(new EnemyIdlingState(this));

        Agent.updatePosition = false;
        Agent.updateRotation = false;
    }

    void OnDisable()
    {
        Health.OnDamageReceived -= HandleImpact;
        Health.OnDeath -= HandleDeath;
    }

    void HandleImpact()
    {
        SwitchState(new EnemyImpactState(this));
    }

    void HandleDeath()
    {
        SwitchState(new EnemyDeathState(this));

        Ragdoll.ToggleRagdoll(true);
        GetComponent<Target>().enabled = false;
        Knockback.enabled = false;
        Controller.enabled = false;
        Animator.enabled = false;
        Weapon.DropWeapon();
        Audio.PlayOneShot(DeathSounds[UnityEngine.Random.Range(0, 2)]);
    }
}
