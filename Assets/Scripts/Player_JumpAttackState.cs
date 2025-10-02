using UnityEngine;

public class Player_JumpAttackState : EntityState
{
    private static readonly int jumpAttackTriggerHash = Animator.StringToHash("jumpAttackTrigger");
    private bool hasTouchedGround;
    public Player_JumpAttackState(
        Player player,
        StateMachine stateMachine,
        string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        hasTouchedGround = false;
        player.SetVelocity(player.jumpAttackVelocity.x * player.facingDirection, player.jumpAttackVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (player.groundDetected && !hasTouchedGround)
        {
            hasTouchedGround = true;
            animator.SetTrigger(jumpAttackTriggerHash);
            player.SetVelocity(0, rb.linearVelocity.y);
        }

        if (triggerCalled && player.groundDetected) stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
