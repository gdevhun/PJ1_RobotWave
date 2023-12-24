using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCircle : MonoBehaviour
{
    public GameObject rotatingObjectPrefab; // 회전하는 물체의 프리팹
    public float rotationSpeed = 5f; // 물체의 회전 속도
    public Transform playerTrans;
	private List<GameObject> rotatingFireList = new List<GameObject>(); // 생성된 물체들을 담을 리스트
	public float additiveDmg = 0;

	void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
		AddFireNum();
	}

	public void AddFireDmg(float additivedmg)
	{
		this.additiveDmg += additivedmg;
		BroadcastMessage("FireDamage",this.additiveDmg);
	}

	public void AddFireNum()
	{
		//하나를 생성해서 리스트에 넣기
		GameObject rotatingObject = Instantiate(rotatingObjectPrefab, Vector3.zero, Quaternion.identity);
		rotatingObject.transform.SetParent(transform);
		rotatingObject.GetComponent<Bullet>().additiveDamage = additiveDmg;
		rotatingFireList.Add(rotatingObject);

		SpawnRotatingObjects();
	}

	
    void SpawnRotatingObjects()
    {
		for (int i = 0; i < rotatingFireList.Count; i++)
		{

			// 리스트에 있는 오브젝트 접근해서 수정
			GameObject rotatingObject = rotatingFireList[i];
			rotatingObject.transform.localPosition = Vector3.zero;
			rotatingObject.transform.localRotation = Quaternion.identity;
			rotatingObject.GetComponent<Bullet>().additiveDamage = additiveDmg;
			// 위치와 회전 설정
			float angle = i * 360f / rotatingFireList.Count;
			rotatingObject.transform.localPosition = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) *
				2f, Mathf.Sin(angle * Mathf.Deg2Rad) * 2f, 0f);
			rotatingObject.transform.Rotate(Vector3.forward, angle);
		}
    }
	private void Update()
	{
        transform.position = playerTrans.position;
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

    }
}
