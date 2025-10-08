using UnityEngine;

public class EnemyState : EntityState
{
    private static readonly int MoveAnimSpeedMultiplier = Animator.StringToHash("moveAnimSpeedMultiplier");
    private static readonly int BattleAnimSpeedMultiplier = Animator.StringToHash("battleAnimSpeedMultiplier");
    private static readonly int hasAggroVelocity = Animator.StringToHash("xVelocity");
    protected Enemy enemy;
    protected EnemyState currentState;

    public EnemyState(Enemy enemy,
        StateMachine stateMachine,
        string animBoolName
        ) : base(stateMachine, animBoolName)
    {
        this.enemy = enemy;

        animator = enemy.animator;
        rb = enemy.rb;
    }
    
    public override void Enter()
    {
        base.Enter();
        currentState = this;
    }

    public override void Update()
    {
        base.Update();

        float battleAnimSpeedMultiplier = enemy.battleMoveSpeed / enemy.moveSpeed;

        animator.SetFloat(BattleAnimSpeedMultiplier, battleAnimSpeedMultiplier);
        animator.SetFloat(MoveAnimSpeedMultiplier, enemy.moveAnimSpeedMultiplier);
        animator.SetFloat(hasAggroVelocity, Mathf.Abs(rb.linearVelocity.x));

        Debug.Log("current state: " + this);
    }
}
