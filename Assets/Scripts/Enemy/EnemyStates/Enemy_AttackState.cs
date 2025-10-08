using UnityEngine;

public class Enemy_AttackState : Enemy_GroundedState
{
    public Enemy_AttackState(
        Enemy enemy,
        StateMachine stateMachine,
        string animBoolName)
        : base(enemy, stateMachine, animBoolName) { }

    public override void Update()
    {
        base.Update();

        if (triggerCalled) stateMachine.ChangeState(enemy.idleState);
    }
}
