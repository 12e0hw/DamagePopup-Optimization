using UnityEngine;
using TMPro;

public class SimpleMoveUp : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private float alpha = 1f;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // UI 스크린 좌표계 기준이므로 위로 서서히 이동
        transform.Translate(Vector3.up * 150f * Time.deltaTime);

        // 페이드 아웃 연출
        if (textMesh != null)
        {
            alpha -= Time.deltaTime;
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, alpha);
        }
    }
}