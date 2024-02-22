using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FollowText : MonoBehaviour
{
	private RectTransform rect;
	private Transform target;
	private Vector3 offset;
	private void Awake()
	{
		rect = GetComponent<RectTransform>();
		offset = new Vector3(0f, 0.9f, 0f);
	}

	public void SetTarget(Transform trans)
	{
		target = trans;
	}

	void LateUpdate()
	{
		if (target) //target 변수가 Null이 아니면 실행
		{
			rect.position = Camera.main.WorldToScreenPoint(target.position+offset);
		}
	}

	private void OnDisable()
	{
		target = null; //비활성화 시 초기화
	}
}
