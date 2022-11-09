using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public event Action TargetEvent;
    public event Action CycleTargetLeftEvent;
    public event Action CycleTargetRightEvent;
    public event Action AttackEvent;

    public Vector2 MovementValue { get; private set; }

    Controls controls;

    void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context) { }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        TargetEvent?.Invoke();
    }

    public void OnCycleTargetLeft(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        CycleTargetLeftEvent?.Invoke();
    }

    public void OnCycleTargetRight(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        CycleTargetRightEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        AttackEvent?.Invoke();
    }
}
