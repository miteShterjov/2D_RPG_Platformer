using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy => GetComponent<Enemy>();
    public override void TakeDamage(float damage, Transform damageSource)
    {
        base.TakeDamage(damage, damageSource);
        if(isDead) return;
        if (damageSource.CompareTag("Player")) enemy.TryEnterBattleState(damageSource);
    }
}
