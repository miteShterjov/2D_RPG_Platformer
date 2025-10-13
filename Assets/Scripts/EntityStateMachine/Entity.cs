using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public bool GroundDetected { get; private set; }
    public bool WallDetected { get; private set; }
    public int FacingDirection { get; private set; } = 1;

    [Header("Collision Detection")]
    [SerializeField] private float groundCheckDistance = 1.4f;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform primaryWallCheck;
    [SerializeField] private Transform secondaryWallCheck;

    protected StateMachine stateMachine;
    private bool isFacingRight = true;
    private bool isKnocked;
    private Coroutine knockbackCoroutine;

    protected virtual void Awake()
    {
        // execution order is important here
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
        // print("Current active state: " + stateMachine.currentState);
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        if (isKnocked) return;
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    public void CallAnimTrigger() => stateMachine.currentState.CallAnimTrigger();

    public void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        isFacingRight = !isFacingRight;
        FacingDirection *= -1;
    }

    public void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && !isFacingRight) Flip();
        else if (xVelocity < 0 && isFacingRight) Flip();
    }

    public void RecevieKnockback(float knockbackDuration, Vector2 knockbackForce)
    {
        if (knockbackCoroutine != null) StopCoroutine(knockbackCoroutine);
        knockbackCoroutine = StartCoroutine(KnockbackCoroutine(knockbackDuration, knockbackForce));
    }

    public virtual void EntityDeath()
    {
        
    }

    private IEnumerator KnockbackCoroutine(float knockbackDuration, Vector2 knockbackForce)
    {
        isKnocked = true;
        rb.linearVelocity = knockbackForce;

        yield return new WaitForSeconds(knockbackDuration);

        rb.linearVelocity = Vector2.zero;

        isKnocked = false;
    }

    private void HandleCollisionDetection()
    {
        GroundDetected = Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            groundCheckDistance,
            groundLayer
            );

        if (secondaryWallCheck != null)
        {
            WallDetected = Physics2D.Raycast(
                primaryWallCheck.position,
                Vector2.right * FacingDirection,
                wallCheckDistance,
                groundLayer)
            && Physics2D.Raycast(
                secondaryWallCheck.position,
                Vector2.right * FacingDirection,
                wallCheckDistance,
                groundLayer);
        }
        else
        {
            WallDetected = Physics2D.Raycast(
                primaryWallCheck.position,
                Vector2.right * FacingDirection,
                wallCheckDistance,
                groundLayer);
        }
    }
    
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + new Vector3(0, -groundCheckDistance, 0));
        Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + new Vector3(wallCheckDistance * FacingDirection, 0, 0));
        if (secondaryWallCheck != null)
            Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + new Vector3(wallCheckDistance * FacingDirection, 0, 0));
    }
}
