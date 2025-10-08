using UnityEngine;

public class Entity_AnimTriggerEvents : MonoBehaviour
{
    private Entity entity;

    void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    public void CurrentStateTrigger()
    {
        entity.CallAnimTrigger();
    }
}
