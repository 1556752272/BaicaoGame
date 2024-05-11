using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController instance;
    
    public void Awake()
    {
        instance = this;
    }

    public DamageNumber numberToSpawn;
    public Transform numberCanvas;//生成的数字放在这个canvas下面

    private List<DamageNumber> numberPool = new List<DamageNumber>();

    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnDamage(float damageAmount,Vector3 location)
    {
        int rounded = Mathf.RoundToInt(damageAmount);

        DamageNumber newDamage = GetFromPool();
        newDamage.transform.position = location;
        newDamage.Setup(rounded);
        newDamage.gameObject.SetActive(true);
        
    }
    public void SpawnDamage(float damageAmount, Vector3 location,bool fire)
    {
        int rounded = Mathf.RoundToInt(damageAmount);

        DamageNumber newDamage = GetFromPool();
        newDamage.transform.position = location;
        newDamage.Setup(rounded);
        newDamage.damageText.color = Color.red;
        newDamage.gameObject.SetActive(true);

    }
    public void SpawnDamage(float damageAmount, Vector3 location, bool blood,bool blood2)
    {
        int rounded = Mathf.RoundToInt(damageAmount);

        DamageNumber newDamage = GetFromPool();
        newDamage.transform.position = location;
        newDamage.Setup(rounded);
        newDamage.damageText.color = Color.green;
        newDamage.gameObject.SetActive(true);

    }

    public DamageNumber GetFromPool()
    {
        DamageNumber numberToOutput = null;

        if (numberPool.Count == 0)
        {
            numberToOutput = Instantiate(numberToSpawn, numberCanvas);
        }
        else {
            numberToOutput = numberPool[0];//这里写的很奇怪
            numberPool.RemoveAt(0);
        }
        return numberToOutput;
    }

    public void PlaceInPool(DamageNumber numberToPlace)
    {
        numberToPlace.gameObject.SetActive(false);

        numberPool.Add(numberToPlace);
    }
}
