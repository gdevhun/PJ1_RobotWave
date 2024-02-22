using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Explain : MonoBehaviour
{
	[SerializeField]
	private AudioSource TypingSound;
	public TextMeshProUGUI[] mytext;
	public string message;
	public Image[] images;
	public TextMeshProUGUI countdownText;

	public int messagenum = 0;
	void Awake()
	{
		for (int i = 0; i < mytext.Length; i++)
		{
			mytext[i].gameObject.SetActive(false);
		}
		for (int i = 0; i < images.Length; i++)
		{
			images[i].gameObject.SetActive(false);
		}
	}
	void Start()
	{
		StartCoroutine(ShowTextSequence());

	}
	IEnumerator ShowTextSequence()
	{
		// 각 TextMeshProUGUI 요소를 순서대로 보이게 하기 위해 루프 사용
		for (int i = 0; i < mytext.Length; i++)
		{
			mytext[i].gameObject.SetActive(true); // TextMeshProUGUI 활성화
			yield return StartCoroutine(Typing(mytext[i])); // 타이핑 코루틴 호출
			messagenum++; //차례로 이미지를 띄우기위한 로직변수
		}
		//모든 이미지와 타이핑 로직이 끝나면 카운트 코루틴 실행.
		yield return StartCoroutine(CountSecond());

		SceneManager.LoadScene("GameScene"); //카운트가1이되면 게임신으로 전환.
	}
	IEnumerator CountSecond() //카운트세는 코루틴함수
	{
		int mySec = 5;

		while (mySec > 0)
		{
			countdownText.text = mySec.ToString() + "초 뒤 게임시작!"; // 숫자를 문자열로 변환하여 신에 표시
			yield return new WaitForSeconds(1); // 1초 대기
			mySec--;
		}
	}
	IEnumerator Typing(TextMeshProUGUI mytext)
	{
		message = mytext.text;
		mytext.text = "";
		if (messagenum == 1)
		{
			OnImage(images[0]);
			yield return new WaitForSeconds(0.5f);
			OnImage(images[1]);
			yield return new WaitForSeconds(0.5f);
		}
		if (messagenum >= 2)
		{
			OnImage(images[messagenum]);
		}

		for (int i = 0; i < message.Length; i++)
		{
			mytext.text += message[i];
			yield return new WaitForSeconds(0.1f);
			TypingSound.Play();
		}
		yield return new WaitForSeconds(1.5f);
	}
	public void OnImage(Image image)
	{
		if (image != null)
		{   //이미지 활성화 함수
			image.gameObject.SetActive(true);
			SoundManager.instance.PlaySFX("Button");
		}
	}
}