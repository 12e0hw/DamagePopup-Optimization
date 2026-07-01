using UnityEngine;
using UnityEngine.InputSystem;


public class AreaAttackTester_After : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MonsterSpawner_After monsterSpawner;

    [Header("Attack Settings")]
    [SerializeField] private int minDamage = 10;
    [SerializeField] private int maxDamage = 100;
    [SerializeField] private int hitRepeatPerCast = 3;


    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            AttackAll();
        }
    }


    private void AttackAll()
    {
        var monsters = monsterSpawner.SpawnedMonsters;

        for (int repeat = 0; repeat < hitRepeatPerCast; repeat++)
        {
            for (int index = 0; index < monsters.Count; index++)
            {
                Monster_After monster = monsters[index];

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
