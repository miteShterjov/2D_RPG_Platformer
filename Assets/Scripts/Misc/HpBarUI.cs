using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour
{
    [Header("HpBar Properties")]
    [SerializeField] private Image background;
    [SerializeField] private Image midground;
    [SerializeField] private Image foreground;
    [Space]
    [SerializeField] private float reduceSpeed = 2f;
    [SerializeField] private float midDelaySpeed = 0.5f;

    private float target;
    private Entity entity;

    void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    void Start()
    {
        EnableHpBar(false);
    }

    void Update()
    {
        foreground.fillAmount = Mathf.MoveTowards(foreground.fillAmount, target, reduceSpeed * Time.deltaTime);
        foreground.color = Color.Lerp(Color.red, Color.green, foreground.fillAmount);
        midground.fillAmount = Mathf.MoveTowards(midground.fillAmount, target, midDelaySpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        if (entity == null) return;
        entity.OnEntityFlip += Entity_OnEntityFlip;
    }

    private void OnDisable()
    {
        if (entity == null) return;
        entity.OnEntityFlip -= Entity_OnEntityFlip;
    }

    private void Entity_OnEntityFlip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void EnableHpBar(bool enable) => gameObject.SetActive(enable);
    
    public void UpdateHpBar(float currentHealth, float maxHealth) => target = currentHealth / maxHealth;
}
