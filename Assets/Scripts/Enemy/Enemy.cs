using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_BattleState battleState;
    public Enemy_GroundedState groundedState;

    [Header("Movement")]
    [SerializeField] public float moveSpeed = 1.5f;
    [SerializeField] public float idleTime = 2f;
    [Range(0, 2)]
    [SerializeField] public float moveAnimSpeedMultiplier = 1f;
    [Header("Player Detection")]
    [SerializeField] private Transform playerCheck;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] public float playerCheckRadius = 10f;
    [Header("Battle State")]
    [SerializeField] public float battleMoveSpeed = 3f;
    [SerializeField] public float attackDistance = 1f;
    [SerializeField] public float minRetreatDistance = 1f;
    [SerializeField] public Vector2 retreatVelocity;


    public RaycastHit2D PlayerDetection()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            playerCheck.position,
            Vector2.right * FacingDirection,
            playerCheckRadius,
            playerLayer | groundLayer
            );

        if (hit.collider != null && hit.collider.gameObject.layer != LayerMask.NameToLayer("Player")) return default;

        return hit;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(
            playerCheck.position,
            new Vector3(playerCheck.position.x + (FacingDirection * playerCheckRadius), playerCheck.position.y)
        );

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(
            transform.position,
            new Vector3(transform.position.x + (FacingDirection * attackDistance), transform.position.y)
        );

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(
            transform.position,
            new Vector3(transform.position.x + (FacingDirection * minRetreatDistance), transform.position.y)
        );
    }
}
