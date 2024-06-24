using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        MoveToNPC(PlayerController.instance.gameObject);//��������ƶ�
        //MoveToPosition(new Vector3(10,0,0));
    }

    void Update()
    {
        
    }

    //����ĳ�������ƶ�
    void MoveToNPC(GameObject npc)
    {
        StartCoroutine(IMoveToNPC(npc));
    }
    IEnumerator IMoveToNPC(GameObject npc)
    {
        while (true)
        {

            // ���㳯����ҵķ�������
            Vector3 direction = (npc.transform.position - transform.position).normalized;
            //��ȡ����
            float moveSpeed = GetComponent<Unit>().moveSpeed;
            // �ƶ�����
            float distance = Vector3.Distance(npc.transform.position, transform.position);
            //����������1���ƶ�
            //if (distance > 1)
            //{
            //}

            transform.Translate(direction * moveSpeed * Time.deltaTime);
            //rb.velocity = direction * moveSpeed;

            //ת��Χ
            float turnRange = 0.1f;
            //������ұ�
            if ((transform.position.x - npc.transform.position.x > turnRange) && transform.localScale.x < 0)
            {
                Vector3 newScale = transform.localScale;
                newScale.x = -newScale.x;
                transform.localScale = newScale;
            }
            //��������
            if ((npc.transform.position.x - transform.position.x > turnRange) && transform.localScale.x > 0)
            {
                Vector3 newScale = transform.localScale;
                newScale.x = -newScale.x;
                transform.localScale = newScale;
            }

            // �ȴ���һ֡
            yield return null;
        }
    }

    //����ĳ�������ƶ�
    void MoveToPosition(Vector3 pos)
    {
        StartCoroutine(IMoveToPosition(pos));
    }
    IEnumerator IMoveToPosition(Vector3 pos)
    {
        while (true)
        {

            // ���㷽��
            Vector3 direction = (pos - transform.position).normalized;
            //��ȡ����
            float moveSpeed = GetComponent<Unit>().moveSpeed;
            // �ƶ�����
            float distance = Vector3.Distance(pos, transform.position);

            if (distance < 0.1) {
                yield break;
            }

            transform.Translate(direction * moveSpeed * Time.deltaTime);

            // �ȴ���һ֡
            yield return null;
        }
    }
}
