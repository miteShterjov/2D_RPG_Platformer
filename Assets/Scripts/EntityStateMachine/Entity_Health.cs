using System;
using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected float currentHealth;
    [SerializeField] protected bool isDead = false;
    [Header("On Damage Knockback")]
    [SerializeField] private float heavyKnockbackDuration = 0.5f;
    [SerializeField] private float knockbackDuration = 0.2f;
    [SerializeField] private Vector2 knockbackForce = new Vector2(2.5f, 2.5f);
    [SerializeField] private Vector2 heavyKnockbackForce = new Vector2(7f, 7f);
    [Header("On Heavy Damage")]
    [SerializeField] private float heavyDamageThreshold = .3f;
    [Header("HpBarUI")]
    [SerializeField] private GameObject hpBarUIPrefab;


    private Entity_VFX entityVFX;
    private Entity entity;
    private GameObject hpBarUIInstance;

    protected virtual void Awake()
    {
        entityVFX = GetComponent<Entity_VFX>();
        entity = GetComponent<Entity>();
    }

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        hpBarUIInstance = Instantiate(
            hpBarUIPrefab,
            transform.position + new Vector3(0, 1, 0),
            Quaternion.identity, transform);

    }

    protected virtual void Update()
    {
        if (currentHealth != maxHealth) hpBarUIInstance?.GetComponent<HpBarUI>().EnableHpBar(true);
    }

    public virtual void TakeDamage(float damage, Transform damageSource)
    {
        if (isDead) return;
        entityVFX?.PlayOnDamageVFX();
        Vector2 knockback = CalcKnockbackDirection(damage, damageSource);
        float knockbackDuration = CalcKnockbackDuration(damage);
        entity?.RecevieKnockback(knockbackDuration, knockback);
        ReduceHP(damage);
        Debug.Log(gameObject.name + " took " + damage + " damage. Current HP: " + currentHealth);
    }

    private void ReduceHP(float damage)
    {
        currentHealth -= damage;
        hpBarUIInstance?.GetComponent<HpBarUI>().UpdateHpBar(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        entity.EntityDeath();
    }

    private Vector2 CalcKnockbackDirection(float damage,Transform damageSource)
    {
        int direction = transform.position.x - damageSource.position.x > 0 ? 1 : -1;
        Vector2 knockback = isCritDamage(damage) ? heavyKnockbackForce : knockbackForce;

        knockback.x *= direction;

        return knockback;
    }

    private bool isCritDamage(float damage) => damage / maxHealth >= heavyDamageThreshold;
    private float CalcKnockbackDuration(float damage) => isCritDamage(damage) ? heavyKnockbackDuration : knockbackDuration;
    
}
