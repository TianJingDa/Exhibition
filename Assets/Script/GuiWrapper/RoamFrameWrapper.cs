using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HedgehogTeam.EasyTouch;


public class RoamFrameWrapper : GuiFrameWrapper
{
    //private int tempModelID;//所选的户型

    private GameObject modelSwitchContentInRoam;
    private GameObject detailScrollViewBgInRoam;
    private GameObject detailScrollViewInRoam;
    private GameObject detailImageInRoam;
    private ScrollRect detailScrollRect;
    private Mask detailScrollMask;

    private Dropdown modelDropdownInRoam;

    private ETCJoystick joystickLeft;
    private ETCJoystick joystickRight;

    void Start()
    {
        id = GuiFrameID.RoamFrameWrapper;
        Init();
        detailScrollRect = detailScrollViewInRoam.GetComponent<ScrollRect>();
        detailScrollMask = detailScrollViewInRoam.GetComponent<Mask>();
        List<string> dropDownOptions = GameManager.Instance.GetAllModelNames();
        RefreshDropdown(modelDropdownInRoam, dropDownOptions);
        RefreshPlayer();
    }

    protected override void OnStart(Dictionary<string, GameObject> gameObjectDict)
    {
        modelSwitchContentInRoam    = gameObjectDict["ModelSwitchContentInRoam"];
        detailScrollViewBgInRoam    = gameObjectDict["DetailScrollViewBgInRoam"];
        detailScrollViewInRoam      = gameObjectDict["DetailScrollViewInRoam"];
        detailImageInRoam           = gameObjectDict["DetailImageInRoam"];
        modelDropdownInRoam         = gameObjectDict["ModelDropdownInRoam"].GetComponent<Dropdown>();
        joystickLeft                = gameObjectDict["JoystickLeft"].GetComponent<ETCJoystick>();
        joystickRight               = gameObjectDict["JoystickRight"].GetComponent<ETCJoystick>();
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
        dpd.value = GameManager.Instance.MainModelID;
        dpd.RefreshShownValue();
    }
    protected override void OnButtonClick(Button btn)
    {
        base.OnButtonClick(btn);

        switch (btn.name)
        {
            case "Roam2StartBtn":
                GameManager.Instance.SwitchStateAndModel(StateID.StartState);
                break;
            case "HideModelSwitchBtn":
                modelSwitchContentInRoam.SetActive(false);
                break;
            case "ModelSwitchBtnInRoam":
                modelSwitchContentInRoam.SetActive(!modelSwitchContentInRoam.activeSelf);
                break;
            case "BrowseStateBtnInRoam":                
                GameManager.Instance.SwitchStateAndModel(StateID.BrowseState);
                break;
            case "CompareStateBtnInRoam":
                GameManager.Instance.SwitchStateAndModel(StateID.CompareState);
                break;
            case "ResetBtnInRoam":
                GameManager.Instance.ResetState();
                break;
            case "DetailBtnInRoam":
                enableDoubleTap = false;
                GameManager.Instance.SetModelActive(false);
                detailScrollViewBgInRoam.SetActive(true);
                detailScrollRect.horizontal = false;
                detailScrollMask.enabled = true;
                detailImageInRoam.transform.localScale = Vector3.one * minScale;
                detailImageInRoam.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                Sprite sprite = GameManager.Instance.GetMainDetailSprite();
                CommonTool.AdjustContent(detailImageInRoam.GetComponent<Image>(), sprite);
                break;
            case "DetailScrollViewBgInRoam":
                enableDoubleTap = true;
                GameManager.Instance.SetModelActive(true);
                detailScrollViewBgInRoam.SetActive(false);
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
                GameManager.Instance.SwitchMainModel(dpd.value);
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
        joystickRight.axisY.directTransform = player.Find("Camera");
    }

    public void OnDoubleTap(Gesture gesture)
    {
        if (detailImageInRoam.transform.localScale != Vector3.one * minScale)
        {
            detailImageInRoam.transform.localScale = Vector3.one * minScale;
            detailScrollRect.horizontal = false;
            detailScrollMask.enabled = true;
            detailImageInRoam.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        else
        {
            detailImageInRoam.transform.localScale = Vector3.one * maxScale;
            detailScrollRect.horizontal = true;
            detailScrollMask.enabled = false;
        }
    }

    public void OnPinch(Gesture gesture)
    {
        Vector3 scale = detailImageInRoam.transform.localScale + Vector3.one * gesture.deltaPinch * Time.deltaTime * pinchSensibility;
        if (scale.x > maxScale || scale.y > maxScale || scale.z > maxScale)
        {
            detailImageInRoam.transform.localScale = maxScale * Vector3.one;
            return;
        }
        if (scale.x < minScale || scale.y < minScale || scale.z < minScale)
        {
            detailImageInRoam.transform.localScale = minScale * Vector3.one;
            return;
        }
        detailImageInRoam.transform.localScale = scale;
    }

    public void OnPinchEnd(Gesture gesture)
    {
        bool isMinScale = detailImageInRoam.transform.localScale == Vector3.one * minScale;
        detailScrollRect.horizontal = !isMinScale;
        detailScrollMask.enabled = isMinScale;
    }

}
