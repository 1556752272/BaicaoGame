using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour
{
    public GameObject Button;
    public GameObject talkUI;
    public int num;
    public TextAsset dialogFile;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Button.SetActive(true);
           
            //if (num == 4)
            //{
            //    PlayerController.instance.ch4Join();
            //    Destroy(this.gameObject);
            //}
            //if (num == 2)
            //{
            //    PlayerController.instance.ch2Join();
            //    Destroy(this.gameObject);
            //}
            //if (num == 3)
            //{
            //    PlayerController.instance.ch3Join();
            //    Destroy(this.gameObject);
            //}

        }
        
    }

    // Update is called once per frame
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {   
            Button.SetActive(false);

        }
            
    }

    private void Update()
    {
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            updateDialog();
        }
    }
    public void updateDialog() {
        dialogSystem.instance.changeText(dialogFile,this.gameObject);
    }
    public void JoinCharacter()
    {
        if (num == 4)
        {
            PlayerController.instance.ch4Join();
            // Destroy(this.gameObject);
        }
        if (num == 2)
        {
            PlayerController.instance.ch2Join();
            // Destroy(this.gameObject);
        }
        if (num == 3)
        {
            PlayerController.instance.ch3Join();
            // Destroy(this.gameObject);
        }
    }
}