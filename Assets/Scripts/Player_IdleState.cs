using UnityEngine;

public class Player_IdleState : EntityState
{
    public Player_IdleState(
        Player player,
        StateMachine stateMachine,
        string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Update()
    {
        base.Update();

        // Enter Move if any movement key (W/A/S/D) is pressed
        if (player.moveInput != Vector2.zero)
            stateMachine.ChangeState(player.moveState);
    }
}

