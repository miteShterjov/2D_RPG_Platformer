using System;
using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    private Transform player;
    public Enemy_BattleState(
        Enemy enemy,
        StateMachine stateMachine,
        string animBoolName
        ) : base(enemy, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        
        if (player == null) player = enemy.GetPlayerReference();

        if (ShouldRetreat())
        {
            rb.linearVelocity = new Vector2(enemy.retreatVelocity.x * -DirectionToPlayer(), enemy.retreatVelocity.y);
            enemy.HandleFlip(DirectionToPlayer());
        }
    }

    public override void Update()
    {
        base.Update();

        if (WhitInAttackRange() && enemy.PlayerDetection()) stateMachine.ChangeState(enemy.attackState);
        else enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocity.y);

        if (enemy.GroundDetected && !enemy.PlayerDetection()) stateMachine.ChangeState(enemy.idleState);
    }

    private bool WhitInAttackRange() => DistanceToPlayer() <= enemy.attackDistance;

    private float DistanceToPlayer()
    {
        if (player == null) return Mathf.Infinity;
        return Math.Abs(player.position.x - enemy.transform.position.x);
    }

    private int DirectionToPlayer()
    {
        if (player == null) return 0;
        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }

    private bool ShouldRetreat() => DistanceToPlayer() <= enemy.minRetreatDistance;
}
