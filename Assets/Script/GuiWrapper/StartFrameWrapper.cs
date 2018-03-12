using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartFrameWrapper : GuiFrameWrapper
{
    private int tempModelID;//所选的户型
    private int tempStateID;//所选的模式

    private GameObject computerContent;
    private GameObject shareContent;
    private Dropdown modelDropdown;
    private Dropdown stateDropdown;

    void Start()
    {
        id = GuiFrameID.StartFrameWrapper;
        tempModelID = 0;
        tempStateID = 0;
        Init();
        List<string> dropDownOptions = GameManager.Instance.GetAllModelNames();
        RefreshDropdown(modelDropdown, dropDownOptions);
        RefreshDropdown(stateDropdown);
    }

    protected override void OnStart(Dictionary<string, GameObject> gameObjectDict)
    {
        computerContent     = gameObjectDict["ComputerContent"];
        shareContent        = gameObjectDict["ShareContent"];
        modelDropdown       = gameObjectDict["ModelDropdown"].GetComponent<Dropdown>();
        stateDropdown       = gameObjectDict["StateDropdown"].GetComponent<Dropdown>();
    }

    /// <summary>
    /// 刷新Dropdown的状态
    /// </summary>
    /// <param name="dpd"></param>
    /// <param name="index"></param>
    private void RefreshDropdown(Dropdown dpd)
    {
        if (!dpd)
        {
            MyDebug.LogYellow("Dropdown is null!");
            return;
        }
        for (int i = 0; i < dpd.options.Count; i++)
        {
            dpd.options[i].text = GameManager.Instance.GetMutiLanguage(dpd.options[i].text);
        }
        dpd.value = 0;
        dpd.RefreshShownValue();
    }

    private void RefreshDropdown(Dropdown dpd,List<string> dropDownOptions)
    {
        if (!dpd)
        {
            MyDebug.LogYellow("Dropdown is null!");
            return;
        }
        dpd.ClearOptions();
        dpd.AddOptions(dropDownOptions);
        dpd.value = 0;
        dpd.RefreshShownValue();
    }
    protected override void OnButtonClick(Button btn)
    {
        base.OnButtonClick(btn);

        switch (btn.name)
        {
            case "StartBtn":
                GameManager.Instance.SwitchStateAndModel((StateID)tempStateID, tempModelID);
                break;
            case "ComputerBtn":
            case "ComputerContent":
                computerContent.SetActive(!computerContent.activeSelf);
                break;
            case "ShareBtn":
                shareContent.SetActive(!shareContent.activeSelf);
                break;
            case "WeChatBtn":
                break;
            case "WeChatMomentsBtn":
                break;
            case "SinaWeiboBtn":
                break;
            default:
                MyDebug.LogYellow("Can not find Button: " + btn.name);
                break;
        }
    }

    protected override void OnDropdownClick(Dropdown dpd)
    {
        base.OnDropdownClick(dpd);

        switch (dpd.name)
        {
            case "ModelDropdown":
                tempModelID = dpd.value;
                break;
            case "StateDropdown":
                tempStateID = dpd.value;
                break;
            default:
                MyDebug.LogYellow("Can not find Dropdown:" + dpd.name);
                break;
        }
    }

}
