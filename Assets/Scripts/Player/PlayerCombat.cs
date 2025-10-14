using UnityEngine;

public class PlayerCombat : Entity_Combat
{
    [Header("Counter Attack")]
    [SerializeField] private float counterRecovery;
    public bool CounterAttackPerformed()
    {
        bool hasPreformedCounter = false;

        foreach (var target in GetDetectedColliders())
        {
            ICounterable counterable = target.GetComponent<ICounterable>();

            if (counterable == null) continue;

            if (counterable.CanBeCountered)
            {
                counterable.HandleCounter();
                hasPreformedCounter = true;
            }
        }

        return hasPreformedCounter;
    }
    public float GetCounterDuration() => counterRecovery;
}
