using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field:SerializeField] public InputReader InputReader { get; private set; }
    [field:SerializeField] public CharacterController Controller { get; private set; }
    [field:SerializeField] public Animator Animator { get; private set; }
    [field:SerializeField] public Transform MainCameraTransform { get; private set; }
    [field:SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field:SerializeField] public float TargetingMovementSpeed { get; private set; }

    void Awake()
    {
        MainCameraTransform = Camera.main.transform;
    }

    void Start()
    {
        SwitchState(new PlayerFreeLookState(this));
    }
}
