using UnityEngine;

public class Enemy_AnimTriggers : Entity_AnimTriggerEvents
{
    private Enemy enemy;
    private Enemy_VFX enemyVFX;
    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponentInParent<Enemy>();
        enemyVFX = GetComponentInParent<Enemy_VFX>();
    }
    private void EnableCounterWindow()
    {
        enemy.EnableCounterWindow(true);
        enemyVFX.EnableAttackAlert(true);
    }

    private void DisableCounterWindow()
    {
        enemy.EnableCounterWindow(false);
        enemyVFX.EnableAttackAlert(false);
    }
}
