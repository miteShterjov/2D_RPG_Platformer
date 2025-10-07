using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy enemy;
    public EnemyState(Enemy enemy,
        StateMachine stateMachine,
        string animBoolName
        ) : base(stateMachine, animBoolName)
    {
        this.enemy = enemy;

        animator = enemy.animator;
        rb = enemy.rb;
    }
}
