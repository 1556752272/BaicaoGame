using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Levelupend : MonoBehaviour
{
    public float fadeDuration = 1f; // 淡出时间  
    public float timer = 0f;

    private void Start()
    {
        // 如果需要立即开始淡出，可以调用 StartFadeOut()  
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fadeDuration)
        {
            this.gameObject.SetActive(false);
            timer = 0f;
        }
    }

  
    
}
