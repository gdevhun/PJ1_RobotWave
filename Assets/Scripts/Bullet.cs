using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum Type
	{
        Melee, Range
	}
    [SerializeField]
    private Type type;

    

    [SerializeField]
    private float moveSpeed = 5f;

    WaitForSeconds wait;
    public float damage;
    public float additiveDamage;
    public float totalDmg;
    
    //에네미 데미지 전달, 아이템획득 모두 이함수를 통해처리함.
    public float Damage(){

        totalDmg= damage + additiveDamage;
        if (GameManager.instance.isFeverMode == true)
		{
            totalDmg *= 2;
        }
        return totalDmg;

    }
	public float FireDamage(float addtiveDamage)
	{
        this.additiveDamage = addtiveDamage;
		totalDmg = damage + additiveDamage;

		return totalDmg;
	}


	// Start is called before the first frame update
	void Start()
    {
        if(type == Type.Range)
            StartCoroutine(DisableRoutine());
    }
	private void Awake()
	{
        wait = new WaitForSeconds(3f);
    }

	IEnumerator DisableRoutine()
	{
        yield return wait;
        gameObject.SetActive(false);
	}
    
    void Update()
    {
        if (type == Type.Range)
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
    public void ActiveExplosion()
    {
		if (type == Type.Range)
		{
            SoundEnemyCollide();
            ExplosionManager.instance.GetAvailableExgo(transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
		else
		{
            SoundManager.instance.PlaySFX("FiredEnemy", 1f);
            ExplosionManager.instance.GetAvailableExgo2(transform.position, Quaternion.identity);
        }
    }
    private void SoundEnemyCollide()
    {   //1부터 3까지의 임의의 정수 생성
        int RandomSoundIndex = Random.Range(1, 4);
        // 생성된 정수 값에 따라 다른 사운드 재생
        switch (RandomSoundIndex)
        {
            case 1:
                SoundManager.instance.PlaySFX("BulletCollide1", 0.3f);
                break;
            case 2:
                SoundManager.instance.PlaySFX("BulletCollide2", 0.3f);
                break;
            case 3:
                SoundManager.instance.PlaySFX("BulletCollide3", 0.3f);
                break;
            default:
                break;
        }
    }
}
