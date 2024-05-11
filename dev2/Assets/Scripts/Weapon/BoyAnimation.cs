using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyAnimation : MonoBehaviour
{
    public AttackBoy boy;
    public Transform sprite;

    public float speed=2f;

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
        if (!boy.faceRight)
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
        }
        else
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
    }
}
