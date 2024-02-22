using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletManager : MonoBehaviour
{

    public static BulletManager instance;
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
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject specialbulletPrefab;

    public ExplosionManager expManager;

    private List<Bullet> bulletPool = new List<Bullet>();
    private List<Bullet> specialbulletPool = new List<Bullet>();
    
    void Start()
    {
        for(int i=0; i<30; i++)
		{
            GameObject bullet = Instantiate(bulletPrefab,transform);
            bullet.SetActive(false);
            Bullet mybullet = bullet.GetComponent<Bullet>();
            bulletPool.Add(mybullet);

            

            GameObject specialbullet = Instantiate(specialbulletPrefab);
            specialbullet.transform.parent = this.transform;
            specialbullet.SetActive(false);
            Bullet mySpecialbullet = specialbullet.GetComponent<Bullet>();
            specialbulletPool.Add(mySpecialbullet);
        }
    }
    public Bullet GetBullet(Vector3 vec, Quaternion quat)
    {   
        Bullet go = GetAvailabeBullet(bulletPool, bulletPrefab);
        go.transform.SetPositionAndRotation(vec, quat);
        go.gameObject.SetActive(true);
        return go;
    }
    public Bullet GetSpecialBullet(Vector3 vec, Quaternion quat)
	{
        Bullet go = GetAvailabeBullet(specialbulletPool, specialbulletPrefab);
		go.transform.SetPositionAndRotation(vec, quat);
		go.gameObject.SetActive(true);
        return go;
    }
    public Bullet GetAvailabeBullet(List<Bullet> list,GameObject go)
	{
        for(int i=0; i<list.Count; i++)
		{
			if (!list[i].gameObject.activeInHierarchy)
			{
                return list[i];
			}

        }
        // 모든 총알이 활성화되어 있다면 새로운 총알을 생성하여 반환
        Bullet newBullet = Instantiate(go).GetComponent<Bullet>();
        newBullet.gameObject.SetActive(false);
        Bullet mybullet = newBullet.GetComponent<Bullet>();
        list.Add(newBullet);
        return newBullet;
    }
   
}
