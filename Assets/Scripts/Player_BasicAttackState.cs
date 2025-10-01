using UnityEngine;

public class Player_BasicAttackState : EntityState
{
    private float attackVelocityTimer;

    private const int FirstComboIndex = 1;
    private int currentComboIndex = 1;
    private int comboLimit = 3;

    public Player_BasicAttackState(
        Player player,
        StateMachine stateMachine,
        string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        ResetComboIndexWhenCapped();

        animator.SetInteger("basicAttackIndex", currentComboIndex);
        ApplyAttackVelocity();
    }


    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        if (triggerCalled) stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();

        currentComboIndex++;
    }

    private void ResetComboIndexWhenCapped()
    {
        if (currentComboIndex > comboLimit) currentComboIndex = FirstComboIndex;
    }
    
    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;
        if (attackVelocityTimer <= 0) player.SetVelocity(0, rb.linearVelocity.y);
    }

    private void ApplyAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(player.attackVelocity.x, player.attackVelocity.y);
    }
}
