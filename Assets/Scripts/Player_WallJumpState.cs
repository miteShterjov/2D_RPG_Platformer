using UnityEngine;

public class Player_WallJumpState : EntityState
{
    public Player_WallJumpState(
        Player player,
        StateMachine stateMachine,
        string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        player.SetVelocity(player.wallJumpDirection.x * -player.facingDirection, player.wallJumpDirection.y);
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocity.y < 0) stateMachine.ChangeState(player.fallState);

        if (player.groundDetected) stateMachine.ChangeState(player.idleState);

        if (player.wallDetected && !player.groundDetected) stateMachine.ChangeState(player.wallSlideState);
    }
}
