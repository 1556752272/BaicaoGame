using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Weapon
{   //update还在移动位置
    public float moveSpeed;//武器直线飞行速度

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }
}
