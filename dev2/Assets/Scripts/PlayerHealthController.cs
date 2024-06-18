using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public float InvincibleTimer=1.0f;
    private float InvincibleCounter;
    public float TimeSlowTimer = 0.5f;
    private float TimeSlowCounter;
    public BoyDamagers boy;
    public CharacterAnimation ch1;
    public CharacterAnimation ch2;
    public CharacterAnimation ch3;
    public CharacterAnimation ch4;
    private void Awake()
    {
        instance = this;
    }

    public float currentHealth;
    private float maxHealth;

    public Slider healthSlider;

    public GameObject deathEffect;//这里将来放死亡特效
    public TMP_Text leftText;
    public TMP_Text rightText;
    void Start()
    {
        boy = null;
        maxHealth = PlayerStatController.instance.health[0].value;
        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        leftText.text = currentHealth.ToString();
        rightText.text ="/  "+ maxHealth.ToString();
    }

    
    void Update()
    {
        if (InvincibleCounter >= 0)
        {
            InvincibleCounter-=Time.deltaTime;
        }
    }

    public void TakeDamage(float damageToTake)
    {
        //如果是无敌状态，不受伤害
        if(TestParameter.instance.isIndestructible == true) { return; }


        if (InvincibleCounter < 0)
        {
            SFXManager.instance.PlaySFXPitched(0);
            currentHealth -= damageToTake;
            leftText.text = currentHealth.ToString();
            InvincibleCounter = InvincibleTimer;
            // 可选：在一定延迟后恢复时间流速
            //Time.timeScale = 0.25f;
            //Invoke("ResetTimeScale", 0.06f); // 0.3秒后恢复
            if (boy != null)
            {
                boy.powerup();
            }
           
        }
        else
        {
            return;
        }
        

        if (currentHealth <= 0)
        {
            //gameObject.SetActive(false);
            
            Die(); 
            LevelManager.instance.EndLevel();

           // Instantiate(deathEffect, transform.position, transform.rotation);

           // SFXManager.instance.PlaySFX(3);
        }

        healthSlider.value = currentHealth; 
    }
    private void ResetTimeScale()
    {
        Time.timeScale = 1.0f;
    }
    public void addHealth(int num)
    {
        currentHealth += num;
        leftText.text = currentHealth.ToString();
        DamageNumberController.instance.SpawnDamage(num,PlayerController.instance.transform.position+new Vector3(0,2,0),true,true);
    }
    public void Die()
    {
        StartCoroutine(PauseGameAfterTwoSeconds());
        ch1.Die();
        ch2.Die();
        ch3.Die();
        ch4.Die();
    }
    IEnumerator PauseGameAfterTwoSeconds()
    {
        // 等待两秒
        yield return new WaitForSeconds(1f);

        // 暂停游戏
        Time.timeScale = 0f;
    }
    public void AddHealth(int num)
    {
        maxHealth += num;
        currentHealth += num;
        leftText.text = currentHealth.ToString();
        rightText.text = "/  " + maxHealth.ToString();
    }
}
