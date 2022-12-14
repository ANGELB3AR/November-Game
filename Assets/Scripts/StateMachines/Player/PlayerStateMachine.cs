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
    [field:SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field:SerializeField] public AudioSource Audio { get; private set; }

    // External References
    [field:SerializeField] public Transform MainCameraTransform { get; private set; }

    // Variable References
    [field:SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field:SerializeField] public float TargetingMovementSpeed { get; private set; }
    [field:SerializeField] public float ForwardAttackSpeed { get; private set; }
    [field:SerializeField] public Attack[] SwordCombo { get; private set; }
    [field:SerializeField] public Attack[] SpearCombo { get; private set; }
    [field: SerializeField] public Attack[] HeavyCombo { get; private set; }
    [field:SerializeField] public float ImpactDuration { get; private set; }
    [field:SerializeField] public float Gravity { get; private set; }
    [field:SerializeField] public AudioClip[] ImpactSounds { get; private set; }
    [field:SerializeField] public AudioClip[] DeathSounds { get; private set; }
    [field:SerializeField] public float DodgeDuration { get; private set; }
    [field:SerializeField] public float DodgeDistance { get; private set; }
    [field:SerializeField] public float DodgeCooldown { get; private set; }
    public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity;

    void Awake()
    {
        MainCameraTransform = Camera.main.transform;
    }

    void OnEnable()
    {
        Health.OnDamageReceived += HandleImpact;
        Health.OnDeath += HandleDeath;
    }

    void Start()
    {
        SwitchState(new PlayerFreeLookState(this));
    }

    void OnDisable()
    {
        Health.OnDamageReceived -= HandleImpact;
        Health.OnDeath -= HandleDeath;
    }

    void HandleImpact()
    {
        SwitchState(new PlayerImpactState(this));
    }

    void HandleDeath()
    {
        SwitchState(new PlayerDeathState(this));

        Ragdoll.ToggleRagdoll(true);
        Controller.enabled = false;
        Animator.enabled = false;
        Weapon.DropWeapon();
        Audio.PlayOneShot(DeathSounds[UnityEngine.Random.Range(0, 2)]);
    }

    public void SetDodgeTime()
    {
        PreviousDodgeTime = Time.deltaTime;
    }
}
