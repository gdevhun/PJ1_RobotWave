using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;


public class GameManager : MonoBehaviour
{
	public GameObject myCursor;
	public Player player;
    public static GameManager instance = null;

	[SerializeField]
	private GameObject playerWinPanel;

	[SerializeField]
	private GameObject robotWinPanel;

	[SerializeField]
	Tilemap[] myTile;

	[SerializeField]
	private GameObject fireCircle;
	[SerializeField]
	private GameObject enemySpawner;
	[SerializeField]
	private GameObject itemSpawner;
	[SerializeField]
	private GameObject healthBar;
	[SerializeField]
	private GameObject HUD;
	
	[HideInInspector]
	public bool isFeverMode = false;
	[HideInInspector]
	public bool isGameOver = false;
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}

		Application.targetFrameRate = 60;
	}
	public void SetGameOver(bool isPlayerWin)
	{
		if (!isGameOver)
		{
			isGameOver = true;

			// 적들을 찾아서 게임 오버 상태로 설정
			Enemy[] enemies = FindObjectsOfType<Enemy>();
			foreach(Enemy enemy in enemies)
			{
				enemy.SetGameOver();
			}

			// 적 스포너의 스폰을 멈춤
			EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
			if (enemySpawner != null)
			{
				enemySpawner.StopEnemySpawning();
			}

			// 아이템 스포너의 스폰을 멈춤
			ItemSpawner itemSpawner = FindObjectOfType<ItemSpawner>();
			if (itemSpawner != null)
			{
				itemSpawner.StopItemSpawning();
			}

			if (isPlayerWin)
			{
				DestroyAllEnemies().Forget();
				ShowPlayerWinPanel().Forget();
			}
			else
			{
				ShowRobotWinPanel().Forget();
			}
		}
	}

	
	private async UniTaskVoid ShowPlayerWinPanel()
	{
		await UniTask.Delay(TimeSpan.FromSeconds(3.5));
		playerWinPanel.SetActive(true);
		Cursor.visible = true;
		myCursor.SetActive(false);
		SoundManager.instance.StopBGM();
		SoundManager.instance.PlayBGM("GameWin");
	}
	private async UniTaskVoid ShowRobotWinPanel()
	{
		await UniTask.Delay(TimeSpan.FromSeconds(2));
		FireCircle fireCircle = FindObjectOfType<FireCircle>();
		fireCircle.gameObject.SetActive(false);

		robotWinPanel.SetActive(true);
		Cursor.visible = true;
		myCursor.SetActive(false);

		SoundManager.instance.StopBGM();
		SoundManager.instance.PlayBGM("GameLose");
	}
	public void MenuSceneLoad()
	{
		SceneManager.LoadScene("MenuScene");
	}
	public void RestartGame()
	{
		if (SoundManager.instance != null)
		{
			SoundManager.instance.StopBGM();
			SoundManager.instance.PlayBGM("LobbyBGM");
		}
		SceneManager.LoadScene("GameScene");
		
	}
	public void StartGame() 
	{
		fireCircle.SetActive(true);
		enemySpawner.SetActive(true);
		itemSpawner.SetActive(true);
		healthBar.SetActive(true);
		HUD.SetActive(true);
		player.enabled = true;
	}
	private async UniTaskVoid DestroyAllEnemies()
	{
		await UniTask.Delay(TimeSpan.FromSeconds(3.5));
		Enemy[] enemies = FindObjectsOfType<Enemy>();
		foreach (Enemy enemy in enemies)
		{
			enemy.DestroySelf();
		}
	}
	public void SetFeverMode(bool enabled)
	{
		isFeverMode = enabled;
		ChangeColor(enabled);

	}
	private void ChangeColor(bool darker)
	{
		for (int i = 0; i < myTile.Length; i++)
		{
			if (darker)
			{
				myTile[i].color = new Color32(101, 103, 115, 255);
			}
			else
			{
				myTile[i].color = Color.white;
			}
		}
	}
}
