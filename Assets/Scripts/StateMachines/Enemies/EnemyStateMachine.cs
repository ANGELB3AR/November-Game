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

    // External References
    [field:SerializeField] public PlayerStateMachine Player { get; private set; }

    // Variable References
    [field:SerializeField] public float MovementSpeed { get; private set; }
    [field:SerializeField] public float AttackRange { get; private set; }

    void Awake()
    {
        Player = FieldOfView.GetPlayer();
    }

    void Start()
    {
        SwitchState(new EnemyIdlingState(this));

        Agent.updatePosition = false;
        Agent.updateRotation = false;
    }
}
