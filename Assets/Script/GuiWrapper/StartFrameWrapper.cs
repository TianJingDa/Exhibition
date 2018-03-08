using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartFrameWrapper : GuiFrameWrapper
{
    private int tempModelID;//所选的户型
    private int tempStateID;//所选的模式

    private Dropdown modelDropdown;
    private Dropdown stateDropdown;

    void Start()
    {
        id = GuiFrameID.StartFrameWrapper;
        Init();
        RefreshDropdown(modelDropdown);
        RefreshDropdown(stateDropdown);
    }

    protected override void OnStart(Dictionary<string, GameObject> gameObjectDict)
    {
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

    protected override void OnButtonClick(Button btn)
    {
        base.OnButtonClick(btn);

        switch (btn.name)
        {
            case "StartBtn":
                GameManager.Instance.CurMainModelID = (ModelID)tempModelID;
                GameManager.Instance.CurStateID = (StateID)tempStateID;
                GameManager.Instance.SwitchModelAndState();
                break;
            case "ComputeBtn":
                MyDebug.LogGreen("ComputeBtn");
                break;
            case "2DCodeBtn":
                MyDebug.LogGreen("2DCodeBtn");
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
