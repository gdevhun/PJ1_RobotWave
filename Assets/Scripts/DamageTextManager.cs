using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextManager : MonoBehaviour
{
    [SerializeField]
    private GameObject damageTextPrefab; // 데미지 텍스트 프리팹
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
        // 초기에 텍스트 오브젝트를 풀에 생성
        for (int i = 0; i < 50; i++) // 예시로 50개를 미리 생성
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
            // 풀이 비어 있으면 새로운 텍스트를 생성
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
        yield return new WaitForSeconds(1.0f); // 텍스트를 활성화한 후 1초 후에 비활성화
        damageText.gameObject.SetActive(false);
        damageTextPool.Enqueue(damageText);
    }
}