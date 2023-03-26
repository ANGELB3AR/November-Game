using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
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
    [field:SerializeField] public PlayerState CurrentState { get; private set; }
    [field:SerializeField] public PlayerState PreviousState { get; private set; }
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

    // Animator Variables
    readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

    const float crossFadeDuration = 0.1f;

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
        SwitchState(PlayerState.Freelook);
    }

    void OnDisable()
    {
        Health.OnDamageReceived -= HandleImpact;
        Health.OnDeath -= HandleDeath;
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        if (!IsOwner) { return; }

        HandleCurrentState(deltaTime);
    }

    void HandleCurrentState(float deltaTime)
    {
        switch (CurrentState)
        {
            case PlayerState.Idling:
                break;
            case PlayerState.Attacking:
                break;
            case PlayerState.Death:

                break;
            case PlayerState.Dodging:
                break;
            case PlayerState.Freelook:
                Move(CalculateMovement(), FreeLookMovementSpeed, deltaTime);

                if (InputReader.MovementValue == Vector2.zero)
                {
                    Animator.SetFloat(FreeLookSpeedHash, 0, 0.1f, deltaTime);
                    return;
                }
                Animator.SetFloat(FreeLookSpeedHash, 1, 0.1f, deltaTime);
                transform.rotation = Quaternion.LookRotation(CalculateMovement());
                break;
            case PlayerState.Impact:
                break;
            case PlayerState.Targeting:
                break;
            default:
                break;
        }
    }

    void SwitchState(PlayerState newState)
    {
        if (CurrentState == newState) { return; }

        PreviousState = CurrentState;
        CurrentState = newState;

        HandleExitState();
        HandleEnterState();
    }

    private void HandleEnterState()
    {
        switch (CurrentState)
        {
            case PlayerState.Idling:
                break;
            case PlayerState.Attacking:
                break;
            case PlayerState.Death:
                HandleDeath();
                break;
            case PlayerState.Dodging:
                break;
            case PlayerState.Freelook:
                InputReader.TargetEvent += FreeLook_OnTarget;
                InputReader.AttackEvent += FreeLook_OnAttack;
                InputReader.DodgeEvent += FreeLook_OnDodge;

                Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, crossFadeDuration);
                break;
            case PlayerState.Impact:
                HandleImpact();
                break;
            case PlayerState.Targeting:
                break;
            default:
                break;
        }
    }

    void HandleExitState()
    {
        switch (PreviousState)
        {
            case PlayerState.Idling:
                break;
            case PlayerState.Attacking:
                break;
            case PlayerState.Death:
                break;
            case PlayerState.Dodging:
                break;
            case PlayerState.Freelook:
                break;
            case PlayerState.Impact:
                break;
            case PlayerState.Targeting:
                break;
            default:
                break;
        }
    }

    void FreeLook_OnTarget()
    {
        SwitchState(PlayerState.Targeting);
    }

    void FreeLook_OnAttack()
    {
        SwitchState(new PlayerAttackingState(stateMachine, 0));
    }

    void FreeLook_OnDodge()
    {
        if (Time.time - PreviousDodgeTime < DodgeCooldown) { return; }
        SwitchState(PlayerState.Dodging);
    }


    void HandleImpact()
    {
        SwitchState(PlayerState.Impact);
    }

    void HandleDeath()
    {
        SwitchState(PlayerState.Death);

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

    Vector3 CalculateMovement()
    {
        Vector3 forward = MainCameraTransform.forward;
        Vector3 right = MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * InputReader.MovementValue.y
            + right * InputReader.MovementValue.x;
    }

    void Move(Vector3 direction, float speed, float deltaTime)
    {
        MoveServerRpc(direction, speed, deltaTime);
    }

    [ServerRpc(RequireOwnership = true)]
    void MoveServerRpc(Vector3 direction, float speed, float deltaTime)
    {
        if (!Controller.isGrounded)
        {
            direction = ApplyGravity(direction, deltaTime);
        }

        Controller.Move(direction * speed * deltaTime);
    }

    Vector3 ApplyGravity(Vector3 direction, float deltaTime)
    {
        direction.y -= Gravity * deltaTime;
        return direction;
    }



    public enum PlayerState
    {
        Idling,
        Attacking,
        Death,
        Dodging,
        Freelook,
        Impact,
        Targeting
    }
}
