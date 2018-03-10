using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrowseFrameWrapper : GuiFrameWrapper
{
    private int tempModelID;//所选的户型

    private GameObject modelSwitchContentInBrowse;
    private Dropdown modelDropdownInBrowse;
    private Slider angleSliderInBrowse;

    void Start()
    {
        id = GuiFrameID.BrowseFrameWrapper;
        tempModelID = 0;
        Init();
        List<string> dropDownOptions = GameManager.Instance.GetAllModelNames();
        RefreshDropdown(modelDropdownInBrowse, dropDownOptions);
    }

    protected override void OnStart(Dictionary<string, GameObject> gameObjectDict)
    {
        modelSwitchContentInBrowse = gameObjectDict["ModelSwitchContentInBrowse"];
        modelDropdownInBrowse = gameObjectDict["ModelDropdownInBrowse"].GetComponent<Dropdown>();
        angleSliderInBrowse = gameObjectDict["AngleSliderInBrowse"].GetComponent<Slider>();
    }

    private void RefreshDropdown(Dropdown dpd, List<string> dropDownOptions)
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
            case "Browse2StartBtn":
                GameManager.Instance.SwitchStateAndModel(StateID.StartState,-1);
                break;
            case "ModelSwitchBtnInBrowse":
                modelSwitchContentInBrowse.SetActive(!modelSwitchContentInBrowse.activeSelf);
                break;
            case "CompairStateBtnInBrowse":
                GameManager.Instance.SwitchStateAndModel(StateID.CompareState, tempModelID);
                break;
            case "RoamStateBtnInBrowse":
                GameManager.Instance.SwitchStateAndModel(StateID.RoamState, tempModelID);
                break;
            case "ResetBtnInBrowse":
                angleSliderInBrowse.value = 0;
                GameManager.Instance.ResetState();
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
            case "ModelDropdownInBrowse":
                tempModelID = dpd.value;
                GameManager.Instance.SwitchMainModel(tempModelID);
                break;
            default:
                MyDebug.LogYellow("Can not find Dropdown:" + dpd.name);
                break;
        }
    }

    protected override void OnSliderClick(Slider sld)
    {
        base.OnSliderClick(sld);

        switch (sld.name)
        {
            case "AngleSliderInBrowse":
                GameManager.Instance.SetMainCameraAngle(sld.value);
                break;
            default:
                MyDebug.LogYellow("Can not find Slider:" + sld.name);
                break;
        }
    }

}
