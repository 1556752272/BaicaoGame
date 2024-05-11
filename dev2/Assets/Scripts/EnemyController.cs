using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyController : Enemy
{
    public Rigidbody2D theRB;
    public float moveSpeed;
    private Transform target;

    public float damage;

    public float hitWaitTime = 1f;//��ײ��ɫ�´ε���ȴʱ��
    private float hitCounter;

    

    public float knockBackTime = 0.5f;
    private float knockBackCounter;

    public int expToGive = 1;

    public int coinValue = 1;//����������
    public float coinDropRate = .5f;//�����Ҹ���
    [HideInInspector] public bool faceRight;
    public bool isSlow;
    public float slowNumber;
    private int stoneattacktimer;
    private bool stoned;
    private float stonedTimer;
    [HideInInspector] private float movespeed2;


    private bool isIgnited = false; // �Ƿ񱻵�ȼ  
    private Coroutine igniteCoroutine; // ��ȼЭ��  
    void Start()
    {
        movespeed2 = moveSpeed;
        maxhealth = health;
        //target = FindObjectOfType<PlayerController>().transform;
        target = PlayerHealthController.instance.transform;
        theRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if ((PlayerController.instance.transform.position.x < transform.position.x) && faceRight)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = -newScale.x;
            transform.localScale = newScale;

            faceRight = false;
        }
        if ((PlayerController.instance.transform.position.x > transform.position.x) && !faceRight)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = -newScale.x;
            transform.localScale = newScale;

            faceRight = true;
        }
        if (stonedTimer > 0)
        {
            stonedTimer -= Time.deltaTime;
            this.theRB.velocity = Vector3.zero;
            if (stonedTimer <= 0)
            {
                stoned = false;
            }
        }
        
        
        if (PlayerController.instance.gameObject.activeSelf == true)
        {
            if(knockBackCounter > 0)
            {
                knockBackCounter -= Time.deltaTime;

                if(moveSpeed > 0)
                {
                    moveSpeed = -moveSpeed * 4f;
                }

                if(knockBackCounter <= 0)
                {
                    moveSpeed = Mathf.Abs(moveSpeed * .25f);
                }
            }
                if (stoned)
                {
                    theRB.velocity = Vector3.zero;
                }
                else
                {

                    theRB.velocity = (target.position - transform.position).normalized * moveSpeed;
                
            }
            if (isSlow) {theRB.velocity *= slowNumber;
                
            }
            
            if (hitCounter >= 0f)
            {
                hitCounter -= Time.deltaTime;
            }
        }else
        {
            theRB.velocity = Vector2.zero;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && hitCounter < 0f)
        {
            knockBackCounter = 0.2f*knockBackTime;//������ϣ������������ɫ�󷴵�һ�����
            
            PlayerHealthController.instance.TakeDamage(damage);
            hitCounter = hitWaitTime;
        }
        if (collision.gameObject.tag == "Player" && hitCounter >= 0f)
        {
            knockBackCounter = 0.1f * knockBackTime;//������ϣ������������ɫ�󷴵�һ�����

        }

    }

    public override void TakeDamage(float damageToTake)
    {
        health -= damageToTake;
        SFXManager.instance.PlaySFXPitched(2);
        if (health <= 0)
        {
            //Destroy(gameObject);   

            ExperienceLevelController.instance.SpawnExp(transform.position,expToGive);
            Die();
            Destroy(gameObject);

            if(Random.value <= coinDropRate)//һ����ʵ�����
            {
                CoinController.instance.DropCoin(transform.position, coinValue);
            }
            
        }else
        {
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);

    }
    public override void TakeDamage(float damageToTake,int fire)
    {
        health -= damageToTake;
        if (health <= 0)
        {
            //Destroy(gameObject);   

            ExperienceLevelController.instance.SpawnExp(transform.position, expToGive);
            Die();
            Destroy(gameObject);

            if (Random.value <= coinDropRate)//һ����ʵ�����
            {
                CoinController.instance.DropCoin(transform.position, coinValue);
            }

        }
        else
        {
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position,true);

    }
    public override void TakeDamage(float damageToTake,bool shouldKnockback)//��������Ƿ��л�����Ч
    {

        if (poison) {TakeDamage(damageToTake * 1.3f); }
        else{ TakeDamage(damageToTake);}
        if (shouldKnockback) {
            knockBackCounter = knockBackTime;
        }
    }

    public override void makeStoned()
    {
        stoneattacktimer++;
        if (stoneattacktimer == 5)
        {
            stoneattacktimer = 0;
            stoned = true;
            stonedTimer = 2f;
        }
    }
    public override void makemaxStoned()
    {
        stoneattacktimer=5;
        if (stoneattacktimer == 5)
        {
            stoneattacktimer = 0;
            stoned = true;
            stonedTimer = 2f;
        }
    }

    public override void makeFired(int num)
    {
        igniteDamage = num;
        if (!isIgnited)
        {
            isIgnited = true;
            igniteCoroutine = StartCoroutine(TakeIgniteDamage());
        }
    }

    // ÿ�����Ѫ��Э��  
    private IEnumerator TakeIgniteDamage()
    {
        while (isIgnited && health > 0)
        {
            yield return new WaitForSeconds(igniteInterval);
            TakeDamage(igniteDamage,2);
        }
    }

    // ���������߼�  
    private void Die()
    {
        if (isIgnited) { 
        isIgnited = false;
            if (igniteCoroutine != null)
            {StopCoroutine(igniteCoroutine);

            }
        

        // ��Ⲣ�˺���Χ�ĵ���  
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider col in colliders)
        {
            Enemy enemy = col.GetComponent<Enemy>(); // ��������һ��Enemy�ű��ڵ�������  
            if (enemy != null)
            {
                enemy.TakeDamage(deathDamage,2);
            }
        }
        }

    }

}  


