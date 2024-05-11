using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 用于检测鼠标点击事件

public class OptionSelector : MonoBehaviour
{
    public KeyCode selectKey = KeyCode.Return; // 确认选择的键盘按键
    public KeyCode leftKey = KeyCode.Q; // 向左选择的键盘按键
    public KeyCode rightKey = KeyCode.E; // 向右选择的键盘按键
    private bool firstTouch = true;
    public LevelUpSelectionButton[] options; // 所有选项对象,我觉得来一个光层比较好？或者说改变一下光泽

    private int selectedOptionIndex; // 当前选中的选项索引

    void Start()
    {
        // 默认选中第一个选项
        //SelectOption(0);
    }

    void Update()
    {
        // 检测键盘输入
        if ((Input.GetKeyDown(leftKey)|| Input.GetKeyDown(rightKey))&&firstTouch)
        {
            SelectOption(1);
            firstTouch = false;
            options[1].panel.SetActive(false);
            options[0].panel.SetActive(true);
            options[2].panel.SetActive(true);
        }
        else { 
        if (Input.GetKeyDown(leftKey))
        {
            SelectOption(selectedOptionIndex - 1);
        }
        else if (Input.GetKeyDown(rightKey))
        {
            SelectOption(selectedOptionIndex + 1);
        }
        else if (Input.GetKeyDown(selectKey))
        {
            ConfirmSelection();
        }
        }
    }

    public void SelectOption(int index)
    {
        // 确保索引在选项范围内
        index = Mathf.Clamp(index, 0, options.Length - 1);

        // 取消之前选中的选项
        if (options[selectedOptionIndex] != null)
        {
            options[selectedOptionIndex].panel.SetActive(true);
        }

        // 显示新选中的选项
        selectedOptionIndex = index;
        options[selectedOptionIndex].panel.SetActive(false);
    }

    public void ConfirmSelection()
    {
        // 调用确认选中选项的逻辑
        options[selectedOptionIndex].SelectUpgrade();
        firstTouch = true;//并且重置下一次
        options[1].panel.SetActive(false);
        options[0].panel.SetActive(false);
        options[2].panel.SetActive(false);
    }

    // 检测鼠标点击事件
    public void OnPointerClick()
    {
        // 检查是否点击了当前选中的选项
        if (EventSystem.current.currentSelectedGameObject == options[selectedOptionIndex])
        {
            ConfirmSelection();
        }
    }
}