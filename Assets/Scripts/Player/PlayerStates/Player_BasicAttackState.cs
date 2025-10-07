using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    private static readonly int basicAttackIndexHash = Animator.StringToHash("basicAttackIndex");
    private float attackVelocityTimer;
    private float lastTimeAttacked;
    private const int FirstComboIndex = 1;
    private int currentComboIndex = 1;
    private int comboLimit = 3;
    private int attackDirection;
    private bool comboAttackQueued;

    public Player_BasicAttackState(
        Player player,
        StateMachine stateMachine,
        string animBoolName)
        : base(player, stateMachine, animBoolName)
    {
        if (player.attackVelocity.Length != comboLimit)
        {
            Debug.LogWarning("Attack velocity array length has to be the same size as combo limit. Auto changed combo limit to match array size.");
            comboLimit = player.attackVelocity.Length;
        }
    }

    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false;
        ResetComboIndexWhenCapped();

        // Set attack direction based on movement input, if no input use facing direction
        attackDirection = player.moveInput.x != 0 ? (int)Mathf.Sign(player.moveInput.x) : player.FacingDirection;

        animator.SetInteger(basicAttackIndexHash, currentComboIndex);
        ApplyAttackVelocity();
    }


    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        if (playerInput.Player.Attack.WasPressedThisFrame()) QueNextAttack();

        if (triggerCalled) HandleStateExit();
    }

    public override void Exit()
    {
        base.Exit();

        currentComboIndex++;
        lastTimeAttacked = Time.time;
    }

    private void HandleStateExit()
    {
        if (comboAttackQueued)
        {
            animator.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }
        else stateMachine.ChangeState(player.idleState);
    }

    private void QueNextAttack()
    {
        if (currentComboIndex < comboLimit) comboAttackQueued = true;
    }

    private void ResetComboIndexWhenCapped()
    {
        if (currentComboIndex > comboLimit) currentComboIndex = FirstComboIndex;
        if (Time.time - lastTimeAttacked > player.comboResetTime) currentComboIndex = FirstComboIndex;
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;
        if (attackVelocityTimer <= 0) player.SetVelocity(0, rb.linearVelocity.y);
    }

    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[currentComboIndex - 1];
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(attackVelocity.x * attackDirection, attackVelocity.y);
    }
}
