using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    /// <summary>
    /// 对话文本，csv
    /// </summary>
    public TextAsset dialogDataFile;

    /// <summary>
    /// 左侧角色图像
    /// </summary>
    public SpriteRenderer spriteLeft;

    /// <summary>
    /// 右侧角色图像
    /// </summary>
    public SpriteRenderer spriteRight;

    /// <summary>
    /// 角色名字文本
    /// </summary>
    public TextMeshProUGUI nameText;

    /// <summary>
    /// 对话内容文本
    /// </summary>
    public TextMeshProUGUI dialogText;

    /// <summary>
    /// 角色图片列表
    /// </summary>
    public List<Sprite> sprites=new List<Sprite>();

    /// <summary>
    /// 角色名字对应图片的字典
    /// </summary>
    Dictionary<string,Sprite> imageDic= new Dictionary<string,Sprite>();

    /// <summary>
    /// 保存当前对话索引值
    /// </summary>
    public int dialogIndex;

    /// <summary>
    /// 对话文本，按行分割
    /// </summary>
    public string[] dialogRows;

    private void Awake()
    {
        imageDic["张家铭"] = sprites[0];
        imageDic["永次郎"] = sprites[1];
    }



    // Start is called before the first frame update
    void Start()
    {
        ReadText(dialogDataFile);
        ShowDialogRow();
        //UpdateText("张家铭","八嘎");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(string _name,string _text)
    {
        nameText.text = _name;
        dialogText.text = _text;
    }

    public void UpdateImage(string _name,string _position)
    {
        if(_position=="左")
        {
            spriteLeft.sprite = imageDic[_name];
        }
        else if(_position == "右")
        {
            spriteRight.sprite = imageDic[_name];
        }
    }

    public void ReadText(TextAsset _textAsset)
    {
        dialogRows = _textAsset.text.Split('\n');
        //foreach (var row in rows)
        //{
        //    string[] cell = row.Split(',');

        //}
        Debug.Log("读取成功");
    }

    public void ShowDialogRow()
    {
        foreach (var row in dialogRows)
        {
            string[] cells = row.Split(',');
            if (cells[0]=="#"&& int.Parse(cells[1])==dialogIndex)
            {
                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);

                dialogIndex=int.Parse(cells[5]);
                break;
            }

        }
    }


}
