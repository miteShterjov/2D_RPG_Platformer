using UnityEngine;

public class Player_GroundedState : PlayerState
{
    public Player_GroundedState(
        Player player,
        StateMachine stateMachine,
        string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState);
            return;
        }

        if (playerInput.Player.Jump.WasPressedThisFrame()) stateMachine.ChangeState(player.jumpState);

        if (playerInput.Player.Attack.WasPressedThisFrame()) stateMachine.ChangeState(player.basicAttackState);
        
        if (playerInput.Player.CounterAttack.WasPressedThisFrame()) stateMachine.ChangeState(player.counterAttackState);
    }
}
