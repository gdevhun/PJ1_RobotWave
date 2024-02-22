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
		GameManager.instance.StartGame();
		gameObject.SetActive(false);

	}
	
}
