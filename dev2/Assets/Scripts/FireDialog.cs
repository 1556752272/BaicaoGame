using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FireDialog : MonoBehaviour
{
    public static FireDialog instance;
    
    /// <summary>
    /// 对话文本，csv
    /// </summary>
    public TextAsset dialogDataFile;

    /// <summary>
    /// 左侧角色图像
    /// </summary>
    public Image spriteLeft;

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
    public List<Sprite> sprites = new List<Sprite>();

    /// <summary>
    /// 背景图片列表
    /// </summary>
    public List<Sprite> bgsprites = new List<Sprite>();

    /// <summary>
    /// 角色名字对应图片的字典
    /// </summary>
    Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();

    /// <summary>
    /// 背景名字对应图片的字典
    /// </summary>
    Dictionary<string, Sprite> bgimageDic = new Dictionary<string, Sprite>();

    /// <summary>
    /// 保存当前对话索引值
    /// </summary>
    public int dialogIndex;

    /// <summary>
    /// 对话文本，按行分割
    /// </summary>
    public string[] dialogRows;
    private float Timerr = 2;
    public GameObject spritewhite;
    public int num = 0;
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



    // Start is called before the first frame update
    void Start()
    {

       

    }
    public void Fire()
    {
        Debug.Log("duqu");
        ReadText(dialogDataFile);
        ShowDialogRow();

        
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Timerr >= 0)
        {
            Timerr -= Time.deltaTime;

        }
        if (Timerr <= 0 && Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("读取成功");
            OnClickNext();
        }
    }

    public void UpdateText(string _name, string _text)
    {
        nameText.text = _name;
        StartCoroutine(TypeDialog(_text));

    }

    public void UpdateImage(string _name, string _position)
    {
        if (_position == "左")
        {
            spriteLeft.sprite = imageDic[_name];
        }
        else if (_position == "右")
        {
            spriteRight.sprite = imageDic[_name];
        }
    }

    public void bgUpdateImage(string _name, string _position)
    {
        if (_position == "中")
        {
            spriteRight.sprite = bgimageDic[_name];
        }
        else if (_position == "右")
        {
            spriteLeft.sprite = bgimageDic[_name];
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
            if (num == 4) spritewhite.SetActive(true);
            string[] cells = row.Split(',');
            if (cells[0] == "a" && int.Parse(cells[1]) == dialogIndex)
            {

                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);
                bgUpdateImage(cells[6], cells[7]);

                dialogIndex = int.Parse(cells[5]);

                break;
            }
            else if (cells[0] == "b" && int.Parse(cells[1]) == dialogIndex)
            {

                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);
                bgUpdateImage(cells[6], cells[7]);

                dialogIndex = int.Parse(cells[5]);

                break;
            }
            else if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)
            {

                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);
                //bgUpdateImage(cells[6], cells[7]);

                dialogIndex = int.Parse(cells[5]);

                break;
            }
            else if (cells[0] == "&" && int.Parse(cells[1]) == dialogIndex)
            {

                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);
                bgUpdateImage(cells[6], cells[7]);

                dialogIndex = int.Parse(cells[5]);

                break;
            }
            else if (cells[0] == "%" && int.Parse(cells[1]) == dialogIndex)
            {

                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);
                bgUpdateImage(cells[6], cells[7]);

                dialogIndex = int.Parse(cells[5]);

                break;
            }
            else if (cells[0] == "!" && int.Parse(cells[1]) == dialogIndex)
            {

                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);
                bgUpdateImage(cells[6], cells[7]);

                dialogIndex = int.Parse(cells[5]);

                break;
            }
            else if (cells[0] == "@" && int.Parse(cells[1]) == dialogIndex)
            {

                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);
                bgUpdateImage(cells[6], cells[7]);

                dialogIndex = int.Parse(cells[5]);

                break;
            }
            else if (cells[0] == "*" && int.Parse(cells[1]) == dialogIndex)
            {

                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);
                bgUpdateImage(cells[6], cells[7]);

                dialogIndex = int.Parse(cells[5]);

                break;
            }
            else if (cells[0] == "END" && int.Parse(cells[1]) == dialogIndex)
            {
                Debug.Log("剧情结束");
                SceneManager.LoadScene(1);
                break;
            }
            num++;
        }
    }

    public void OnClickNext()
    {
        Timerr = 0.1f;
        ShowDialogRow();
    }

    private IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";

        foreach (char letter in dialog)
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / 30);
        }
    }
}
