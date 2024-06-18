using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

//测试参数
public class TestParameter : MonoBehaviour
{
    public static TestParameter instance;
    public bool isIndestructible = false;//是否无敌状态
    
    void Awake()
    {
        if (instance==null)
        {
            TestParameter.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //切换无敌状态
    public void ToggleIndestructible()
    {
        isIndestructible = !isIndestructible;
    }
    //调整游戏速度
    public Slider slider;
    public float timeScale;
    public void AdjustTimeScale()
    {
        timeScale = 1 + slider.value * 10;
        Time.timeScale = timeScale;
    }
}
