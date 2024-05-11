using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public float currentHealth, maxHealth;
  

    public Slider healthSlider;

    public GameObject deathEffect;//这里将来放死亡特效

    void Start()
    {
        boy = null;
        maxHealth = PlayerStatController.instance.health[0].value;
        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
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
        if (InvincibleCounter < 0)
        {
            SFXManager.instance.PlaySFXPitched(0);
            currentHealth -= damageToTake;
            InvincibleCounter = InvincibleTimer;
            Time.timeScale = 0.3f;
            
            if (boy != null)
            {
                boy.powerup();
            }
            // 可选：在一定延迟后恢复时间流速
            Invoke("ResetTimeScale", 0.3f); // 0.3秒后恢复
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
}
