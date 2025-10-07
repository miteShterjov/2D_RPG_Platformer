using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player player;
    protected InputSystem_Actions playerInput;

    public PlayerState(
        Player player,
        StateMachine stateMachine,
        string animBoolName
        ) : base(stateMachine, animBoolName)
    {
        this.player = player;

        animator = player.animator;
        rb = player.rb;
        playerInput = player.playerInput;
    }

    public override void Update()
    {
        base.Update();

        animator.SetFloat("yVelocity", rb.linearVelocity.y);

        if (playerInput == null) playerInput = player.playerInput;
        if (playerInput == null) Debug.LogWarning("PlayerState->Update: playerInput is null");

        // var playerMap = playerInput.Player;
        // if (object.ReferenceEquals(playerMap, null)) return;

        // var sprintAction = playerMap.Sprint;
        // if (sprintAction != null && sprintAction.WasPressedThisFrame() && CanDash())
        //     stateMachine.ChangeState(player.sprintState);

        if (playerInput.Player.Sprint.WasPressedThisFrame()) stateMachine.ChangeState(player.sprintState);
    }

    private bool CanDash()
    {
        if (player.WallDetected) return false;
        if (stateMachine.currentState == player.sprintState) return false;
        return true;
    }
}
