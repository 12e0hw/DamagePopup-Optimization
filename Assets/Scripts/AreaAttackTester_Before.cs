using UnityEngine;
using UnityEngine.InputSystem;


public class AreaAttackTester_Before : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private int minDamage = 10;
    [SerializeField] private int maxDamage = 100;
    [SerializeField] private int hitRepeatPerCast = 3;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            AttackAll();
        }
    }


    void AttackAll()
    {
        Monster_Before[] monsters = FindObjectsByType<Monster_Before>(FindObjectsSortMode.None);

        for (int repeat = 0; repeat < hitRepeatPerCast; repeat++)
        {
            for (int index = 0; index < monsters.Length; index++)
            {
                Monster_Before monster = monsters[index];

                if (monster == null)
                {
                    continue;
                }

                int damage = Random.Range(minDamage, maxDamage + 1);
                monster.TakeDamage(damage, repeat);
            }
        }
    }
}
