using UnityEngine;

public class Enemy_MoveState : Enemy_GroundedState
{
    public Enemy_MoveState(
        Enemy enemy,
        StateMachine stateMachine,
        string animBoolName)
        : base(enemy, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        if (!enemy.GroundDetected || enemy.WallDetected) enemy.Flip();
    }

    public override void Update()
    {
       base.Update();

       enemy.SetVelocity(enemy.moveSpeed * enemy.FacingDirection, enemy.rb.linearVelocity.y);

       if (!enemy.GroundDetected || enemy.WallDetected) stateMachine.ChangeState(enemy.idleState);
    }
    
}
