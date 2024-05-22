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

    //�����۽��� �ڷ�ƾ ����
    private Coroutine itemSpawnCoroutine;

    // ��ϵ� ��ġ���� �����ϴ� ����Ʈ
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
            // ��� ���� ����Ʈ�� ������� ��
            return -1;
        }

        // ��� ������ ���� ����Ʈ �߿��� �����ϰ� ����
        int randomIndex = Random.Range(0, availableIndices.Count);
        return availableIndices[randomIndex];
    }
}