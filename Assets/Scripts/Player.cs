using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject Bullet;

    [SerializeField]
    private GameObject specialBullet;

    [SerializeField]
    private GameObject FeverEffect;

    private Transform gunTransform;
    [SerializeField]
    private float shootInterval = 0.3f;
    public float feverShootInterval = 0.12f;
	private float lastShotTime;

    private Animator animator;
    private HpBar hpBar;

    [SerializeField]
    private float maxHP;
    private float hp;

    private float collisionDamageTime = 0.1f;
    private float lastDamagedTime;

    private SpriteRenderer spriteRenderer;
    private Color originalcolor;
    [SerializeField]
    private Color hitColor;

    [SerializeField]
    private float moveSpeed = 3f;

    [SerializeField]
    private TextMeshProUGUI damageText;
    [SerializeField]
    private TextMeshProUGUI itemGetText;


    private float dashTime;
    private Vector3 dashVec;

    bool isFlipped = false;
    public float additiveDmg;

    bool isRolling = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        hpBar=GetComponentInChildren<HpBar>();
        gunTransform = transform.Find("Gun");
        hp = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalcolor = spriteRenderer.color;
    }
     
    // Update is called once per frame
    void Update()
    {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = mousePos - transform.position;
        direction = direction.normalized;
		Vector3 currScale = transform.localScale;
		Vector3 currHpBarScale = hpBar.transform.localScale;



		if (GameManager.instance.isGameOver)
		{
            return;
		}
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 moveTo = new Vector3(horizontalInput, verticalInput, 0f).normalized;

		
		if (dashTime <= 0)
		{
            transform.position += moveTo * moveSpeed * Time.deltaTime;
        }
        else
        {   //대쉬활성화  
			transform.position += dashVec * moveSpeed * 1.6f * Time.deltaTime;
            dashTime -= Time.deltaTime;
		}

		// 스페이스를 누르면 isRolling을 true로 설정
		if (Input.GetKeyDown(KeyCode.Space))
        {
            if (TryGetComponent(out SkillCoolTimer skillCoolTimer))
            {
                if (!skillCoolTimer.skillcool)
                {

					ShowItemMessage("대쉬!");
					animator.SetBool("isRolling", true);
                    isRolling = true;
                    dashVec = moveTo;
                    dashTime = 0.4f;
					//StartCoroutine(DashSkill(moveTo));
					skillCoolTimer.SkillCool();
                    Invoke("DisableRollingAnimation", 0.3f);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (TryGetComponent(out FeverSkillCoolTimer fskillCoolTimer))
            {
                if (!fskillCoolTimer.skillcool)
                {
                    fskillCoolTimer.SkillCool();
                    GameManager.instance.isFeverMode = true;
                    SetFevermode();
                }
            }
        }

        if (moveTo!=Vector3.zero){
            animator.SetBool("isWalking", true);
		}
		else
		{
            animator.SetBool("isWalking", false);
		}
        
  

        // Flip을 체크
        if (direction.x > 0)
        {
            isFlipped = false;
        }
        else
        {
            isFlipped = true;
        }
        
        // Flip 여부에 따라 스케일 조절
        float scaleX = isFlipped ? -Mathf.Abs(currScale.x) : Mathf.Abs(currScale.x);
        float hpBarScaleX = isFlipped ? -Mathf.Abs(currHpBarScale.x) : Mathf.Abs(currHpBarScale.x);

        transform.localScale = new Vector3(scaleX, currScale.y, currScale.z);
        hpBar.transform.localScale = new Vector3(hpBarScaleX, currHpBarScale.y, currHpBarScale.z);
        //마우스클릭시 총알 구현
        if (Input.GetMouseButton(0)) //0 왼쪽마우스클릭
		{
            if (Time.time - lastShotTime >= (GameManager.instance.isFeverMode? feverShootInterval : shootInterval))
            {   //총알생성시 방향 구현
                Vector2 bulletDirection = mousePos - transform.position;
                float angle = Mathf.Atan2(bulletDirection.y, bulletDirection.x)
                    * Mathf.Rad2Deg;
                Quaternion bulletRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                if (GameManager.instance.isFeverMode == true)
                {   //피버모드진입시 스페셜불렛생성
                    Bullet bullet = BulletManager.instance.GetSpecialBullet(gunTransform.position, bulletRotation);
                    bullet.additiveDamage = additiveDmg;
                    SoundManager.instance.PlaySFX("SpecialBullet");
                }
                else
                {   //일반모드 불렛 생성

                    Bullet bullet = BulletManager.instance.GetBullet(gunTransform.position, bulletRotation);
                    bullet.additiveDamage = additiveDmg;
                    SoundManager.instance.PlaySFX("Bullet");
                }
                lastShotTime = Time.time;
            }
        }

        Vector3 offset1 = new Vector3(0f, 0.8f, 0f); //damageText
        damageText.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset1);
        Vector3 offset2 = new Vector3(0f, 1.2f, 0f);  //itemGetText
        itemGetText.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset2);
    }

    void DisableRollingAnimation() //roll 예외처리함수
    {
        animator.SetBool("isRolling", false);
        isRolling = false;
    }

    

    private void OnTriggerEnter2D(Collider2D other)
	{   //공격받을때 호출되는 trigger함수
        if (other.gameObject.tag.Equals("Enemy") ||
            other.gameObject.tag == "Boss")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            TakeDamage(enemy.attackDamage);
        }
        else if (other.gameObject.tag == "ItemRed") //아이템충돌
        {
            SoundManager.instance.PlaySFX("ItemGet1");
            UIManager.instance.ActiveItemScene2();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "ItemPurple") //아이템충돌
        {   //공격속도증가아이템인 ItemAttackspdup 0.075
            SoundManager.instance.PlaySFX("ItemGet1");
            UIManager.instance.ActiveItemScene1();
            Destroy(other.gameObject);
        }   
        else if (other.gameObject.tag == "ItemDia") //아이템충돌
        {   //공격데미지증가아이템인 ItemAttackdmgup
            SoundManager.instance.PlaySFX("ItemGet2");
            UIManager.instance.ActiveItemScene3();
            Destroy(other.gameObject);
        }
    }


    public void SetFevermode()
    {
        FeverEffect.SetActive(true);
        Invoke("PlayGunGrabSFX", 0.5f);
        ShowItemMessage("피버모드 활성화!");
        GameManager.instance.SetFeverMode(true);
        CancelInvoke("ResetFeverMode");
        Invoke("ResetFeverMode", 4.5f);
    }

    private void ResetFeverMode()
	{
        FeverEffect.SetActive(false);
        GameManager.instance.SetFeverMode(false);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{   //한번 충돌됬을 때 호출되는 collision함수
		if (other.gameObject.tag == "Enemy" ||
            other.gameObject.tag=="Boss") //적과충돌
		{
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            TakeCollisionDamage(enemy.collisionDamage);
		}
        
    }

	private void OnCollisionStay2D(Collision2D other)
	{   //stay->계속 충돌하고있어도 호출되는 함수

        if (other.gameObject.tag == "Enemy" ||
            other.gameObject.tag == "Boss")
		{
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            TakeCollisionDamage(enemy.collisionDamage);
        }
	}
    private void TakeCollisionDamage(float damage)
	{
		if (Time.time - lastDamagedTime >= collisionDamageTime)
		{
            TakeDamage(damage);
            lastDamagedTime = Time.time;
        }
	}
    private void TakeDamage(float damage)
	{
        // 텍스트 설정 및 활성화
        damageText.text = "-" + damage.ToString();
        damageText.enabled = true;
        Invoke("DisableDamageText", 1f); // 1초 후 비활성화

        if (GameManager.instance.isGameOver)
		{
            return;
		}
        hp -= damage;

		if (hp <= 0)
		{
            animator.SetTrigger("isDead");
            GameManager.instance.SetGameOver(false);
		}
		else
		{
            spriteRenderer.color = hitColor;
            Invoke("ResetColor", 0.1f);
        }
        hpBar.SetHP(hp, maxHP);
	}
    private void ResetColor()
	{
        spriteRenderer.color = originalcolor;
	}
    private void ShowItemMessage(string message)
    {
        CancelInvoke("DisableItemGetText");
        itemGetText.text = message;
        itemGetText.enabled = true;
        Invoke("DisableItemGetText", 2f);
    }
    private void DisableDamageText()
    {   //데미지출력 비활성화 함수
        damageText.enabled = false;
    }
    private void DisableItemGetText()
	{   //아이템획득 메시지 출력 비활성화 함수
        itemGetText.enabled = false;
	}
    void PlayGunGrabSFX() //fever아이템획득시 
    {   //부를 인보크함수.
        SoundManager.instance.PlaySFX("GunGrab");
    }
    
    public void ButtonEvent_Attackup()
	{
        ShowItemMessage("공격력 증가!");
        additiveDmg += 3;
    }
    public void ButtonEvent_AttackSpdUp()
	{
        SoundManager.instance.PlaySFX("ItemGet1");
        shootInterval -= 0.06f;
        ShowItemMessage("공격속도 증가!");

        if (shootInterval < feverShootInterval)
        {
            shootInterval = feverShootInterval;
            itemGetText.text = "최대공격속도!";
            itemGetText.enabled = true;
            Invoke("DisableItemGetText", 2f); // 2초 후 비활성화
        }      
    }
    public void ButtonEvent_Hpup()
	{
        hp += 35;
        // 텍스트 설정 및 활성화
        ShowItemMessage("체력 회복!");
        if (hp > maxHP)
        {
            ShowItemMessage("최대 체력!");
            hp = maxHP;
        }
        hpBar.SetHP(hp, maxHP);
        SoundManager.instance.PlaySFX("ItemHeal");
    }
    public void ButtonEvent_PlayerSpdUp()
	{
        //3.5 +0.2 +0.2 +0.2
		if (moveSpeed >= 4.4)
        {
			ShowItemMessage("최대 이동속도!");
			return;
        }
        else
        {
			moveSpeed += 0.25f;
			ShowItemMessage("이동속도 증가!");
		}
    }
    
}
