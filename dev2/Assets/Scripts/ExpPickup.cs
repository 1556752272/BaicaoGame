using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPickup : MonoBehaviour
{
    public int expValue;

    [HideInInspector]public bool movingToPlayer;
    public float moveSpeed;

    public float timeBetweenChecks = .2f;
    private float checkCounter;

    private PlayerController player;

    void Start()
    {
        player = PlayerHealthController.instance.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (movingToPlayer == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else {
            checkCounter -= Time.deltaTime;
            if (checkCounter <= 0) {
                checkCounter = timeBetweenChecks;

                if(Vector3.Distance(transform.position,player.transform.position) < player.pickupRange)
                {
                    movingToPlayer = true;
                    moveSpeed += player.moveSpeed;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            ExperienceLevelController.instance.GetExp(expValue);
            SFXManager.instance.PlaySFXPitched(9);
            Destroy(gameObject);
        }
    }
}
