using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private LayerMask whatIsTarget;
    [SerializeField] private float targetCheckRadius = 1f;
    [Header("Attack")]
    [SerializeField] private float damage = 10f;

    private Entity_VFX entityVFX;

    private void Awake()
    {
        entityVFX = GetComponent<Entity_VFX>();
    }

    public void PreformAttack()
    {
        foreach (Collider2D collider in GetDetectedColliders())
        {
            IDamagable damagable = collider.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(damage, transform);
                entityVFX?.PlayOnHitVFX(collider.transform);
                continue;
            }

            Entity_Health targetHealth = collider.GetComponent<Entity_Health>();
            targetHealth?.TakeDamage(damage, transform);
            entityVFX?.PlayOnHitVFX(collider.transform);
        }
    }

    protected Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
