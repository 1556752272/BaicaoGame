using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    //public EnemyController enemy;
    public Transform sprite;

    public float speed;

    public float minSize, maxSize;

    private float activeSize;

    // Start is called before the first frame update
    void Start()
    {
        activeSize = maxSize;

        speed = speed * Random.Range(.75f, 1.25f);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.transform.position.x <= this.transform.position.x)
        {
            sprite.localScale = Vector3.MoveTowards(sprite.localScale, Vector3.one * activeSize, speed * Time.deltaTime);
            if (sprite.localScale.x == activeSize)
            {
                if (activeSize == maxSize)
                {
                    activeSize = minSize;
                }
                else
                {
                    activeSize = maxSize;
                }
            }
        }else if (PlayerController.instance.transform.position.x > this.transform.position.x)
        {
            sprite.localScale = Vector3.MoveTowards(sprite.localScale, new Vector3(1, -1, 0) * -activeSize, speed * Time.deltaTime);
            if (sprite.localScale.x == -activeSize)
            {
                if (activeSize == maxSize)
                {
                    activeSize = minSize;
                }
                else
                {
                    activeSize = maxSize;
                }
            }
        }
        //sprite.localScale = Vector3.MoveTowards(sprite.localScale, Vector3.one * activeSize, speed * Time.deltaTime);
        //if (sprite.localScale.x == activeSize) {
        //    if (activeSize == maxSize)
        //    {
        //        activeSize = minSize;
        //    }
        //    else {
        //        activeSize = maxSize;
        //    }
        //}
        //if (!enemy.faceRight)
        //{
        //    sprite.localScale = Vector3.MoveTowards(sprite.localScale, Vector3.one * activeSize, speed * Time.deltaTime);
        //    if (sprite.localScale.x == activeSize)
        //    {
        //        if (activeSize == maxSize)
        //        {
        //            activeSize = minSize;
        //        }
        //        else
        //        {
        //            activeSize = maxSize;
        //        }
        //    }
        //}
        //else
        //{
        //    sprite.localScale = Vector3.MoveTowards(sprite.localScale, new Vector3(1, -1, 0) * -activeSize, speed * Time.deltaTime);
        //    if (sprite.localScale.x == -activeSize)
        //    {
        //        if (activeSize == maxSize)
        //        {
        //            activeSize = minSize;
        //        }
        //        else
        //        {
        //            activeSize = maxSize;
        //        }
        //    }
        //}
    }
}
