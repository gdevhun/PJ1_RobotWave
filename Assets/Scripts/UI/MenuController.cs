using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class MenuController : MonoBehaviour
{
	public TextMeshProUGUI countdownText;

	private void Awake()
	{
		Screen.SetResolution(1920, 1080, true);
	}
	private void Start()
	{
		SoundManager.instance.PlayBGM("LobbyBGM");
	}
	IEnumerator CountSecond() //ī��Ʈ���� �ڷ�ƾ�Լ�
	{
		int mySec = 3;
		countdownText.gameObject.SetActive(true);
		while (mySec > 0)
		{
			countdownText.text = mySec.ToString() + "�� �� ���ӽ���!"; // ���ڸ� ���ڿ��� ��ȯ�Ͽ� �ſ� ǥ��
			yield return new WaitForSeconds(1); // 1�� ���
			mySec--;
		}
		
		SceneManager.LoadScene("GameScene");
	}

	public void StartGame()
	{
		SceneManager.LoadScene("GameScene");
	}
	public void ExplainGame()
	{
		SceneManager.LoadScene("ExplainScene");
	}
	public void ExitGame()
	{
		Application.Quit();
	}
}
