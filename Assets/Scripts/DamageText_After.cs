using TMPro;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]

public class DamageText_After : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI damageLabel;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Animation")]
    [SerializeField] private float lifeTime = 0.6f;
    [SerializeField] private float riseSpeed = 120f;

    private RectTransform rectTransform;
    private DamageManager_After owner;

    private float timer;
    private bool isPlaying;

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


    public void Initialize(DamageManager_After manager)
    {
        owner = manager;
        isPlaying = false;
        gameObject.SetActive(false);
    }


    public void Play(int damage, Vector2 anchoredPosition)
    {
        gameObject.SetActive(true);

        damageLabel.SetText("{0}", damage);

        rectTransform.anchoredPosition = anchoredPosition;
        canvasGroup.alpha = 1f;

        timer = 0f;
        isPlaying = true;

        transform.SetAsLastSibling();
    }


    private void Update()
    {
        if (!isPlaying)
        {
            return;
        }

        timer += Time.deltaTime;

        rectTransform.anchoredPosition += Vector2.up * riseSpeed * Time.deltaTime;
        canvasGroup.alpha = 1f - timer / lifeTime;

        if (timer >= lifeTime)
        {
            ReturnToPool();
        }
    }


    private void ReturnToPool()
    {
        isPlaying = false;
        owner.Release(this);
    }
}
