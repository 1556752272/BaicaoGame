using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierThrow : MonoBehaviour
{
    //public Transform target; // 怪物的目标位置
    public Vector2 controlPoint; // 贝塞尔曲线的控制点
    private float duration = 2.0f; // 物品飞行的持续时间
    private float timer; // 用于计时的变量

    private bool initialized = false;
    [SerializeField] private Vector2 startPos;
    private Vector2 midPos;
    
    private float percent = 0;
    private float percentSpeed = 0;
    [SerializeField] private float speed = 10;
    public Transform target;
    [SerializeField] private int level = 1;
    private Vector2 lastTargetPos;
  //  private IBulletBasic bulletBasic;
   // private Damage damage;
    private bool alive = true;
    void Start()
    {
        timer = duration; // 初始化计时器
                          // controlPoint = (transform.position + target.position);
    }

    void Update()
    {
        timer -= Time.deltaTime; // 减少计时器

        if (timer >= 0)
        {
            percent += percentSpeed * Time.deltaTime;
            if (percent > 1)
                percent = 1;
            transform.position = Bezier(percent, startPos, midPos, lastTargetPos);

            if (timer <= 0)
            {
                Potion p=this.gameObject.GetComponent<Potion>();//我希望时间到了他也会自己碎裂
                p.playpotion();
                Destroy(gameObject);
            }
        }
        else
        {
            // 飞行时间结束，销毁物品
            Destroy(gameObject);
        }
    }
    public void Init( GameObject parent) { 
           
            //GameObject[] enemys = Game0bject.FindGame0bjectswithTag(Consts.TAG_ENEMY);
        //    if (enemys.Length > 0)
        //    int rdId = Random.Range(0， enemys.Length);
        //    target = enemys[rdId].transform;
            lastTargetPos = target.position;
        //else {
        //    lastTargetPos = Utils.GetMousePosition();
            startPos = parent.transform.position;
            midPos = GetMiddlePosition(parent.transform.position, lastTargetPos);
            percentSpeed = speed/ (lastTargetPos - startPos).magnitude;
            transform.position = startPos;
            percent = 0;
            initialized = true;
    }
    Vector2 GetMiddlePosition(Vector2 a, Vector2 b) { 
        Vector2 m = Vector2.Lerp(a, b,0.1f);
        Vector2 normal = Vector2.Perpendicular(a - b).normalized;
        float rd = Random.Range(-2f,2f);
        float curveRatio = 0.3f;
        return m + (a - b).magnitude* curveRatio * rd* normal;
    }
    public static Vector2 Bezier(float t, Vector2 a, Vector2 b, Vector2 c)
    {
        var ab = Vector2.Lerp(a, b,t);
        var bc = Vector2.Lerp(b,c,t);
        return Vector2.Lerp(ab, bc,t);
    }

}
