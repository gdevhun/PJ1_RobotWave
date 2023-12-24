using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCircle : MonoBehaviour
{
    public GameObject rotatingObjectPrefab; // ȸ���ϴ� ��ü�� ������
    public float rotationSpeed = 5f; // ��ü�� ȸ�� �ӵ�
    public Transform playerTrans;
	private List<GameObject> rotatingFireList = new List<GameObject>(); // ������ ��ü���� ���� ����Ʈ
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
		//�ϳ��� �����ؼ� ����Ʈ�� �ֱ�
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

			// ����Ʈ�� �ִ� ������Ʈ �����ؼ� ����
			GameObject rotatingObject = rotatingFireList[i];
			rotatingObject.transform.localPosition = Vector3.zero;
			rotatingObject.transform.localRotation = Quaternion.identity;
			rotatingObject.GetComponent<Bullet>().additiveDamage = additiveDmg;
			// ��ġ�� ȸ�� ����
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
