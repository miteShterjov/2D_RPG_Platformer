using Unity.VisualScripting;
using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;
    protected InputSystem_Actions playerInput;

    protected Animator animator;
    protected Rigidbody2D rb;

    protected float stateTimer;
    protected bool triggerCalled;

    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;

        animator = player.animator;
        rb = player.rb;
        playerInput = player.playerInput;
    }

    public virtual void Enter()
    {
        animator.SetBool(animBoolName, true);
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        animator.SetFloat("yVelocity", rb.linearVelocity.y);

        // can enter dash from any state
        if (playerInput.Player.Sprint.WasPressedThisFrame()) stateMachine.ChangeState(player.sprintState);
    }

    public virtual void Exit()
    {
        animator.SetBool(animBoolName, false);
    }

    public void CallAnimTrigger() => triggerCalled = true; 

    private bool CanDash()
    {
        if (player.wallDetected) return false;
        if (stateMachine.currentState == player.sprintState) return false;
        return true;
    }
}
