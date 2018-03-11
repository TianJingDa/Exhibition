using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompareFrameWrapper : GuiFrameWrapper
{
    private int tempMainModelID;//所选的主户型
    //private int tempViceModelID;//所选的副户型

    private GameObject modelSwitchContentInCompare;
    private Dropdown mainModelDropdownInCompare;
    private Dropdown viceModelDropdownInCompare;
    private Slider angleSliderInBrowse;


    void Start()
    {
        id = GuiFrameID.CompareFrameWrapper;
        tempMainModelID = GameManager.Instance.MainModelID;
        Init();
        List<string> dropDownOptions = GameManager.Instance.GetAllModelNames();
        RefreshDropdown(mainModelDropdownInCompare, dropDownOptions, tempMainModelID);
        RefreshDropdown(viceModelDropdownInCompare, dropDownOptions, 0);
    }

    protected override void OnStart(Dictionary<string, GameObject> gameObjectDict)
    {
        modelSwitchContentInCompare = gameObjectDict["ModelSwitchContentInCompare"];
        mainModelDropdownInCompare = gameObjectDict["MainModelDropdownInCompare"].GetComponent<Dropdown>();
        viceModelDropdownInCompare = gameObjectDict["ViceModelDropdownInCompare"].GetComponent<Dropdown>();
        angleSliderInBrowse = gameObjectDict["AngleSliderInBrowse"].GetComponent<Slider>();
    }

    private void RefreshDropdown(Dropdown dpd, List<string> dropDownOptions, int modelID)
    {
        if (!dpd)
        {
            MyDebug.LogYellow("Dropdown is null!");
            return;
        }
        dpd.ClearOptions();
        dpd.AddOptions(dropDownOptions);
        dpd.value = modelID;
        dpd.RefreshShownValue();
    }


    protected override void OnButtonClick(Button btn)
    {
        base.OnButtonClick(btn);

        switch (btn.name)
        {
            case "Compare2StartBtn":
                GameManager.Instance.SwitchStateAndModel(StateID.StartState, -1);
                break;
            case "ModelSwitchBtnInCompare":
                modelSwitchContentInCompare.SetActive(!modelSwitchContentInCompare.activeSelf);
                break;
            case "HideModelSwitchBtn":
                modelSwitchContentInCompare.SetActive(false);
                break;
            case "ResetBtnInCompare":
                angleSliderInBrowse.value = 0;
                GameManager.Instance.ResetState();
                break;
            case "BrowseStateBtnInCompare":
                GameManager.Instance.SwitchStateAndModel(StateID.BrowseState, tempMainModelID);
                break;
            case "RoamStateBtnInCompare":
                GameManager.Instance.SwitchStateAndModel(StateID.RoamState, tempMainModelID);
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
            case "MainModelDropdownInCompare":
                tempMainModelID = dpd.value;
                GameManager.Instance.SwitchMainModel(tempMainModelID);
                break;
            case "ViceModelDropdownInCompare":
                GameManager.Instance.SwitchViceModel(dpd.value);
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
                GameManager.Instance.SetCameraAngel(sld.value);
                break;
            default:
                MyDebug.LogYellow("Can not find Slider:" + sld.name);
                break;
        }
    }

}
