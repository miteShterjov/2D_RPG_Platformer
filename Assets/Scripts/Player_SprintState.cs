using UnityEngine;

public class Player_SprintState : EntityState
{
    private float originalGravityScale;
    private int attackDirection;
    public Player_SprintState(
        Player player,
        StateMachine stateMachine,
        string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        attackDirection = player.moveInput.x != 0 ? (int)Mathf.Sign(player.moveInput.x) : player.facingDirection;
        stateTimer = player.sprintDuration;
        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
    }

    public override void Update()
    {
        base.Update();

        CancelDashIfNeeded();

        player.SetVelocity(player.dashSpeed * player.facingDirection, 0);

        if (stateTimer <= 0)
            if (player.groundDetected) stateMachine.ChangeState(player.idleState);
            else stateMachine.ChangeState(player.fallState);


    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, 0);
        rb.gravityScale = originalGravityScale;
    }

    private void CancelDashIfNeeded()
    {
        if (player.wallDetected)
            if (player.groundDetected) stateMachine.ChangeState(player.idleState);
            else stateMachine.ChangeState(player.wallSlideState);
    }
}