using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    //-5.25 5.25 -4 4
    [SerializeField]
    private GameObject player;

    void Start()
    {
        transform.position = player.transform.position;
    }

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

