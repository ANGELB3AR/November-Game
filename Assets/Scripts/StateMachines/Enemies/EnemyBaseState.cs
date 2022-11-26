using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public bool InAttackRange()
    {
        float distanceToPlayer = Vector3.Distance(stateMachine.Player.transform.position, stateMachine.transform.position);
        return distanceToPlayer <= stateMachine.AttackRange;
    }

    protected void Move(Vector3 velocity, float speed, float deltaTime)
    {
        if (!stateMachine.Controller.isGrounded)
        {
            velocity = ApplyGravity(velocity, deltaTime);
        }

        stateMachine.Controller.Move(velocity * speed * deltaTime);
    }

    Vector3 ApplyGravity(Vector3 velocity, float deltaTime)
    {
        velocity.y -= stateMachine.Gravity * deltaTime;
        return velocity;
    }
}
