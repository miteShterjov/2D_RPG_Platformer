using UnityEngine;

public class Player_AnimTriggerEvents : MonoBehaviour
{
    private Player player;

    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public void CurrentStateTrigger()
    {
        player.CallAnimTrigger();
    }
}
