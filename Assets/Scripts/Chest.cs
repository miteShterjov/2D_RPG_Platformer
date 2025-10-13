using UnityEngine;

public class Chest : MonoBehaviour, IDamagable
{
    public static readonly string ChestHASH = "chestOpen";

    [Header("Open Details")]
    [SerializeField] private Vector2 knockback;

    private Animator animator => GetComponentInChildren<Animator>();
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private Entity_VFX fx => GetComponent<Entity_VFX>();

    public void TakeDamage(float damage, Transform damageSource)
    {
        fx.PlayOnDamageVFX();
        animator.SetBool(ChestHASH, true);
        rb.linearVelocity = knockback;

        rb.angularVelocity = Random.Range(-200f, 200f);

        // drop items to do 
    }
}
