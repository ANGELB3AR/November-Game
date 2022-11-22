using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    // Component References
    [field:SerializeField] public InputReader InputReader { get; private set; }
    [field:SerializeField] public CharacterController Controller { get; private set; }
    [field:SerializeField] public Animator Animator { get; private set; }
    [field:SerializeField] public Targeter Targeter { get; private set; }
    [field:SerializeField] public Health Health { get; private set; }
    [field:SerializeField] public WeaponHandler Weapon { get; private set; }
    [field:SerializeField] public DamageCounter Damage { get; private set; }

    // External References
    [field:SerializeField] public Transform MainCameraTransform { get; private set; }

    // Variable References
    [field:SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field:SerializeField] public float TargetingMovementSpeed { get; private set; }
    [field:SerializeField] public Attack[] SwordCombo { get; private set; }
    [field:SerializeField] public Attack[] SpearCombo { get; private set; }
    [field: SerializeField] public Attack[] HeavyCombo { get; private set; }
    [field:SerializeField] public float ImpactDuration { get; private set; }


    void Awake()
    {
        MainCameraTransform = Camera.main.transform;
    }

    void OnEnable()
    {
        Health.DamageReceived += InitiateImpact;
    }

    void Start()
    {
        SwitchState(new PlayerFreeLookState(this));
    }

    void OnDisable()
    {
        Health.DamageReceived -= InitiateImpact;
    }

    void InitiateImpact()
    {
        SwitchState(new PlayerImpactState(this));
    }
}
