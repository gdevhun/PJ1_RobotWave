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
		// �� TextMeshProUGUI ��Ҹ� ������� ���̰� �ϱ� ���� ���� ���
		for (int i = 0; i < mytext.Length; i++)
		{
			mytext[i].gameObject.SetActive(true); // TextMeshProUGUI Ȱ��ȭ
			yield return StartCoroutine(Typing(mytext[i])); // Ÿ���� �ڷ�ƾ ȣ��
			messagenum++; //���ʷ� �̹����� �������� ��������
		}
		//��� �̹����� Ÿ���� ������ ������ ī��Ʈ �ڷ�ƾ ����.
		yield return StartCoroutine(CountSecond());

		SceneManager.LoadScene("GameScene"); //ī��Ʈ��1�̵Ǹ� ���ӽ����� ��ȯ.
	}
	IEnumerator CountSecond() //ī��Ʈ���� �ڷ�ƾ�Լ�
	{
		int mySec = 5;

		while (mySec > 0)
		{
			countdownText.text = mySec.ToString() + "�� �� ���ӽ���!"; // ���ڸ� ���ڿ��� ��ȯ�Ͽ� �ſ� ǥ��
			yield return new WaitForSeconds(1); // 1�� ���
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
		{   //�̹��� Ȱ��ȭ �Լ�
			image.gameObject.SetActive(true);
			SoundManager.instance.PlaySFX("Button");
		}
	}
}