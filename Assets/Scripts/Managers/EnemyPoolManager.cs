using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
	public GameObject[] enemiesPrefabs; //8��
	List<GameObject>[] enemiesPool;

	public static EnemyPoolManager instance;
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		enemiesPool = new List<GameObject>[enemiesPrefabs.Length];
		for (int i = 0; i < enemiesPool.Length; i++)
		{
			enemiesPool[i] = new List<GameObject>();
		}
		
	}
	private void Start()
	{
		for(int i=0; i<10; i++)
		{	//�߰����� Ǯ��
			for(int j=1; j<3; j++)
			{
				GameObject enemy = Instantiate(enemiesPrefabs[j], transform);
				enemy.transform.parent = this.transform;
				enemy.SetActive(false);
				enemiesPool[j].Add(enemy);
			}
		}
		for(int i=0; i<35; i++)
		{	//�Ϲ� ���׹̵� Ǯ��
			for(int j=3; j<enemiesPrefabs.Length; j++)
			{
				GameObject enemy = Instantiate(enemiesPrefabs[j], transform);
				enemy.transform.parent = this.transform;
				enemy.SetActive(false);
				enemiesPool[j].Add(enemy);
			}
		}
	}
	//0�� -> boss, 1~2��->miniboss , 3~7�� -> enemies
	public GameObject GetEnemies(int enemyindex)
	{
		GameObject selectedEnemy = null;
		foreach (GameObject enemy in enemiesPool[enemyindex])
		{
			if (!enemy.activeSelf)
			{
				selectedEnemy = enemy;
				selectedEnemy.SetActive(true);
				break;
			}
		}
		if (!selectedEnemy)
		{
			selectedEnemy = Instantiate(enemiesPrefabs[enemyindex], transform);
			selectedEnemy.transform.parent = this.transform;
			enemiesPool[enemyindex].Add(selectedEnemy);
		}
		
		return selectedEnemy;
	}

}
