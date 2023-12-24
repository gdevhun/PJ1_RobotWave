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
        // 마우스 커서의 스크린 상의 위치를 게임 월드 상의 좌표로 변환
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Z축 값을 0으로 설정하여 오브젝트가 카메라 앞에 나타나도록 함
        mousePos.z = 0;

        // 오브젝트의 위치를 마우스 커서 위치로 설정
        transform.position = mousePos;
    }
}
