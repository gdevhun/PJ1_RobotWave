using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    public GameObject ItemPurplePanel;
    public GameObject ItemRedPanel;
    public GameObject ItemDiaPanel;
    public GameObject NotifyBossPanel;
    public GameObject NotifyMiniBossPanel;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /*#################################################################*/
    public void NotifyBossText() //보스밎 준보스 알림텍스트 (notifyText 스크립트연결)
	{
        NotifyBossPanel.SetActive(true);
        NotifyBossPanel.GetComponentInChildren<NotifyText>().DisplayBossMessage();
        Invoke("UnactiveBossPanel", 5f);
        //NotifyBossPanel.SetActive(false); 9
    }
    public void NotifyMiniBossText()
    {
        NotifyMiniBossPanel.SetActive(true);
		NotifyMiniBossPanel.GetComponentInChildren<NotifyText>().DisplayMiniBossMessage();
        Invoke("UnactiveMiniBossPanel", 6f);
        //NotifyMiniBossPanel.SetActive(false);
    }

	public void UnactiveBossPanel()
	{   //메세지 출력 후 비활성화를 위한 함수
        NotifyBossPanel.SetActive(false);
	}
    public void UnactiveMiniBossPanel()
    {   //메세지 출력 후 비활성화를 위한 함수
        NotifyMiniBossPanel.SetActive(false);
    }
    /*#################################################################*/


    public void ActiveItemScene1()  //ItemPurplePanel 액티브
    {
        ItemPurplePanel.gameObject.SetActive(true);
        PauseGame();

    }
    public void ActiveItemScene2()  //ItemRedPanel 액티브
    {
        ItemRedPanel.gameObject.SetActive(true);
        PauseGame();
    }
    public void ActiveItemScene3()  //ItemDiaPanel 액티브
    {
        ItemDiaPanel.gameObject.SetActive(true);
        PauseGame();
    }

    /*#################################################################*/
    // 특정 함수를 호출하여 게임 화면 중단 및 재시작
    public void PauseAndResumeGame()
    {
        // 게임을 중단하고 원하는 처리를 수행
        PauseGame();

    }
    public void PauseGame()
    {
        // 게임을 중단하는 코드 작성
        Time.timeScale = 0f; // 게임 시간을 정지
    }

    public void ResumeGame()
    {
        if (ItemPurplePanel.activeSelf)
        {
            ItemPurplePanel.SetActive(false);
        }

        if (ItemRedPanel.activeSelf)
        {
            ItemRedPanel.SetActive(false);
        }

        if (ItemDiaPanel.activeSelf)
        {
            ItemDiaPanel.SetActive(false);
        }
        Time.timeScale = 1f; // 게임 시간을 원래대로 복원
        SoundManager.instance.PlaySFX("GunGrab", 1f);
    }
}
