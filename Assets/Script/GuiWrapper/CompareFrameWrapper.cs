using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HedgehogTeam.EasyTouch;

public class CompareFrameWrapper : GuiFrameWrapper
{
    //private int tempMainModelID;//所选的主户型
    //private int tempViceModelID;//所选的副户型

    private GameObject modelSwitchContentInCompare;
    private GameObject detailScrollViewBgInCompare;
    private GameObject detailScrollViewInCompare;
    private GameObject detailImageInCompare;
    private ScrollRect detailScrollRect;
    private Mask detailScrollMask;
    private Dropdown mainModelDropdownInCompare;
    private Dropdown viceModelDropdownInCompare;
    private Slider angleSliderInCompare;


    void Start()
    {
        id = GuiFrameID.CompareFrameWrapper;
        Init();
        detailScrollRect = detailScrollViewInCompare.GetComponent<ScrollRect>();
        detailScrollMask = detailScrollViewInCompare.GetComponent<Mask>();
        List<string> dropDownOptions = GameManager.Instance.GetAllModelNames();
        RefreshDropdown(mainModelDropdownInCompare, dropDownOptions, GameManager.Instance.MainModelID);
        RefreshDropdown(viceModelDropdownInCompare, dropDownOptions, 0);
    }

    protected override void OnStart(Dictionary<string, GameObject> gameObjectDict)
    {
        modelSwitchContentInCompare         = gameObjectDict["ModelSwitchContentInCompare"];
        detailScrollViewBgInCompare         = gameObjectDict["DetailScrollViewBgInCompare"];
        detailScrollViewInCompare           = gameObjectDict["DetailScrollViewInCompare"];
        detailImageInCompare                = gameObjectDict["DetailImageInCompare"];
        mainModelDropdownInCompare          = gameObjectDict["MainModelDropdownInCompare"].GetComponent<Dropdown>();
        viceModelDropdownInCompare          = gameObjectDict["ViceModelDropdownInCompare"].GetComponent<Dropdown>();
        angleSliderInCompare                = gameObjectDict["AngleSliderInCompare"].GetComponent<Slider>();
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
                GameManager.Instance.SwitchStateAndModel(StateID.StartState);
                break;
            case "ModelSwitchBtnInCompare":
                modelSwitchContentInCompare.SetActive(!modelSwitchContentInCompare.activeSelf);
                break;
            case "HideModelSwitchBtn":
                modelSwitchContentInCompare.SetActive(false);
                break;
            case "ResetBtnInCompare":
                angleSliderInCompare.value = 0;
                GameManager.Instance.ResetState();
                break;
            case "BrowseStateBtnInCompare":
                GameManager.Instance.SwitchStateAndModel(StateID.BrowseState);
                break;
            case "RoamStateBtnInCompare":
                GameManager.Instance.SwitchStateAndModel(StateID.RoamState);
                break;
            case "MainDetailBtnInCompare":
                enableDoubleTap = false;
                GameManager.Instance.SetModelActive(false, false);
                detailScrollViewBgInCompare.SetActive(true);
                detailScrollRect.horizontal = false;
                detailScrollMask.enabled = true;
                detailImageInCompare.transform.localScale = Vector3.one * minScale;
                detailImageInCompare.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                Sprite mainSprite = GameManager.Instance.GetMainDetailSprite();
                CommonTool.AdjustContent(detailImageInCompare.GetComponent<Image>(), mainSprite);
                break;
            case "ViceDetailBtnInCompare":
                enableDoubleTap = false;
                GameManager.Instance.SetModelActive(false, false);
                detailScrollViewBgInCompare.SetActive(true);
                detailScrollRect.horizontal = false;
                detailScrollMask.enabled = true;
                detailImageInCompare.transform.localScale = Vector3.one * minScale;
                detailImageInCompare.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                Sprite viceSprite = GameManager.Instance.GetViceDetailSprite();
                CommonTool.AdjustContent(detailImageInCompare.GetComponent<Image>(), viceSprite);
                break;
            case "DetailScrollViewBgInCompare":
                enableDoubleTap = true;
                GameManager.Instance.SetModelActive(true, true);
                detailScrollViewBgInCompare.SetActive(false);
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
                GameManager.Instance.SwitchMainModel(dpd.value);
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
            case "AngleSliderInCompare":
                GameManager.Instance.SetCameraAngel(sld.value);
                break;
            default:
                MyDebug.LogYellow("Can not find Slider:" + sld.name);
                break;
        }
    }

    public void OnDoubleTap(Gesture gesture)
    {
        if (detailImageInCompare.transform.localScale != Vector3.one * minScale)
        {
            detailImageInCompare.transform.localScale = Vector3.one * minScale;
            detailScrollRect.horizontal = false;
            detailScrollMask.enabled = true;
            detailImageInCompare.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        else
        {
            detailImageInCompare.transform.localScale = Vector3.one * maxScale;
            detailScrollRect.horizontal = true;
            detailScrollMask.enabled = false;
        }
    }

    public void OnPinch(Gesture gesture)
    {
        Vector3 scale = detailImageInCompare.transform.localScale + Vector3.one * gesture.deltaPinch * Time.deltaTime * pinchSensibility;
        if (scale.x > maxScale || scale.y > maxScale || scale.z > maxScale)
        {
            detailImageInCompare.transform.localScale = maxScale * Vector3.one;
            return;
        }
        if (scale.x < minScale || scale.y < minScale || scale.z < minScale)
        {
            detailImageInCompare.transform.localScale = minScale * Vector3.one;
            return;
        }
        detailImageInCompare.transform.localScale = scale;
    }

    public void OnPinchEnd(Gesture gesture)
    {
        bool isMinScale = detailImageInCompare.transform.localScale == Vector3.one * minScale;
        detailScrollRect.horizontal = !isMinScale;
        detailScrollMask.enabled = isMinScale;
    }


}
