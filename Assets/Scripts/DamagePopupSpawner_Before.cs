using UnityEngine;

public class DamagePopupSpawner_Before : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private DamagePopup_Before damagePopupPrefab;

    [Header("Canvas")]
    [SerializeField] private Canvas damageCanvas;
    [SerializeField] private RectTransform canvasRect;

    [Header("Camera")]
    [SerializeField] private Camera worldCamera;

    [Header("Popup Offset")]
    [SerializeField] private float yOffsetStep = 15f;


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
    }


    public void ShowDamage(int damage, Vector3 worldPosition, int hitIndex = 0)
    {
        Vector2 canvasPosition;

        if (!WorldToCanvasPosition(worldPosition, out canvasPosition))
        {
            return;
        }

        canvasPosition += new Vector2(0f, yOffsetStep * hitIndex);

        DamagePopup_Before popup = Instantiate(damagePopupPrefab, canvasRect);

        popup.Setup(damage, canvasPosition);
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
}
