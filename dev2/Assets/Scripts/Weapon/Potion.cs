using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Potion : MonoBehaviour
{

    public PoisonDamager liquidPrefab; // Һ��ɢ����Prefab
    public Transform targetpos; // �����Ŀ��λ��
    public Vector2 controlPoint; // ���������ߵĿ��Ƶ�
    private float duration = 4.0f; // ��Ʒ���еĳ���ʱ��
    private float timer; // ���ڼ�ʱ�ı���
    public Transform startTrans;
    public Transform endTrans;
    public LineRenderer lineRender;
    public Vector3[] _path;
    public float height;

    void Start()
    {
        //rb = this.gameObject.GetComponent<Rigidbody2D>();
        timer = duration; // ��ʼ����ʱ��

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
            // ����ҩˮƿ
            Destroy(gameObject);

        }
        
    }
    public void playpotion()
    {
        Instantiate(liquidPrefab, transform.position, Quaternion.identity);
    }

}
