using UnityEngine;

public class Player_CounterAttackState : PlayerState
{
    public static readonly int countered = Animator.StringToHash("counterAttackPreformed");
    private PlayerCombat playerCombat;
    private bool counteredSomebody;

    public Player_CounterAttackState(
        Player player,
        StateMachine stateMachine,
        string animBoolName)
        : base(player, stateMachine, animBoolName)
    {
        playerCombat = player.GetComponent<PlayerCombat>();
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = playerCombat.GetCounterDuration();
        counteredSomebody = playerCombat.CounterAttackPerformed();
        
        animator.SetBool(countered, counteredSomebody);
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(0, rb.linearVelocity.y);

        if (triggerCalled) stateMachine.ChangeState(player.idleState);

        if (stateTimer < 0 && !counteredSomebody) stateMachine.ChangeState(player.idleState);
    }
}
