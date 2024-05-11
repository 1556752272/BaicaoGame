using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //普通的相机跟随函数
    private Transform target;
    
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
