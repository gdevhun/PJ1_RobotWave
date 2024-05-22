using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] items;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private float spawnInterval;

    //아이템스폰 코루틴 선언
    private Coroutine itemSpawnCoroutine;

    // 기록된 위치들을 저장하는 리스트
    private List<Vector2> usedSpawnPositions = new List<Vector2>();


    private void Start()
    {
        StartItemSpawning();
    }

    private void StartItemSpawning()
    {
        itemSpawnCoroutine = StartCoroutine(SpawnItemRoutine());
    }

    public void StopItemSpawning()
    {
        if (itemSpawnCoroutine != null)
        {
            StopCoroutine(itemSpawnCoroutine);
        }
    }

    private IEnumerator SpawnItemRoutine()
    {
        yield return new WaitForSeconds(spawnInterval);

        while (true)
        {
            SpawnItem();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnItem()
    {
        //int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        //int itemIndex = Random.Range(0, items.Length);
        //Instantiate(items[itemIndex], spawnPoints[spawnPointIndex].position, Quaternion.identity);

        int spawnPointIndex = FindAvailableSpawnPointIndex();
        if (spawnPointIndex != -1)
        {
            int itemIndex = Random.Range(0, items.Length);
            Instantiate(items[itemIndex], spawnPoints[spawnPointIndex].position, Quaternion.identity);
            usedSpawnPositions.Add(spawnPoints[spawnPointIndex].position);
        }
    }
    private int FindAvailableSpawnPointIndex()
    {
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!usedSpawnPositions.Contains(spawnPoints[i].position))
            {
                availableIndices.Add(i);
            }
        }

        if (availableIndices.Count == 0)
        {
            // 모든 스폰 포인트가 사용중일 때
            return -1;
        }

        // 사용 가능한 스폰 포인트 중에서 랜덤하게 선택
        int randomIndex = Random.Range(0, availableIndices.Count);
        return availableIndices[randomIndex];
    }
}