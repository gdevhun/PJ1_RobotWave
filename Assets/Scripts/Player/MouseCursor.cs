using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
	void Start()
	{
        Cursor.visible = false;
        SoundManager.instance.PlaySFX("GunGrab");
    }
	// Update is called once per frame
	void Update()
    {
        // ���콺 Ŀ���� ��ũ�� ���� ��ġ�� ���� ���� ���� ��ǥ�� ��ȯ
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Z�� ���� 0���� �����Ͽ� ������Ʈ�� ī�޶� �տ� ��Ÿ������ ��
        mousePos.z = 0;

        // ������Ʈ�� ��ġ�� ���콺 Ŀ�� ��ġ�� ����
        transform.position = mousePos;
    }
}
