using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
    public void NotifyBossText() //�����G �غ��� �˸��ؽ�Ʈ (notifyText ��ũ��Ʈ����)
	{
        NotifyBossPanel.SetActive(true);
        NotifyBossPanel.GetComponentInChildren<NotifyText>().DisplayBossMessage();
        UnActiveMiniBossPanel().Forget();
    }
    public void NotifyMiniBossText()
    {
        NotifyMiniBossPanel.SetActive(true);
		NotifyMiniBossPanel.GetComponentInChildren<NotifyText>().DisplayMiniBossMessage();
        UnActiveBossPanel().Forget();
    }

	private async UniTaskVoid UnActiveBossPanel()
	{   
        //�޼��� ��� �� ��Ȱ��ȭ�� ���� �Լ�
        await UniTask.Delay(TimeSpan.FromSeconds(6f));
        NotifyBossPanel.SetActive(false);
	}
    private async UniTaskVoid UnActiveMiniBossPanel()
    {   
        //�޼��� ��� �� ��Ȱ��ȭ�� ���� �Լ�
        await UniTask.Delay(TimeSpan.FromSeconds(5f));
        NotifyMiniBossPanel.SetActive(false);
    } 
    


    public void ActiveItemScene1()  //ItemPurplePanel ��Ƽ��
    {
        ItemPurplePanel.gameObject.SetActive(true);
        PauseGame();

    }
    public void ActiveItemScene2()  //ItemRedPanel ��Ƽ��
    {
        ItemRedPanel.gameObject.SetActive(true);
        PauseGame();
    }
    public void ActiveItemScene3()  //ItemDiaPanel ��Ƽ��
    {
        ItemDiaPanel.gameObject.SetActive(true);
        PauseGame();
    }


    // Ư�� �Լ��� ȣ���Ͽ� ���� ȭ�� �ߴ� �� �����
    public void PauseAndResumeGame()
    {
        // ������ �ߴ��ϰ� ���ϴ� ó���� ����
        PauseGame();
    }
    public void PauseGame()
    {
        // ������ �ߴ��ϴ� �ڵ� �ۼ�
        Time.timeScale = 0f; // ���� �ð��� ����
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
        Time.timeScale = 1f; // ���� �ð��� ������� ����
        SoundManager.instance.PlaySFX("GunGrab", 1f);
    }
}
