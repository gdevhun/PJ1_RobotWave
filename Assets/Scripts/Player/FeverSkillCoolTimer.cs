using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FeverSkillCoolTimer : MonoBehaviour
{

    public TextMeshProUGUI coolTimeText;
    public Image coverImage;//
    public bool skillcool;

    public float cooldownTime = 20f;
    public float currentTime = 0f;

    private void Start()
    {
        coverImage.gameObject.SetActive(false);
        coolTimeText.gameObject.SetActive(false);
        skillcool = false;
        currentTime = cooldownTime;
        coolTimeText.text = cooldownTime.ToString("F0"); // �ʱ� �ð� ����
    }

    private void Update()
    {
        if (skillcool)
        {
            UpdateCooldown();
        }
    }

    public void SkillCool()
    {
        skillcool = true;
        coolTimeText.gameObject.SetActive(true);
        coverImage.gameObject.SetActive(true);//
        currentTime = cooldownTime;
    }


    private void UpdateCooldown()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            coverImage.fillAmount = (currentTime / cooldownTime);//
            coolTimeText.text = currentTime.ToString("F0"); // ���� �ð��� �ؽ�Ʈ�� ǥ��
        }
        else
        {
            skillcool = false;
            coolTimeText.gameObject.SetActive(false);
            coverImage.gameObject.SetActive(false);//
        }
    }



}
