using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    [Header("On Taking Damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageVFXDuration = 0.2f;
    [Header("On Doing Damage VFX")]
    [SerializeField] private GameObject onHitVFX;
    [SerializeField] private Color onHitVFXColor = Color.white;

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

    public void PlayOnHitVFX(Transform target)
    {
        GameObject onHitVFXInstance = Instantiate(onHitVFX, target.position, Quaternion.identity);
        
        SpriteRenderer vfxSpriteRenderer = onHitVFXInstance.GetComponentInChildren<SpriteRenderer>();
        if (vfxSpriteRenderer != null)
        {
            vfxSpriteRenderer.color = onHitVFXColor;
        }
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
