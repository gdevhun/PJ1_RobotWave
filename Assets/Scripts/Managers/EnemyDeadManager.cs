using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemyDeadManager : MonoBehaviour
{
    public static EnemyDeadManager instance;
    public GameObject deadPrefab;
    private List<GameObject> enemyDeadAnimPool = new List<GameObject>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
	private void Start()
	{
        for (int i = 0; i < 25; i++)
        {
            GameObject exgo = Instantiate(deadPrefab);
            exgo.transform.parent = this.transform;
            exgo.SetActive(false);
            enemyDeadAnimPool.Add(exgo);
        }
    }

    public GameObject GetEnemyDeadEffect(Vector3 vec)
    {
        for (int i = 0; i < enemyDeadAnimPool.Count; i++)
        {
            if (!enemyDeadAnimPool[i].gameObject.activeInHierarchy)
            {
                enemyDeadAnimPool[i].transform.position = vec;
                enemyDeadAnimPool[i].SetActive(true);
                DisableExgo(enemyDeadAnimPool[i]).Forget();
                return enemyDeadAnimPool[i];
            }

        }
        
        // ��� ���׹���������Ʈ�� Ȱ��ȭ�Ǿ� �ִٸ� ���ο� ��ü �����Ͽ� ��ȯ
        GameObject exgo = Instantiate(deadPrefab);
        exgo.SetActive(true);
        enemyDeadAnimPool.Add(exgo);
        DisableExgo(exgo).Forget();
        return exgo;
    }
    private async UniTaskVoid DisableExgo(GameObject go)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        go.SetActive(false);
    }
}
