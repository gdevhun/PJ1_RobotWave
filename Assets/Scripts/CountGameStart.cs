using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CountGameStart : MonoBehaviour
{
	TextMeshProUGUI countdownText;
    // Start is called before the first frame update
    void Start()
    {
        countdownText = GetComponent<TextMeshProUGUI>();
		StartCoroutine(CountSecond());
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
		GameManager.instance.StartGame();
		gameObject.SetActive(false);

	}
	
}
