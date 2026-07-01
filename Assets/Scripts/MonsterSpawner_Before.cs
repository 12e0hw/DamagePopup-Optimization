using System.Collections.Generic;
using UnityEngine;


public class MonsterSpawner_Before : MonoBehaviour
{
    [Header("Prefab")]

    [SerializeField] private Monster_Before monsterPrefab;

    [Header("References")]
    [SerializeField] private DamagePopupSpawner_Before damagePopupSpawner;

    [Header("Spawn Settings")]
    [SerializeField] private int monsterCount = 49;


    [SerializeField] private float spawnRangeX = 7f;
    [SerializeField] private float spawnRangeZ = 7f;
    [SerializeField] private float spawnY = 0.5f;

    [SerializeField] private float minDistance = 1.2f;
    [SerializeField] private int maxTryCount = 100;


    private readonly List<Monster_Before> spawnedMonsters = new List<Monster_Before>();
    public IReadOnlyList<Monster_Before> SpawnedMonsters => spawnedMonsters;


    private void Start()
    {
        Spawn();
    }


    private void Spawn()
    {
        List<Vector3> usedPositions = new List<Vector3>();

        for (int i = 0; i < monsterCount; i++)
        {

            Vector3 position = GetRandomPosition(usedPositions);
            usedPositions.Add(position);

            Quaternion randomRotation = Quaternion.Euler(
                0f,
                Random.Range(0f, 360f),
                0f
            );

            Monster_Before monster = Instantiate(
                monsterPrefab,
                position,
                randomRotation
            );

            monster.Initialize(damagePopupSpawner);

            spawnedMonsters.Add(monster);
        }
    }


    private Vector3 GetRandomPosition(List<Vector3> usedPositions)
    {

        for (int tryCount = 0; tryCount < maxTryCount; tryCount++)
        {

            float randomX = Random.Range(-spawnRangeX, spawnRangeX);
            float randomZ = Random.Range(-spawnRangeZ, spawnRangeZ);

            Vector3 position = transform.position + new Vector3(
                randomX,
                spawnY,
                randomZ
            );

            if (IsFarEnough(position, usedPositions))
            {
                return position;
            }
        }


        float fallbackX = Random.Range(-spawnRangeX, spawnRangeX);
        float fallbackZ = Random.Range(-spawnRangeZ, spawnRangeZ);

        return transform.position + new Vector3(
            fallbackX,
            spawnY,
            fallbackZ
        );
    }


    private bool IsFarEnough(Vector3 position, List<Vector3> usedPositions)
    {

        foreach (Vector3 usedPosition in usedPositions)
        {
            float distance = Vector3.Distance(position, usedPosition);

            if (distance < minDistance)
            {
                return false;
            }
        }


        return true;
    }
}
