using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class dialogSystem : MonoBehaviour
{
    public static dialogSystem instance;
    // �Ի��ı���csv
    public TextAsset dialogDataFile;
    // ����ɫͼ��
    public SpriteRenderer spriteLeft;
    /// �Ҳ��ɫͼ��
    public SpriteRenderer spriteRight;
    /// ��ɫ�����ı�
    public TextMeshProUGUI nameText;
    /// �Ի������ı�
    public TextMeshProUGUI dialogText;
    /// ��ɫͼƬ�б�
    public List<Sprite> sprites = new List<Sprite>();
    /// ����ͼƬ�б�
    public List<Sprite> bgsprites = new List<Sprite>();
    /// ��ɫ���ֶ�ӦͼƬ���ֵ�
    Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();
    /// �������ֶ�ӦͼƬ���ֵ�
    Dictionary<string, Sprite> bgimageDic = new Dictionary<string, Sprite>();
    /// ���浱ǰ�Ի�����ֵ
    public int dialogIndex;
    /// �Ի��ı������зָ�
    public string[] dialogRows;
    private Coroutine currentCoroutine = null;
    public Image spriteLeft2;
    public Image spriteRight2;
    //public int textindex = 0;
    private bool talkStart = false;
    public GameObject darkImage;
    public GameObject Name;
    public GameObject Character_Destroy;
    private void Awake()
    {
        instance = this;
        imageDic["�����������"] = sprites[0];
        imageDic["������"] = sprites[1];
        imageDic["777"] = sprites[2];
        imageDic["������˹"] = sprites[3];
        imageDic["��ʯ�����"] = sprites[4];
        imageDic["�������"] = sprites[5];
        imageDic[""] = sprites[6];
        imageDic["����"] = sprites[7];
        imageDic["������"] = sprites[8];
        imageDic["��ũ"] = sprites[9];
        bgimageDic["����һ"] = bgsprites[0];
        bgimageDic["������"] = bgsprites[1];
        bgimageDic["������"] = bgsprites[2];
        bgimageDic["������"] = bgsprites[3];
        bgimageDic["������"] = bgsprites[4];
        bgimageDic["������"] = bgsprites[5];
        bgimageDic["������"] = bgsprites[6];
        bgimageDic["������"] = bgsprites[7];

    }
    void Start()
    {
        //dialogIndex = 1;
        //ReadText(dialogDataFile);
        //ShowDialogRow();
        //string row = "!,29,,,,30,beijing,zhong,";
        //string[] cells = row.Split(',');
        //foreach(string s in cells)
        //{
        //    Debug.Log(s);
        //}
    }
    void Update()
    {
        //if (Timerr >= 0)
        //{#,7,�����������,��,������һЩ������ֲ�ʳ�ú���Խ����ǵĳ�����������һ��ȫ�µĸ߶ȡ����ǵ����񣬾����ڼ��޻����£���������ֲ���Ч����,8,����һ,��,��

        //    Timerr -= Time.deltaTime;

        //}
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))&& talkStart)//Timerr <= 0 &&
        {
            //OnClickNext();
            //ReadText(dialogDataFile);
            spriteLeft2.gameObject.SetActive(false);
            //spriteRight2.gameObject.SetActive(false);
            ShowDialogRow();



        }
    }
    public void changeLeftCh(Sprite newSprite)
    {
        if (spriteLeft2.gameObject.activeSelf == false) spriteLeft2.gameObject.SetActive(true);
        if (newSprite != null)
        {
            // ��ȡ��Sprite�Ŀ�߱�  
            float spriteAspectRatio = (float)newSprite.bounds.size.y / newSprite.bounds.size.x;

            // ����RectTransform��anchor point��(0.5, 0.5)��������  
            // ��ȡ��ǰImage�Ŀ�ȣ��������Ѿ�����Ҫ�Ŀ�ȣ�  
            float currentHeight = spriteLeft2.rectTransform.sizeDelta.y;

            // �����µĸ߶�  
            float newWeight = currentHeight * spriteAspectRatio;

            // �����µĸ߶ȣ����ֿ�Ȳ���  
            Vector2 newSize = new Vector2(newWeight, currentHeight);
            spriteLeft2.rectTransform.sizeDelta = newSize;

            // �����µ�Sprite  
            spriteLeft2.sprite = newSprite;
        }
        //float spriteWidth = newSprite.rect.width;
        //float spriteHeight = newSprite.rect.height;
        //float spriteRatio = spriteWidth / spriteHeight;

        //// ���ݿ�߱�����Image�ĳߴ�  
        //RectTransform rectTransform = spriteLeft2.rectTransform;
        //Vector2 newSize = rectTransform.sizeDelta;
        //Debug.Log(newSize);
        //if (spriteRatio > 1f) // Sprite�Ͽ�  
        //{
        //    newSize.y = newSize.x / spriteRatio; // ���ֿ�Ȳ��䣬���ݿ�߱ȵ����߶�  
        //}
        //else // Sprite�ϸ߻������  
        //{
        //    newSize.x = newSize.y * spriteRatio; // ���ָ߶Ȳ��䣬���ݿ�߱ȵ������  
        //}

        //rectTransform.sizeDelta = newSize;
        //Debug.Log(newSize);
        //// ��󣬽��µ�SpriteӦ�õ�Image���  
        //spriteLeft2.sprite = newSprite;

    }
    public void changeRightCh(Sprite newSprite)
    {
        if (spriteRight2.gameObject.activeSelf == false) spriteLeft2.gameObject.SetActive(true);
        spriteRight2.sprite = newSprite;
        //float spriteWidth = newSprite.rect.width;
        //float spriteHeight = newSprite.rect.height;
        //float spriteRatio = spriteWidth / spriteHeight;

        //// ���ݿ�߱�����Image�ĳߴ�  
        //RectTransform rectTransform = spriteRight2.rectTransform;
        //Vector2 newSize = rectTransform.sizeDelta;
        //Debug.Log(newSize);
        //if (spriteRatio > 1f) // Sprite�Ͽ�  
        //{
        //    newSize.y = newSize.x / spriteRatio; // ���ֿ�Ȳ��䣬���ݿ�߱ȵ����߶�  
        //}
        //else // Sprite�ϸ߻������  
        //{
        //    newSize.x = newSize.y * spriteRatio; // ���ָ߶Ȳ��䣬���ݿ�߱ȵ������  
        //}

        //rectTransform.sizeDelta = newSize;
        //Debug.Log(newSize);
        //// ��󣬽��µ�SpriteӦ�õ�Image���  
        //spriteRight2.sprite = newSprite;

    }
    public void UpdateText(string _name, string _text)
    {
        nameText.text = _name;
        Debug.Log("nameText.text" + nameText.text);
        Debug.Log("_text" + _text);
        //Debug.Log(222);
        if (currentCoroutine != null)
        {
            // �����Э���������У�����ֹͣ��  
            StopCoroutine(currentCoroutine);
        }

        // �����µ�Э�̲�����������  
        //currentCoroutine = StartCoroutine(TypeDialog(_text));
        dialogText.text = "";
        dialogText.text += _text;
    }
    public void UpdateImage(string _name, string _position)
    {
        //Debug.Log(_name);
        //Debug.Log(_position);
        //Debug.Log(111);
        if (_position == "��")
        {
            if (spriteLeft2.gameObject.activeSelf == false)
            {
                spriteLeft2.gameObject.SetActive(true);
            }
            changeLeftCh(imageDic[_name]);
        }
        else if (_position == "��")
        {
            if (spriteRight.gameObject.activeSelf == false)
            {
                spriteRight.gameObject.SetActive(true);
            }
            changeRightCh(imageDic[_name]);
        }
    }
    public void bgUpdateImage(string _name, string _position)
    {
        if (_position == "��")
        {
            if (spriteRight.gameObject.activeSelf == false)
            {
                spriteRight.gameObject.SetActive(true);
            }
            changeRightCh(bgimageDic[_name]);
        }
        else if (_position == "��")
        {
            if (spriteLeft.gameObject.activeSelf == false)
            {
                spriteLeft.gameObject.SetActive(true);
            }
            spriteLeft.sprite = bgimageDic[_name];
        }
    }
    public void ReadText(TextAsset _textAsset)
    {
        Debug.Log("ִ��ReadText");

        dialogRows = _textAsset.text.Split('\n');

    }

    public void ShowDialogRow()
    {
        //foreach (var row in dialogRows)
        // {
        string row = dialogRows[dialogIndex];
        // Debug.Log("���:" + row);
        //if(num==4)spritewhite.SetActive(true);
        string[] cells = row.Split(',');
        // Debug.Log("���:"+cells[1]);
        //Debug.Log(int.Parse(cells[1]));
        //Debug.Log(dialogIndex);
        if (cells[0] != "END" && int.Parse(cells[1]) == dialogIndex - 1)
        {
            //Debug.Log("!!!!!!!!!!");
            //foreach(string c in cells){
            //    Debug.Log(c);
            //}
            UpdateText(cells[2], cells[4]);
            UpdateImage(cells[2], cells[3]);
            //bgUpdateImage(cells[6], cells[7]);//����Ҳ�ϣ�����±�����

            dialogIndex = int.Parse(cells[5]) + 1;//dialogIndex++
        }
        else if (cells[0] == "END" && int.Parse(cells[1]) == dialogIndex - 1)
        {
            Debug.Log("�������");
            darkImage.SetActive(false);
            Name.SetActive(false);
            spriteLeft2.gameObject.SetActive(false);
            talkStart = false;
            Time.timeScale = 1f;
            if (Character_Destroy != null)
            {
                Character_Destroy.GetComponent<npc>().JoinCharacter();
                Destroy(Character_Destroy);
            }

            //SceneManager.LoadScene(1);
        }
        // num++;
    }
    //public void OnClickNext()
    //{
    //    // Timerr = 2f;
    //    ShowDialogRow();
    //}

    private IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";
        dialogText.text += dialog;
        foreach (char letter in dialog)
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / 30);
        }
    }
    //else if (cells[0] == "b" && int.Parse(cells[1]) == dialogIndex)
    //{

    //    UpdateText(cells[2], cells[4]);
    //    UpdateImage(cells[2], cells[3]);
    //    bgUpdateImage(cells[6], cells[7]);

    //    dialogIndex = int.Parse(cells[5]);

    //    break;
    //}
    //else if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)
    //{

    //    UpdateText(cells[2], cells[4]);
    //    UpdateImage(cells[2], cells[3]);
    //    bgUpdateImage(cells[6], cells[7]);

    //    dialogIndex = int.Parse(cells[5]);

    //    break;
    //}
    //else if (cells[0] == "&" && int.Parse(cells[1]) == dialogIndex)
    //{

    //    UpdateText(cells[2], cells[4]);
    //    UpdateImage(cells[2], cells[3]);
    //    bgUpdateImage(cells[6], cells[7]);

    //    dialogIndex = int.Parse(cells[5]);

    //    break;
    //}
    //else if (cells[0] == "%" && int.Parse(cells[1]) == dialogIndex)
    //{

    //    UpdateText(cells[2], cells[4]);
    //    UpdateImage(cells[2], cells[3]);
    //    bgUpdateImage(cells[6], cells[7]);

    //    dialogIndex = int.Parse(cells[5]);

    //    break;
    //}
    //else if (cells[0] == "!" && int.Parse(cells[1]) == dialogIndex)
    //{

    //    UpdateText(cells[2], cells[4]);
    //    UpdateImage(cells[2], cells[3]);
    //    bgUpdateImage(cells[6], cells[7]);

    //    dialogIndex = int.Parse(cells[5]);

    //    break;
    //}
    //else if (cells[0] == "@" && int.Parse(cells[1]) == dialogIndex)
    //{

    //    UpdateText(cells[2], cells[4]);
    //    UpdateImage(cells[2], cells[3]);
    //    bgUpdateImage(cells[6], cells[7]);

    //    dialogIndex = int.Parse(cells[5]);

    //    break;
    //}
    //else if (cells[0] == "*" && int.Parse(cells[1]) == dialogIndex)
    //{

    //    UpdateText(cells[2], cells[4]);
    //    UpdateImage(cells[2], cells[3]);
    //    bgUpdateImage(cells[6], cells[7]);

    //    dialogIndex = int.Parse(cells[5]);

    //    break;
    //}
    public event EventHandler emd;
    public void skipTalk()
    {

        SceneManager.LoadScene(1);
    }
    public void changeText(TextAsset dialogFile)//�ı��ı����������
    {
        Time.timeScale = 0f;
        dialogDataFile = dialogFile;
        dialogIndex = 1;
        ReadText(dialogDataFile);
        talkStart = true;
        darkImage.SetActive(true);
        Name.SetActive(true);
        ShowDialogRow();
        
    }
    public void changeText(TextAsset dialogFile,GameObject gt)//�ı��ı����������
    {
        Time.timeScale = 0f;
        dialogDataFile = dialogFile;
        dialogIndex = 1;
        ReadText(dialogDataFile);
        talkStart = true;
        darkImage.SetActive(true);
        Name.SetActive(true);
        Character_Destroy = gt;
        ShowDialogRow();
        
    }
}
