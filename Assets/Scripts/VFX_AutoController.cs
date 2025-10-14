using UnityEngine;

public class VFX_AutoController : MonoBehaviour
{
    [Header("Effects parameters")]
    [SerializeField] private bool enableAutoDestroy = true;
    [Range(0.1f, 1f)]
    [SerializeField] private float lifeTime = 0.5f;
    [Header("Transform parameters")]
    [SerializeField] private bool enableRandomOffset = true;
    [SerializeField] private Vector2 offsetRangeX = new Vector2(-0.5f, 0.5f);
    [SerializeField] private Vector2 offsetRangeY = new Vector2(-0.5f, 0.5f);
    [Space]
    [SerializeField] private bool enableRandomRotation = true;
    [SerializeField] private Vector2 rotationRange = new Vector2(0f, 360f);
    [Space]
    [SerializeField] private bool enableRandomScale = true;
    [SerializeField] private Vector2 scaleRange = new Vector2(1f, 3f);

    void Start()
    {
        ApplyDestroyObject();
        ApplyRandomOffset();
        ApplyRandomRotation();
        ApplyRandomScale();
    }

    private void ApplyDestroyObject()
    {
        if (!enableAutoDestroy) return;
        Destroy(gameObject, lifeTime);
    }

    private void ApplyRandomOffset()
    {
        if (!enableRandomOffset) return;
        float offsetX = Random.Range(offsetRangeX.x, offsetRangeX.y);
        float offsetY = Random.Range(offsetRangeY.x, offsetRangeY.y);
        transform.position += new Vector3(offsetX, offsetY, 0f);
    }

    private void ApplyRandomRotation()
    {
        if (!enableRandomRotation) return;
        float randomZRotation = Random.Range(rotationRange.x, rotationRange.y);
        transform.rotation = Quaternion.Euler(0f, 0f, randomZRotation);
    }

    private void ApplyRandomScale()
    {
        if (!enableRandomScale) return;
        float randomScale = Random.Range(scaleRange.x, scaleRange.y);
        transform.localScale = new Vector3(randomScale, randomScale, 1f);
    }
}
