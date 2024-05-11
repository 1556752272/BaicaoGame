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
    // 对话文本，csv
    public TextAsset dialogDataFile;
    // 左侧角色图像
    public SpriteRenderer spriteLeft;
    /// 右侧角色图像
    public SpriteRenderer spriteRight;
    /// 角色名字文本
    public TextMeshProUGUI nameText;
    /// 对话内容文本
    public TextMeshProUGUI dialogText;
    /// 角色图片列表
    public List<Sprite> sprites = new List<Sprite>();
    /// 背景图片列表
    public List<Sprite> bgsprites = new List<Sprite>();
    /// 角色名字对应图片的字典
    Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();
    /// 背景名字对应图片的字典
    Dictionary<string, Sprite> bgimageDic = new Dictionary<string, Sprite>();
    /// 保存当前对话索引值
    public int dialogIndex;
    /// 对话文本，按行分割
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
        imageDic["监狱长佩德罗"] = sprites[0];
        imageDic["囚犯们"] = sprites[1];
        imageDic["777"] = sprites[2];
        imageDic["米米西斯"] = sprites[3];
        imageDic["磐石艾瑞克"] = sprites[4];
        imageDic["火焰杰瑞"] = sprites[5];
        imageDic[""] = sprites[6];
        imageDic["众人"] = sprites[7];
        imageDic["哆瑞咪"] = sprites[8];
        imageDic["神农"] = sprites[9];
        bgimageDic["背景一"] = bgsprites[0];
        bgimageDic["背景二"] = bgsprites[1];
        bgimageDic["背景三"] = bgsprites[2];
        bgimageDic["背景四"] = bgsprites[3];
        bgimageDic["背景五"] = bgsprites[4];
        bgimageDic["背景六"] = bgsprites[5];
        bgimageDic["背景零"] = bgsprites[6];
        bgimageDic["背景七"] = bgsprites[7];

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
        //{#,7,监狱长佩德罗,左,岛上有一些罕见的植物，食用后可以将你们的超能力提升到一个全新的高度。你们的任务，就是在极限环境下，试验这种植物的效果。,8,背景一,中,左

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
            // 获取新Sprite的宽高比  
            float spriteAspectRatio = (float)newSprite.bounds.size.y / newSprite.bounds.size.x;

            // 假设RectTransform的anchor point是(0.5, 0.5)，即中心  
            // 获取当前Image的宽度（假设它已经是想要的宽度）  
            float currentHeight = spriteLeft2.rectTransform.sizeDelta.y;

            // 计算新的高度  
            float newWeight = currentHeight * spriteAspectRatio;

            // 设置新的高度，保持宽度不变  
            Vector2 newSize = new Vector2(newWeight, currentHeight);
            spriteLeft2.rectTransform.sizeDelta = newSize;

            // 设置新的Sprite  
            spriteLeft2.sprite = newSprite;
        }
        //float spriteWidth = newSprite.rect.width;
        //float spriteHeight = newSprite.rect.height;
        //float spriteRatio = spriteWidth / spriteHeight;

        //// 根据宽高比设置Image的尺寸  
        //RectTransform rectTransform = spriteLeft2.rectTransform;
        //Vector2 newSize = rectTransform.sizeDelta;
        //Debug.Log(newSize);
        //if (spriteRatio > 1f) // Sprite较宽  
        //{
        //    newSize.y = newSize.x / spriteRatio; // 保持宽度不变，根据宽高比调整高度  
        //}
        //else // Sprite较高或宽高相等  
        //{
        //    newSize.x = newSize.y * spriteRatio; // 保持高度不变，根据宽高比调整宽度  
        //}

        //rectTransform.sizeDelta = newSize;
        //Debug.Log(newSize);
        //// 最后，将新的Sprite应用到Image组件  
        //spriteLeft2.sprite = newSprite;

    }
    public void changeRightCh(Sprite newSprite)
    {
        if (spriteRight2.gameObject.activeSelf == false) spriteLeft2.gameObject.SetActive(true);
        spriteRight2.sprite = newSprite;
        //float spriteWidth = newSprite.rect.width;
        //float spriteHeight = newSprite.rect.height;
        //float spriteRatio = spriteWidth / spriteHeight;

        //// 根据宽高比设置Image的尺寸  
        //RectTransform rectTransform = spriteRight2.rectTransform;
        //Vector2 newSize = rectTransform.sizeDelta;
        //Debug.Log(newSize);
        //if (spriteRatio > 1f) // Sprite较宽  
        //{
        //    newSize.y = newSize.x / spriteRatio; // 保持宽度不变，根据宽高比调整高度  
        //}
        //else // Sprite较高或宽高相等  
        //{
        //    newSize.x = newSize.y * spriteRatio; // 保持高度不变，根据宽高比调整宽度  
        //}

        //rectTransform.sizeDelta = newSize;
        //Debug.Log(newSize);
        //// 最后，将新的Sprite应用到Image组件  
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
            // 如果有协程正在运行，则先停止它  
            StopCoroutine(currentCoroutine);
        }

        // 启动新的协程并保存其引用  
        //currentCoroutine = StartCoroutine(TypeDialog(_text));
        dialogText.text = "";
        dialogText.text += _text;
    }
    public void UpdateImage(string _name, string _position)
    {
        //Debug.Log(_name);
        //Debug.Log(_position);
        //Debug.Log(111);
        if (_position == "左")
        {
            if (spriteLeft2.gameObject.activeSelf == false)
            {
                spriteLeft2.gameObject.SetActive(true);
            }
            changeLeftCh(imageDic[_name]);
        }
        else if (_position == "右")
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
        if (_position == "中")
        {
            if (spriteRight.gameObject.activeSelf == false)
            {
                spriteRight.gameObject.SetActive(true);
            }
            changeRightCh(bgimageDic[_name]);
        }
        else if (_position == "右")
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
        Debug.Log("执行ReadText");

        dialogRows = _textAsset.text.Split('\n');

    }

    public void ShowDialogRow()
    {
        //foreach (var row in dialogRows)
        // {
        string row = dialogRows[dialogIndex];
        // Debug.Log("序号:" + row);
        //if(num==4)spritewhite.SetActive(true);
        string[] cells = row.Split(',');
        // Debug.Log("序号:"+cells[1]);
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
            //bgUpdateImage(cells[6], cells[7]);//这次我不希望更新背景了

            dialogIndex = int.Parse(cells[5]) + 1;//dialogIndex++
        }
        else if (cells[0] == "END" && int.Parse(cells[1]) == dialogIndex - 1)
        {
            Debug.Log("剧情结束");
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
    public void changeText(TextAsset dialogFile)//改变文本后重置序号
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
    public void changeText(TextAsset dialogFile,GameObject gt)//改变文本后重置序号
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
