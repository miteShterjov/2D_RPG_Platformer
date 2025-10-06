using UnityEngine;

public class Player_WallSlideState : Player_AiredState
{
    public Player_WallSlideState(
        Player player,
        StateMachine stateMachine,
        string animBoolName)
    : base(player, stateMachine, animBoolName) { }

    public override void Update()
    {
        base.Update();
        HandleWallSlide();

        if (playerInput.Player.Jump.WasPressedThisFrame()) stateMachine.ChangeState(player.wallJumpState);

        if (!player.wallDetected && !player.groundDetected) stateMachine.ChangeState(player.fallState);

        if (player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
            if (player.facingDirection != player.moveInput.x) player.Flip();
        }
    }

    private void HandleWallSlide()
    {
        if (player.moveInput.y < 0) player.SetVelocity(player.moveInput.x, rb.linearVelocity.y);
        else player.SetVelocity(player.moveInput.x, rb.linearVelocity.y * player.wallDragMultiplier);
    }
}
