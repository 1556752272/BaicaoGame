using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : MonoBehaviour
{
    public float healAmount = 5.0f; // 每次回复的血量
    private float healInterval = 2f; // 回复血量的时间间隔

    private bool isPlayerInside; // 标记玩家是否在碰撞器内
    private float healTimer; // 用于控制回血时间的计时器
    public float healTimelong = 10.0f;
    void OnEnable()
    {
        // 在脚本激活时开始计时
        healTimer = healInterval;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        // 检测到玩家进入碰撞器
        if (other.gameObject.tag=="Player")
        {
            isPlayerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 检测到玩家离开碰撞器
        if (other.gameObject.tag == "Player")
        {
            isPlayerInside = false;
        }
    }

    void Update()
    {
        // 减少计时器
        healTimer -= Time.deltaTime;
        healTimelong -= Time.deltaTime;
        if (healTimelong <= 0)
        {
            Destroy(gameObject);
        }
        // 检查玩家是否在碰撞器内且计时器已经结束
        if (isPlayerInside && healTimer <= 0)
        {
            // 调用方法回复玩家血量
            HealPlayer();

            // 重置计时器
            healTimer = healInterval;
        }
    }

    void HealPlayer()
    {
        // 假设有一个方法可以获取玩家的引用并回复血量
        // PlayerHealth playerHealth = ...;
        // playerHealth.Heal(healAmount);
        Debug.Log("Player healed for " + healAmount);
        PlayerHealthController.instance.addHealth((int)healAmount);
    }
}
