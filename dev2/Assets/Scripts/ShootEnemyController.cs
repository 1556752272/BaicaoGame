using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Spine.Unity;
public class ShootEnemyController : Enemy
{
    [HideInInspector] public Rigidbody2D theRB;
    public float moveSpeed;
    private Transform target;

    public float damage;//�����˺�

    public float hitWaitTime = 1f;//��ײ��ɫ�´ε���ȴʱ��
    private float hitCounter;
    


    public float knockBackTime = 0.5f;//���˹���ʱ��
    private float knockBackCounter;

    public int expToGive = 1;//���侭��

    public int coinValue = 1;//����������
    public float coinDropRate = .5f;//�����Ҹ���
    //*********************************************************
    
    public GameObject bulletPrefab; // �ӵ�Prefab
    [HideInInspector] public Transform bulletSpawnPoint; // �ӵ����ɵ�
    
    public float attackRange = 5.0f; // ����Ĺ������
    public float attackCooldown = 1.0f; // ������ȴʱ��

    [HideInInspector]public bool faceRight = false; 
    
    public float shootForce = 5.0f; // �����
    private float attackTimer; // ������ʱ��
    public bool isSlow;
    public float slowNumber;
    private int stoneattacktimer;
    private bool stoned;
    private float stonedTimer;
    // private Rigidbody2D rb; // �����Rigidbody2D���
    private bool isIgnited = false; // �Ƿ񱻵�ȼ  
    private Coroutine igniteCoroutine; // ��ȼЭ��  
    public SkeletonAnimation spineAnimation;
    //public bool isWalking = true;
    void Start()
    {
        maxhealth = health;
        bulletSpawnPoint = this.transform;
        //target = FindObjectOfType<PlayerController>().transform;
        target = PlayerHealthController.instance.transform;
        theRB = GetComponent<Rigidbody2D>();
        attackTimer = -1; // ��ʼ��������ʱ��
    }

    void Update()
    {
        if ((PlayerController.instance.transform.position.x < transform.position.x)&&faceRight)
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
            if (stonedTimer <= 0)
            {
                stoned = false;
                
            }
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
        if (attackTimer >= 0)
        {
            attackTimer -= Time.deltaTime;
        }
            if (PlayerController.instance.gameObject.activeSelf == true)//������һ�������ǹ����ƶ���
            {
                // �������Ƿ��������
                if (Vector2.Distance(transform.position, PlayerController.instance.transform.position) <= attackRange)
                {
                    // ֹͣ�����ƶ�
                    theRB.velocity = Vector2.zero;

                    // ÿ����������ȴʱ�䣬����һ�ι���
                    if (attackTimer <= 0)
                    {
                        AttackPlayer();
                        attackTimer = attackCooldown;
                        spineAnimation.AnimationName = "attack";
                    spineAnimation.timeScale = 0.5f;
                        //isWalking = false; 
                        spineAnimation.loop = false; 
                    }
                    
                }
                else
                {
                    // ��Ҳ�������ڣ�����������ƶ�
                    //Vector2 direction = (PlayerController.instance.transform.position - transform.position).normalized;
                    //theRB.velocity = direction * moveSpeed;
                    if (knockBackCounter > 0)
                    {
                        knockBackCounter -= Time.deltaTime;

                        if (moveSpeed > 0)
                        {
                            moveSpeed = -moveSpeed * 2f;
                        }

                        if (knockBackCounter <= 0)
                        {
                            moveSpeed = Mathf.Abs(moveSpeed * .5f);
                        }
                    }
                    if (stoned)
                    {
                        theRB.velocity = Vector3.zero;
                    }
                    else
                    {
                        theRB.velocity = (target.position - transform.position).normalized * moveSpeed;
                        spineAnimation.AnimationName = "walk";
                        spineAnimation.loop = true;
                        spineAnimation.timeScale = 1f;
                    // if (!isWalking)
                    // {isWalking = true;  }

                }
                    if (isSlow) theRB.velocity *= slowNumber;
                    if (hitCounter >= 0f)
                    {
                        hitCounter -= Time.deltaTime;
                    }
                }
            
        }
}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && hitCounter < 0f)
        {
            knockBackCounter = 0.2f * knockBackTime;//������ϣ������������ɫ�󷴵�һ�����

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
        SFXManager.instance.PlaySFXPitched(2);
        health -= damageToTake;
        if(health <= 0)
        {

            //Destroy(gameObject);   
            Application.Quit();
            ExperienceLevelController.instance.SpawnExp(transform.position,expToGive);

            Destroy(gameObject);

            if(Random.value <= coinDropRate)//һ����ʵ�����
            {
                CoinController.instance.DropCoin(transform.position, coinValue);
            }
            
           // SFXManager.instance.PlaySFXPitched(0);
        }else
        {
          //  SFXManager.instance.PlaySFXPitched(1);
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);
        //Debug.Log("�ҵ�Ҫ��������: " + transform.position);

    }

    public override void TakeDamage(float damageToTake,bool shouldKnockback)//��������Ƿ��л�����Ч
    {
        if (poison) { TakeDamage(damageToTake * 1.3f); } //�����ǹ����϶��ؼӱ��˺�
        else { TakeDamage(damageToTake); }
        if (shouldKnockback) {
            knockBackCounter = knockBackTime;
        }
    }
    public override void TakeDamage(float damageToTake, int fire)
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

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position, true);

    }
    public void AttackPlayer()
    {
        //if (PlayerController.instance.transform.position.x < transform.position.x) { //���������
        //// ʵ�����ӵ�
        //GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position - new Vector3(0.3f, 0, 0), Quaternion.identity);

        //// �����ӵ��ķ���������ָ�����
        //Vector3 direction = PlayerController.instance.transform.position - bulletSpawnPoint.position;
        //direction.Normalize(); // ȷ�����������ĳ���Ϊ1

        //// Ϊ�ӵ�����ٶȺͷ���
        //Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        //bulletRigidbody.velocity = direction * shootForce;
        //}
        //else
        //{
            Vector3 direction = PlayerController.instance.transform.position - bulletSpawnPoint.position;
            direction.Normalize(); // ȷ�����������ĳ���Ϊ1
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//damager.transform.rotation
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position+new Vector3(0.3f,0,0), Quaternion.Euler(new Vector3(0, 0, angle)));


            // Ϊ�ӵ�����ٶȺͷ���
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * shootForce, Mathf.Sin(angle * Mathf.Deg2Rad) * shootForce);
       // }
        //Vector3 direction = PlayerController.instance.transform.position-this.transform.position;
        //direction.Normalize(); // ȷ�����������ĳ���Ϊ1
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//damager.transform.rotation
        //BulletDamager bullet = Instantiate(damager, position, Quaternion.Euler(new Vector3(0, 0, angle)));
        //bullet.gameObject.SetActive(true);
        //Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        //bulletRigidbody.velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * shootForce, Mathf.Sin(angle * Mathf.Deg2Rad) * shootForce);
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
        stoneattacktimer++;
        if (stoneattacktimer == 5)
        {
            stoneattacktimer = 5;
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
            TakeDamage(igniteDamage, 2);
        }
    }

    // ���������߼�  
    private void Die()
    {
        if (isIgnited)
        {
            isIgnited = false;
            StopCoroutine(igniteCoroutine);

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
