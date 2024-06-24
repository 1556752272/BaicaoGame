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
        MoveToNPC(PlayerController.instance.gameObject);//向着玩家移动
        //MoveToPosition(new Vector3(10,0,0));
    }

    void Update()
    {
        
    }

    //向着某个对象移动
    void MoveToNPC(GameObject npc)
    {
        StartCoroutine(IMoveToNPC(npc));
    }
    IEnumerator IMoveToNPC(GameObject npc)
    {
        while (true)
        {

            // 计算朝向玩家的方向向量
            Vector3 direction = (npc.transform.position - transform.position).normalized;
            //获取移速
            float moveSpeed = GetComponent<Unit>().moveSpeed;
            // 移动怪物
            float distance = Vector3.Distance(npc.transform.position, transform.position);
            //如果距离大于1才移动
            //if (distance > 1)
            //{
            //}

            transform.Translate(direction * moveSpeed * Time.deltaTime);
            //rb.velocity = direction * moveSpeed;

            //转身范围
            float turnRange = 0.1f;
            //如果在右边
            if ((transform.position.x - npc.transform.position.x > turnRange) && transform.localScale.x < 0)
            {
                Vector3 newScale = transform.localScale;
                newScale.x = -newScale.x;
                transform.localScale = newScale;
            }
            //如果在左边
            if ((npc.transform.position.x - transform.position.x > turnRange) && transform.localScale.x > 0)
            {
                Vector3 newScale = transform.localScale;
                newScale.x = -newScale.x;
                transform.localScale = newScale;
            }

            // 等待下一帧
            yield return null;
        }
    }

    //向着某个对象移动
    void MoveToPosition(Vector3 pos)
    {
        StartCoroutine(IMoveToPosition(pos));
    }
    IEnumerator IMoveToPosition(Vector3 pos)
    {
        while (true)
        {

            // 计算方向
            Vector3 direction = (pos - transform.position).normalized;
            //获取移速
            float moveSpeed = GetComponent<Unit>().moveSpeed;
            // 移动怪物
            float distance = Vector3.Distance(pos, transform.position);

            if (distance < 0.1) {
                yield break;
            }

            transform.Translate(direction * moveSpeed * Time.deltaTime);

            // 等待下一帧
            yield return null;
        }
    }
}
