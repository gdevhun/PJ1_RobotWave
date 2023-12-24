using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    //-5.25 5.25 -4 4
    [SerializeField]
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		if (player != null)
		{
            float posX = player.transform.position.x;
            float posY = player.transform.position.y;
            transform.position = new Vector3(posX, posY, -10f);
        }
    }
}

/*
Clamp는 주어진 값이 특정 범위 안에 있도록 제한하는 함수입니다.
만약 값이 범위를 벗어난 경우, 최솟값 또는 최댓값으로 
고정됩니다. 여기서 value는 제한할 값이며, min과 max는 
허용 범위의 최솟값과 최댓값입니다.
예를 들어, 값이 0에서 10 사이로 제한되도록 하는 경우:
float clampedValue = Mathf.Clamp(value, 0f, 10f);
*/
