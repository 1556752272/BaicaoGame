using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FoodPickup : MonoBehaviour
{
    public GameObject childCanvas;
    public TMP_Text foodtext;
    public float interactionRange = 10.0f; // 玩家与物品的互动距离
    private PlayerController player; // 玩家对象的引用
    public bool power, speed, magnet;
    /*这里可以隐藏*/
    //public KeyCode interactKey = KeyCode.E; // 互动按键
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(PlayerController.instance.transform.position, transform.position) <= interactionRange)
        {
            //Debug.Log("当前距离为" + Vector3.Distance(PlayerController.instance.transform.position, transform.position));
            if (power && FoodController.instance.firstpower)
            {
                foodtext.text = "攻击力提升";
            }
            if (speed && FoodController.instance.firstspeed)
            {
                foodtext.text = "增强速度";
            }
            if (magnet&&FoodController.instance.firstmagnet)
            {
                foodtext.text = "提升拾取范围";
            }

            childCanvas.gameObject.SetActive(true);

            
        }
        else
        {
            // 玩家不在互动范围内，隐藏提示文字
            childCanvas.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //如果按下则增加玩家一段时间属性
            if (power)
            {
                FoodController.instance.powertimeup();
                FoodController.instance.firstpower = true;
                //Debug.Log("食用1");
            }
            if (speed)
            {
                PlayerController.instance.speedattributeTimer = PlayerController.instance.buffDuration;
                FoodController.instance.firstspeed = true;
                //Debug.Log("食用2");
            }
            if (magnet)
            {
                PlayerController.instance.magnetattributeTimer = PlayerController.instance.buffDuration;
                FoodController.instance.firstmagnet = true;
                //Debug.Log("食用3");
            }
            
            FoodController.instance.existingItemPositions.Remove(this.transform.position);
            Destroy(this.gameObject);
            SFXManager.instance.PlaySFXPitched(1);
        }
        
        
    }
}
