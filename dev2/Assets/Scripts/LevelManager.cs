using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public void Awake()
    {
        instance =this;
    }

    private bool gameActive;
    [HideInInspector]public float timer=0f;

    private float waitToShowEndScreen = 1f;

    
    void Start()
    {
        gameActive = true;
    }

    void Update()
    {
        if(gameActive == true)
        {
            timer += Time.deltaTime;
            UIController.instance.UpdateTimer(timer);//帮助UIcontroller更新时间
        }
    }

    public void EndLevel()//当游戏角色死亡时触发协程，触发协程
    {
        gameActive = false;

        StartCoroutine(EndLevelCo());
    }

    IEnumerator EndLevelCo()//设置停止游戏界面
    {
        yield return new WaitForSeconds(waitToShowEndScreen);

        //float minutes = Mathf.FloorToInt( timer / 60f);
        //float seconds = Mathf.FloorToInt( timer % 60f);

        //UIController.instance.endTimeText.text = minutes.ToString() + " mins" +seconds.ToString("00" + " secs");
        UIController.instance.levelEndScreen.SetActive(true);
    
    }
}
