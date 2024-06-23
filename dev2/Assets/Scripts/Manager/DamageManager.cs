using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //table(attack,victim,damage)
    public static void ApplyDamage(Dictionary<string, dynamic> table)
    {
        
        table["victim"].GetComponent<Unit>().hp -= table["damage"];
    }
}
