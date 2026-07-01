using UnityEngine;


public class Monster_Before : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private int hp = 999999;

    [Header("Popup")]
    [SerializeField] private float popupHeight = 2.0f;


    private DamagePopupSpawner_Before damagePopupSpawner;

    public void Initialize(DamagePopupSpawner_Before spawner)
    {
        damagePopupSpawner = spawner;
    }


    public void TakeDamage(int damage, int hitIndex = 0)
    {
        hp -= damage;

        Vector3 popupPosition = transform.position + Vector3.up * popupHeight;

        damagePopupSpawner.ShowDamage(damage, popupPosition, hitIndex);
    }
}
