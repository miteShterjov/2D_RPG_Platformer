using UnityEngine;

public class Enemy_VFX : Entity_VFX
{
    [SerializeField] private GameObject counterAttackAlert;

    public void EnableAttackAlert(bool enable) => counterAttackAlert.SetActive(enable);
}
