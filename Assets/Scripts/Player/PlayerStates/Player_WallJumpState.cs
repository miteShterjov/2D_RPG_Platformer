using UnityEngine;

public class Player_WallJumpState : PlayerState
{
    private float regrabLockTIme = 0.15f;
    private float regrabLockTimer;

    public Player_WallJumpState(
        Player player,
        StateMachine stateMachine,
        string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        int pushDirection = -player.FacingDirection;

        regrabLockTimer = regrabLockTIme;

        player.SetVelocity(player.wallJumpForce.x * pushDirection, player.wallJumpForce.y);
        if (player.FacingDirection != player.moveInput.x) player.Flip();

    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocity.y < 0) stateMachine.ChangeState(player.fallState);

        // This is here becouse the player was re-grabbing the wall immediately after launch. 
        // The jump velocity is set in Enter, but Update can switch to wallSlide the same frame because 
        // wallDetected is still true, so the jump gets canceled 9/10 times. This timer prevents that.
        if (regrabLockTimer > 0)
        {
            regrabLockTimer -= Time.deltaTime;
            return;
        }

        if (player.WallDetected) stateMachine.ChangeState(player.wallSlideState);
    }
}
