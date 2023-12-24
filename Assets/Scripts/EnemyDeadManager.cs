using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadManager : MonoBehaviour
{
    public static EnemyDeadManager instance;
    public GameObject deadPrefab;
    private List<GameObject> EnemyDeadAnimPool = new List<GameObject>();
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
            EnemyDeadAnimPool.Add(exgo);
        }
    }

    public GameObject GetEnemyDeadEffect(Vector3 vec)
    {
        for (int i = 0; i < EnemyDeadAnimPool.Count; i++)
        {
            if (!EnemyDeadAnimPool[i].gameObject.activeInHierarchy)
            {
                EnemyDeadAnimPool[i].transform.position = vec;
                EnemyDeadAnimPool[i].SetActive(true);
                StartCoroutine(DisabledExgo(EnemyDeadAnimPool[i]));
                return EnemyDeadAnimPool[i];
            }

        }
        // ��� ���׹���������Ʈ�� Ȱ��ȭ�Ǿ� �ִٸ� ���ο� ��ü �����Ͽ� ��ȯ
        GameObject exgo = Instantiate(deadPrefab);
        exgo.SetActive(true);
        EnemyDeadAnimPool.Add(exgo);
        StartCoroutine(DisabledExgo(exgo));
        return exgo;
    }
    IEnumerator DisabledExgo(GameObject go)
    {
        yield return new WaitForSeconds(2f);
        go.SetActive(false);
    }
}
