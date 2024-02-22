using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextManager : MonoBehaviour
{
    [SerializeField]
    private GameObject damageTextPrefab; // ������ �ؽ�Ʈ ������
    private Queue<TextMeshProUGUI> damageTextPool = new Queue<TextMeshProUGUI>();

    public static DamageTextManager instance;
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
    private void Start()
    {
        // �ʱ⿡ �ؽ�Ʈ ������Ʈ�� Ǯ�� ����
        for (int i = 0; i < 50; i++) // ���÷� 50���� �̸� ����
        {
            TextMeshProUGUI damageText = Instantiate(damageTextPrefab, this.transform).GetComponent<TextMeshProUGUI>();
            damageText.rectTransform.position = Vector3.zero;
            damageText.gameObject.SetActive(false);
            damageTextPool.Enqueue(damageText);
        }
    }


	public void ShowDamageText(Transform trans, float damage)
    {
        TextMeshProUGUI damageText;
        if (damageTextPool.Count > 0)
        {
            damageText = damageTextPool.Dequeue();
        }
        else
        {
            // Ǯ�� ��� ������ ���ο� �ؽ�Ʈ�� ����
            damageText = Instantiate(damageTextPrefab, this.transform).GetComponent<TextMeshProUGUI>();
        }

        int roundedDamage = Mathf.RoundToInt(damage);
        damageText.text = "-" + roundedDamage.ToString();
        damageText.gameObject.SetActive(true);
        damageText.GetComponent<FollowText>().SetTarget(trans);
        StartCoroutine(DisableDamageText(damageText));
    }

    private IEnumerator DisableDamageText(TextMeshProUGUI damageText)
    {
        yield return new WaitForSeconds(1.0f); // �ؽ�Ʈ�� Ȱ��ȭ�� �� 1�� �Ŀ� ��Ȱ��ȭ
        damageText.gameObject.SetActive(false);
        damageTextPool.Enqueue(damageText);
    }
}