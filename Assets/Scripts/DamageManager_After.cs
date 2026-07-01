using System.Collections.Generic;
using UnityEngine;


public class DamageManager_After : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField] private DamageText_After damageTextPrefab;
    [SerializeField] private int initialPoolSize = 300;

    [Header("Canvas")]
    [SerializeField] private Canvas damageCanvas;
    [SerializeField] private RectTransform canvasRect;

    [Header("Camera")]
    [SerializeField] private Camera worldCamera;

    [Header("Popup Offset")]
    [SerializeField] private float yOffsetStep = 15f;

    private readonly Queue<DamageText_After> pool = new Queue<DamageText_After>();
    private readonly List<Monster_After> registeredMonsters = new List<Monster_After>();

    private void Awake()
    {
        if (worldCamera == null)
        {
            worldCamera = Camera.main;
        }

        if (damageCanvas != null && canvasRect == null)
        {
            canvasRect = damageCanvas.GetComponent<RectTransform>();
        }

        PrewarmPool();
    }

    private void PrewarmPool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            DamageText_After damageText = CreateDamageText();
            pool.Enqueue(damageText);
        }
    }


    private DamageText_After CreateDamageText()
    {
        DamageText_After damageText = Instantiate(damageTextPrefab, canvasRect);
        damageText.Initialize(this);
        return damageText;
    }


    public void RegisterMonster(Monster_After monster)
    {
        if (monster == null)
        {
            return;
        }

        if (registeredMonsters.Contains(monster))
        {
            return;
        }

        registeredMonsters.Add(monster);
        monster.OnDamaged += ShowDamage;
    }


    public void UnregisterMonster(Monster_After monster)
    {
        if (monster == null)
        {
            return;
        }

        if (!registeredMonsters.Remove(monster))
        {
            return;
        }

        monster.OnDamaged -= ShowDamage;
    }


    private void ShowDamage(int damage, Vector3 worldPosition, int hitIndex)
    {
        Vector2 canvasPosition;

        if (!WorldToCanvasPosition(worldPosition, out canvasPosition))
        {
            return;
        }

        canvasPosition += new Vector2(0f, yOffsetStep * hitIndex);

        DamageText_After damageText = GetFromPool();
        damageText.Play(damage, canvasPosition);
    }


    private DamageText_After GetFromPool()
    {
        if (pool.Count > 0)
        {
            return pool.Dequeue();
        }

        return CreateDamageText();
    }


    public void Release(DamageText_After damageText)
    {
        if (damageText == null)
        {
            return;
        }

        damageText.gameObject.SetActive(false);
        pool.Enqueue(damageText);
    }


    private bool WorldToCanvasPosition(Vector3 worldPosition, out Vector2 canvasPosition)
    {
        Vector3 screenPosition = worldCamera.WorldToScreenPoint(worldPosition);

        if (screenPosition.z < 0f)
        {
            canvasPosition = Vector2.zero;
            return false;
        }

        Camera uiCamera = damageCanvas.renderMode == RenderMode.ScreenSpaceOverlay
            ? null
            : damageCanvas.worldCamera;

        return RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPosition,
            uiCamera,
            out canvasPosition
        );
    }


    private void OnDestroy()
    {
        for (int i = 0; i < registeredMonsters.Count; i++)
        {
            Monster_After monster = registeredMonsters[i];

            if (monster != null)
            {
                monster.OnDamaged -= ShowDamage;
            }
        }

        registeredMonsters.Clear();
    }
}
