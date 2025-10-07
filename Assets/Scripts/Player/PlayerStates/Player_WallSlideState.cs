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

        if (!player.WallDetected && !player.GroundDetected) stateMachine.ChangeState(player.fallState);

        if (player.GroundDetected)
        {
            stateMachine.ChangeState(player.idleState);
            if (player.FacingDirection != player.moveInput.x) player.Flip();
        }
    }

    private void HandleWallSlide()
    {
        if (player.moveInput.y < 0) player.SetVelocity(player.moveInput.x, rb.linearVelocity.y);
        else player.SetVelocity(player.moveInput.x, rb.linearVelocity.y * player.wallDragMultiplier);
    }
}
