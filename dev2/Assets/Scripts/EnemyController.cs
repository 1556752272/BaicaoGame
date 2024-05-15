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
    private float Hurttimer ;
    private MeshRenderer meshRenderer;
    private Color mineColor;
    void Start()
    {
        movespeed2 = moveSpeed;
        maxhealth = health;
        //target = FindObjectOfType<PlayerController>().transform;
        target = PlayerHealthController.instance.transform;
        theRB = GetComponent<Rigidbody2D>();
        meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        mineColor = meshRenderer.material.color;
    }

    void Update()
    {
        if (Hurttimer >= 0)
        {
            Hurttimer -= Time.deltaTime;
            if (Hurttimer <= 0)
            {
                meshRenderer.material.color = mineColor;
            }
        }
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
    bool isFading = false;
    IEnumerator FadeOut()
    {
        isFading = true;
        Color fadeColor = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, 0); // ͸��ɫ  
        float elapsedTime = 0;

        while (elapsedTime < 1f && isFading)
        {
            // ʹ��Lerp��elapsedTime��fadeDuration֮���ֵ  
            meshRenderer.material.color = Color.Lerp(mineColor, fadeColor, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 1f)
            {
                Die();
                ExperienceLevelController.instance.SpawnExp(transform.position, expToGive);
                if (Random.value <= coinDropRate)//һ����ʵ�����
                {
                    CoinController.instance.DropCoin(transform.position, coinValue);
                }
                Destroy(this.gameObject);
            }
            yield return null; // �ȴ�ֱ����һ֡  
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
        meshRenderer.material.color = Color.gray;

        Hurttimer = 0.3f;
        health -= damageToTake;
        SFXManager.instance.PlaySFXPitched(2);
        if (health <= 0)
        {
            if (isFading == false) StartCoroutine(FadeOut());
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
            if (isFading == false) StartCoroutine(FadeOut());

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
        //DestroyMonster();
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
    public void DestroyMonster()
    {
        // ��ʼ����Ч��  
        StartCoroutine(FadeOutAndDestroy());
    }

    IEnumerator FadeOutAndDestroy()
    {
        Color fadeOutColor = Color.black;//new Color(mineColor.r, mineColor.g, mineColor.b, 0f); // ͸����ɫ  
        float fadeDuration = 1f; // �������ʱ��  

        // ʹ��Color.Lerp������ɫ����  
        for (float t = 0f; t <= 1f; t += Time.deltaTime / fadeDuration)
        {
            Color color = Color.Lerp(mineColor, fadeOutColor, t);
            meshRenderer.material.color = color;

            // �ȴ���һ֡  
            yield return null;
        }

        // ������ɺ����ٹ���  
        Destroy(gameObject);
    }

}  


