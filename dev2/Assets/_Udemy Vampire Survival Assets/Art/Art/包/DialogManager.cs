using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    /// <summary>
    /// �Ի��ı���csv
    /// </summary>
    public TextAsset dialogDataFile;

    /// <summary>
    /// ����ɫͼ��
    /// </summary>
    public SpriteRenderer spriteLeft;

    /// <summary>
    /// �Ҳ��ɫͼ��
    /// </summary>
    public SpriteRenderer spriteRight;

    /// <summary>
    /// ��ɫ�����ı�
    /// </summary>
    public TextMeshProUGUI nameText;

    /// <summary>
    /// �Ի������ı�
    /// </summary>
    public TextMeshProUGUI dialogText;

    /// <summary>
    /// ��ɫͼƬ�б�
    /// </summary>
    public List<Sprite> sprites=new List<Sprite>();

    /// <summary>
    /// ��ɫ���ֶ�ӦͼƬ���ֵ�
    /// </summary>
    Dictionary<string,Sprite> imageDic= new Dictionary<string,Sprite>();

    /// <summary>
    /// ���浱ǰ�Ի�����ֵ
    /// </summary>
    public int dialogIndex;

    /// <summary>
    /// �Ի��ı������зָ�
    /// </summary>
    public string[] dialogRows;

    private void Awake()
    {
        imageDic["�ż���"] = sprites[0];
        imageDic["������"] = sprites[1];
    }



    // Start is called before the first frame update
    void Start()
    {
        ReadText(dialogDataFile);
        ShowDialogRow();
        //UpdateText("�ż���","�˸�");
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
        if(_position=="��")
        {
            spriteLeft.sprite = imageDic[_name];
        }
        else if(_position == "��")
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
        Debug.Log("��ȡ�ɹ�");
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
