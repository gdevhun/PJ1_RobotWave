using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{   //enemy�� player��ġ�� �˰� ���󰡾���.
    private Transform targetTransform;
    private HpBar hpBar;
    [SerializeField]
    private float maxHP;
    private float hp;

    public int score;
    public float attackDamage;
    public float collisionDamage;

    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Color hitColor;
    private Color originalColor;

    [SerializeField]
    private float movespeed = 0.75f;

    private Animator animator;

    [SerializeField]
    private float attackDistanceX;
    [SerializeField]
    private float attackDistanceY;

    void Start()
    {
        //tag�̸����� ��ġ���� player��������
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        hpBar = GetComponentInChildren<HpBar>();
        animator = GetComponent<Animator>();
        hp = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

    }

    void Update()
    {

		if (GameManager.instance.isGameOver)
		{
            animator.SetBool("isAttack", false);
            return;

		}
		if (Mathf.Abs(targetTransform.position.x-transform.position.x)<attackDistanceX&&
            Mathf.Abs(targetTransform.position.y-transform.position.y)<attackDistanceY)
		{
            animator.SetBool("isAttack", true);
		}
		else
		{
            animator.SetBool("isAttack", false);
            Vector3 moveTo = (targetTransform.position - transform.position).normalized;
            transform.position += moveTo * movespeed * Time.deltaTime;

            Vector3 currScale = transform.localScale;
            Vector3 currHpBarScale = hpBar.transform.localScale;
            if (moveTo.x > 0)
            {
                transform.localScale = new Vector3
                    (-Mathf.Abs(currScale.x), currScale.y, currScale.z);
                hpBar.transform.localScale = new Vector3(-Mathf.Abs(currHpBarScale.x),
                    currHpBarScale.y, currHpBarScale.z);
            }
            else
            {
                transform.localScale = new Vector3
                    (Mathf.Abs(currScale.x), currScale.y, currScale.z);
                hpBar.transform.localScale = new Vector3(Mathf.Abs(currHpBarScale.x),
                    currHpBarScale.y, currHpBarScale.z);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
	{
        //�����浹�ߴµ� tag�� ���� �Ѿ��̶��
        if (other.gameObject.tag == "Bullet")
        {
            
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            hp -= bullet.Damage();
            // �ؽ�Ʈ ���� �� Ȱ��ȭ
            Debug.Log(bullet.Damage());

           
            DamageTextManager.instance.ShowDamageText(transform, bullet.Damage());
            bullet.ActiveExplosion();

			if (hp <= 0) //�Ѿ� �浹�� ü���� 0���Ϸ� ��������
			{
                ScoreUI.instance.GetScore(this.score);
                bool isGameOver = false;
				if (gameObject.tag == "Boss")
				{
                    isGameOver = true;
                    SoundManager.instance.PlaySFX("BossDead");
                }
                SoundManager.instance.PlaySFX("EnemyDead");
                EnemyDeadManager.instance.GetEnemyDeadEffect(this.transform.position);
                hp = maxHP;
                gameObject.SetActive(false);
				if (isGameOver)
				{
                    GameManager.instance.SetGameOver(true);
				}
			}
			else //�Ѿ� �浹 �� ü���� �������� ��
            {   
                spriteRenderer.color = hitColor;
                Invoke("ResetColor", 0.1f);
            }
            hpBar.SetHP(hp, maxHP);
        }
	}
    private void ResetColor()
	{
        spriteRenderer.color = originalColor;
	}
    public void SetGameOver()
	{
        if (gameObject.tag == "Enemy" || gameObject.tag=="Boss")
        {
            animator.SetTrigger("isGameOver");
        }
	}
    public void DestroySelf()
	{
        EnemyDeadManager.instance.GetEnemyDeadEffect(this.transform.position);
        gameObject.SetActive(false);
    }
 
}
