using UnityEngine;

public class Player_IdleState : Player_GroundedState
{
    public Player_IdleState(
        Player player,
        StateMachine stateMachine,
        string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0, rb.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput.x == player.facingDirection && player.wallDetected) return;

        // Enter Move if any movement key (W/A/S/D) is pressed
        if (player.moveInput != Vector2.zero) stateMachine.ChangeState(player.moveState);
    }
}

