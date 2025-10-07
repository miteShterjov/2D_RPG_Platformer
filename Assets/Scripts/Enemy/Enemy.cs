using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;

    [Header("Movement")]
    [SerializeField] public float moveSpeed = 1.5f;
    [SerializeField] public float idleTime = 2f;
}
