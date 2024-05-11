using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 3.0f;
    private void Update()
    {
        if(Vector3.Distance(PlayerController.instance.transform.position, transform.position) > 30.0f)//距离过远则子弹销毁
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerHealthController.instance.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
