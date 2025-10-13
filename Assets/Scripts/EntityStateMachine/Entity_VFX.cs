using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    [Header("On Damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageVFXDuration = 0.2f;

    private Material originalMaterial;
    private SpriteRenderer spriteRenderer;
    private Coroutine onDamageVFXCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        originalMaterial = spriteRenderer.material;
    }

    public void PlayOnDamageVFX()
    {
        if (spriteRenderer == null || onDamageMaterial == null) return;
        if (onDamageVFXCoroutine != null)
        {
            StopCoroutine(onDamageVFXCoroutine);
            spriteRenderer.material = originalMaterial;
        }

        onDamageVFXCoroutine = StartCoroutine(RevertToOriginalMaterial());
    }

    private IEnumerator RevertToOriginalMaterial()
    {
        spriteRenderer.material = onDamageMaterial;

        yield return new WaitForSeconds(onDamageVFXDuration);

        spriteRenderer.material = originalMaterial;
    }
}
