using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //public BossEmerged bossEmerged;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private float spawnInterval;

    private Coroutine enemySpawnCoroutine;

    bool MiniBossEmerged = false;  // 스폰 for문 탈출을위한 불 값 ->ui텍스트한번만보여주기위해

    private void MiniBossisEmerged()
	{
        UIManager.instance.NotifyMiniBossText();
        MiniBossEmerged = true;
	}

    private void Start()
    {
        StartEnemySpawning();
    }

    private void StartEnemySpawning()
    {
        enemySpawnCoroutine = StartCoroutine(SpawnEnemyRoutine());
    }

    public void StopEnemySpawning()
    {
        if (enemySpawnCoroutine != null)
        {
            StopCoroutine(enemySpawnCoroutine);
        }
    }

    private IEnumerator SpawnEnemyRoutine()
    {
     
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 10; i++)  
        {

            for (int j = 0; j < 14; j++) 
            {

                if (i > 7) 
				{
                    if (!MiniBossEmerged)
					{
                        MiniBossisEmerged();
						SpawnMiniBoss();
					}
					else
					{
                        SpawnMiniBoss();
                        yield return new WaitForSeconds(2f);
                    }
                }
                SpawnEnemy();
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        yield return new WaitForSeconds(10f);

        
        UIManager.instance.NotifyBossText();
        SpawnBoss();

        for (int k=0; k<5; k++)
		{
            for(int t=0; t<10; t++)
			{
                SpawnEnemy();
			}
            yield return new WaitForSeconds(spawnInterval-4);
            SpawnMiniBoss();
		}
    }

    //int index= Random.Range(1,3) 은 1과 2 리턴
    private void SpawnBoss()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        GameObject boss = EnemyPoolManager.instance.GetEnemies(0);
        boss.gameObject.transform.SetPositionAndRotation(spawnPoints[spawnPointIndex].position, 
            Quaternion.identity);
    }
    private void SpawnMiniBoss()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int minibossIndex = Random.Range(1, 3);
        GameObject miniboss = EnemyPoolManager.instance.GetEnemies(minibossIndex);
        miniboss.gameObject.transform.SetPositionAndRotation(spawnPoints[spawnPointIndex].position,
            Quaternion.identity);
    }

    private void SpawnEnemy()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int enemyIndex = Random.Range(3, 8);
        GameObject enemy= EnemyPoolManager.instance.GetEnemies(enemyIndex);
        enemy.gameObject.transform.SetPositionAndRotation(spawnPoints[spawnPointIndex].position,
            Quaternion.identity);
    }
}