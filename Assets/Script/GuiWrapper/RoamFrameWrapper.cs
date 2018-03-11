using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HedgehogTeam.EasyTouch;


public class RoamFrameWrapper : GuiFrameWrapper
{
    private int tempModelID;//所选的户型

    private GameObject modelSwitchContentInRoam;
    private Dropdown modelDropdownInRoam;

    private ETCJoystick joystickLeft;
    private ETCJoystick joystickRight;

    void Start()
    {
        id = GuiFrameID.RoamFrameWrapper;
        tempModelID = GameManager.Instance.MainModelID;
        Init();
        List<string> dropDownOptions = GameManager.Instance.GetAllModelNames();
        RefreshDropdown(modelDropdownInRoam, dropDownOptions);
        RefreshPlayer();
    }

    protected override void OnStart(Dictionary<string, GameObject> gameObjectDict)
    {
        modelSwitchContentInRoam = gameObjectDict["ModelSwitchContentInRoam"];
        modelDropdownInRoam = gameObjectDict["ModelDropdownInRoam"].GetComponent<Dropdown>();
        joystickLeft = gameObjectDict["JoystickLeft"].GetComponent<ETCJoystick>();
        joystickRight = gameObjectDict["JoystickRight"].GetComponent<ETCJoystick>();
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
        dpd.value = tempModelID;
        dpd.RefreshShownValue();
    }
    protected override void OnButtonClick(Button btn)
    {
        base.OnButtonClick(btn);

        switch (btn.name)
        {
            case "Roam2StartBtn":
                GameManager.Instance.SwitchStateAndModel(StateID.StartState, -1);
                break;
            case "HideModelSwitchBtn":
                modelSwitchContentInRoam.SetActive(false);
                break;
            case "ModelSwitchBtnInRoam":
                modelSwitchContentInRoam.SetActive(!modelSwitchContentInRoam.activeSelf);
                break;
            case "BrowseStateBtnInRoam":                
                GameManager.Instance.SwitchStateAndModel(StateID.BrowseState, tempModelID);
                break;
            case "CompareStateBtnInRoam":
                GameManager.Instance.SwitchStateAndModel(StateID.CompareState, tempModelID);
                break;
            case "ResetBtnInRoam":
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
            case "ModelDropdownInRoam":
                tempModelID = dpd.value;
                GameManager.Instance.SwitchMainModel(tempModelID);
                RefreshPlayer();
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
            case "AngleSliderInRoam":
                GameManager.Instance.SetMainCameraAngle(sld.value);
                break;
            default:
                MyDebug.LogYellow("Can not find Slider:" + sld.name);
                break;
        }
    }

    private void RefreshPlayer()
    {
        Transform player = GameManager.Instance.Player;
        joystickLeft.axisX.directTransform = player;
        joystickLeft.axisY.directTransform = player;
        joystickRight.axisX.directTransform = player;
        joystickRight.axisY.directTransform = player.FindChild("Camera");
    }
}
