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
	IEnumerator CountSecond() //카운트세는 코루틴함수
	{
		int mySec = 3;
		countdownText.gameObject.SetActive(true);
		while (mySec > 0)
		{
			countdownText.text = mySec.ToString() + "초 후 게임시작!"; // 숫자를 문자열로 변환하여 신에 표시
			yield return new WaitForSeconds(1); // 1초 대기
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
