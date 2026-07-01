using System;
using UnityEngine;


public class Monster_After : MonoBehaviour
{
    public event Action<int, Vector3, int> OnDamaged;

    [Header("Status")]
    [SerializeField] private int hp = 999999;

    [Header("Popup")]
    [SerializeField] private float popupHeight = 2.0f;

    public void TakeDamage(int damage, int hitIndex = 0)
    {
        hp -= damage;

        Vector3 popupPosition = transform.position + Vector3.up * popupHeight;

        OnDamaged?.Invoke(damage, popupPosition, hitIndex);
    }
}
