using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem3 : MonoBehaviour
{
    [Header("UI组件")]
    public TMP_Text textLabel;
    public Image faceIm1age;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index;

    List<string> textlist = new List<string>();
    // Start is called before the first frame update
    void Awake()
    {
        GetTextFromFile(textFile);
    }

    private void OnEnable()
    {
        textLabel.text = textlist[index];
        index++;
        Time.timeScale=0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && index == textlist.Count)
        {
            
            index = 0;
            Time.timeScale = 1f;
            PlayerController.instance.ch4Join();//自行设置对话结束做什么
            gameObject.SetActive(false);

            return;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            textLabel.text = textlist[index];
            index++;
        }
    }

    void GetTextFromFile(TextAsset file)
    {
        textlist.Clear();
        index = 0;

        var lineDate = file.text.Split('\n');

        foreach (var line in lineDate)
        {
            textlist.Add(line);
        }
    }
}
