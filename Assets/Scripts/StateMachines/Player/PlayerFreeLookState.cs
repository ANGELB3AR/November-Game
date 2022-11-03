using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputReader.JumpEvent += OnJump;
    }

    public override void Tick(float deltaTime)
    {
        
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
    }

    void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }
}
