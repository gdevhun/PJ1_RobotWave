using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class NotifyText : MonoBehaviour
{
    public TextMeshProUGUI notifyText;
    public float textDelay=0.6f;
    public string notifybossMsg;
    public string notifyminibossMsg;


    IEnumerator DisplayText(string message)
    {
        notifyText.text = ""; //출력전에 비워놓기
        for (int i = 0; i < message.Length; i++)
        {
            notifyText.text += message[i];
            yield return new WaitForSeconds(textDelay);
        } //textDelay=0.5f
    }

    public void DisplayBossMessage()
	{
        SoundManager.instance.StopBGM();
        SoundManager.instance.PlayBGM("Stage_MiniBoss");
        StartCoroutine(DisplayText(notifybossMsg));
	}

    public void DisplayMiniBossMessage()
    {
        SoundManager.instance.StopBGM();
        SoundManager.instance.PlayBGM("Stage_Boss");
        StartCoroutine(DisplayText(notifyminibossMsg));
    }
}
