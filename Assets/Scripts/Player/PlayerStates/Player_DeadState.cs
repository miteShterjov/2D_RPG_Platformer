using UnityEngine;

public class Player_DeadState : PlayerState
{
    public Player_DeadState(
        Player player,
        StateMachine stateMachine,
        string animBoolName)
        : base(player, stateMachine, animBoolName) { }
        
    public override void Enter()
    {
        base.Enter();
        player.playerInput.Disable();
        player.rb.simulated = false;
    }
}
