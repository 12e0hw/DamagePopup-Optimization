using TMPro;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]

public class DamagePopup_Before : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI damageLabel;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Animation")]
    [SerializeField] private float lifeTime = 0.6f;
    [SerializeField] private float riseSpeed = 120f;


    private RectTransform rectTransform;
    private float timer;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        if (damageLabel == null)
        {
            damageLabel = GetComponentInChildren<TextMeshProUGUI>();
        }
    }


    public void Setup(int damage, Vector2 anchoredPosition)
    {
        damageLabel.text = damage.ToString();
        rectTransform.anchoredPosition = anchoredPosition;
        canvasGroup.alpha = 1f;
        timer = 0f;
    }


    private void Update()
    {
        timer += Time.deltaTime;

        rectTransform.anchoredPosition += Vector2.up * riseSpeed * Time.deltaTime;
        canvasGroup.alpha = 1f - timer / lifeTime;

        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
