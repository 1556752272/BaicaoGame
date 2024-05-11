using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Potion : MonoBehaviour
{

    public PoisonDamager liquidPrefab; // 液体散开的Prefab
    public Transform targetpos; // 怪物的目标位置
    public Vector2 controlPoint; // 贝塞尔曲线的控制点
    private float duration = 4.0f; // 物品飞行的持续时间
    private float timer; // 用于计时的变量
    public Transform startTrans;
    public Transform endTrans;
    public LineRenderer lineRender;
    public Vector3[] _path;
    public float height;

    void Start()
    {
        //rb = this.gameObject.GetComponent<Rigidbody2D>();
        timer = duration; // 初始化计时器

    }

    void Update()
    {
        
    }


    public void Updateliquid(PoisonDamager e)
    {
        liquidPrefab = e;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            PoisonDamager p= Instantiate(liquidPrefab, transform.position, Quaternion.identity);
            if (ThrowPotion.instance.weaponLevel >= 5)
            {
                p.getbigger = true;
            }
            // 销毁药水瓶
            Destroy(gameObject);

        }
        
    }
    public void playpotion()
    {
        Instantiate(liquidPrefab, transform.position, Quaternion.identity);
    }

}
