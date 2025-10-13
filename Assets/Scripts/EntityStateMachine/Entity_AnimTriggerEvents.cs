using UnityEngine;

public class Entity_AnimTriggerEvents : MonoBehaviour
{
    private Entity entity;
    private Entity_Combat entityCombat;

    protected virtual void Awake()
    {
        entity = GetComponentInParent<Entity>();
        entityCombat = GetComponentInParent<Entity_Combat>();
    }

    public void CurrentStateTrigger()
    {
        entity.CallAnimTrigger();
    }

    public void AttackTrigger()
    {
        entityCombat.PreformAttack();
    }
}
