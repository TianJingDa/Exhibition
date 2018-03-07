using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartFrameWrapper : GuiFrameWrapper
{

    void Start()
    {
        id = GuiFrameID.StartFrameWrapper;
        Init();
    }

    protected override void OnStart(Dictionary<string, GameObject> gameObjectDict)
    {

    }

    protected override void OnButtonClick(Button btn)
    {
        base.OnButtonClick(btn);

        switch (btn.name)
        {
            case "StartBtn":
                MyDebug.LogGreen("StartBtn");
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
            case "LayoutDropdown":
                MyDebug.LogGreen("LayoutDropdown");
                break;
            case "ModeDropdown":
                MyDebug.LogGreen("ModeDropdown");
                break;
            default:
                MyDebug.LogYellow("Can not find Dropdown:" + dpd.name);
                break;
        }
    }

}
