using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour
{
    public static SkillController instance;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    //这里放三个计时器？
    public Ch1kill ch;
    public Ch1kill ch2;
    public Away ch3;
    public Ch1kill ch4;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void Charactor2join()
    //{
    //    button2.gameObject.SetActive(true);
    //}
    //public void Charactor3join()
    //{
    //    button3.gameObject.SetActive(true);
    //}
    //public void Charactor4join()
    //{
    //    button4.gameObject.SetActive(true);
    //}
    public void skill1start()
    {
        SFXManager.instance.PlaySFXPitched(8);
        ch.chansheng();
    }
    public void skill2start()
    {
        SFXManager.instance.PlaySFXPitched(8);
        ch2.changeheal(); 
    }
    public void skill3start()
    {
        SFXManager.instance.PlaySFXPitched(8);
        ch3.changeDamager();
    }
    public void skill4start()
    {
        SFXManager.instance.PlaySFXPitched(8);
        ch4.changeFireRain();
    }

}
