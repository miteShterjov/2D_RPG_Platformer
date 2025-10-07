using UnityEngine;

public class Player_FallState : Player_AiredState
{
    public Player_FallState(
        Player player,
        StateMachine stateMachine,
        string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Update()
    {
        base.Update();
        if (player.GroundDetected && player.moveInput.x != 0)
        {
            stateMachine.ChangeState(player.moveState);
            return;
        }

        if (player.GroundDetected) stateMachine.ChangeState(player.idleState);

        if (player.WallDetected) stateMachine.ChangeState(player.wallSlideState);
    }
}
