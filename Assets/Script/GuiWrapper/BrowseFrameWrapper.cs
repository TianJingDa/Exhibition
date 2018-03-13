using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HedgehogTeam.EasyTouch;


public class BrowseFrameWrapper : GuiFrameWrapper
{
    //private int tempModelID;//所选的户型

    private GameObject modelSwitchContentInBrowse;
    private GameObject detailScrollViewBgInBrowse;
    private GameObject detailScrollViewInBrowse;
    private GameObject detailImageInBrowse;
    private ScrollRect detailScrollRect;
    private Mask detailScrollMask;
    private Dropdown modelDropdownInBrowse;
    private Slider angleSliderInBrowse;

    void Start()
    {
        id = GuiFrameID.BrowseFrameWrapper;
        Init();
        detailScrollRect = detailScrollViewInBrowse.GetComponent<ScrollRect>();
        detailScrollMask = detailScrollViewInBrowse.GetComponent<Mask>();
        List<string> dropDownOptions = GameManager.Instance.GetAllModelNames();
        RefreshDropdown(modelDropdownInBrowse, dropDownOptions);
    }

    protected override void OnStart(Dictionary<string, GameObject> gameObjectDict)
    {
        modelSwitchContentInBrowse      = gameObjectDict["ModelSwitchContentInBrowse"];
        detailScrollViewBgInBrowse      = gameObjectDict["DetailScrollViewBgInBrowse"];
        detailScrollViewInBrowse        = gameObjectDict["DetailScrollViewInBrowse"];
        detailImageInBrowse             = gameObjectDict["DetailImageInBrowse"];
        modelDropdownInBrowse           = gameObjectDict["ModelDropdownInBrowse"].GetComponent<Dropdown>();
        angleSliderInBrowse             = gameObjectDict["AngleSliderInBrowse"].GetComponent<Slider>();
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
            case "Browse2StartBtn":
                GameManager.Instance.SwitchStateAndModel(StateID.StartState);
                break;
            case "HideModelSwitchBtn":
                modelSwitchContentInBrowse.SetActive(false);
                break;
            case "ModelSwitchBtnInBrowse":
                modelSwitchContentInBrowse.SetActive(!modelSwitchContentInBrowse.activeSelf);
                break;
            case "CompairStateBtnInBrowse":
                GameManager.Instance.SwitchStateAndModel(StateID.CompareState);
                break;
            case "RoamStateBtnInBrowse":
                GameManager.Instance.SwitchStateAndModel(StateID.RoamState);
                break;
            case "ResetBtnInBrowse":
                angleSliderInBrowse.value = 0;
                GameManager.Instance.ResetState();
                break;
            case "DetailBtnInBrowse":
                enableDoubleTap = false;
                GameManager.Instance.SetModelActive(false);
                detailScrollViewBgInBrowse.SetActive(true);
                detailScrollRect.horizontal = false;
                detailScrollMask.enabled = true;
                detailImageInBrowse.transform.localScale = Vector3.one * minScale;
                detailImageInBrowse.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                Sprite sprite = GameManager.Instance.GetMainDetailSprite();
                CommonTool.AdjustContent(detailImageInBrowse.GetComponent<Image>(), sprite);
                break;
            case "DetailScrollViewBgInBrowse":
                enableDoubleTap = true;
                GameManager.Instance.SetModelActive(true);
                detailScrollViewBgInBrowse.SetActive(false);
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
                GameManager.Instance.SwitchMainModel(dpd.value);
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

    public void OnDoubleTap(Gesture gesture)
    {
        if (detailImageInBrowse.transform.localScale != Vector3.one * minScale)
        {
            detailImageInBrowse.transform.localScale = Vector3.one * minScale;
            detailScrollRect.horizontal = false;
            detailScrollMask.enabled = true;
            detailImageInBrowse.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        else
        {
            detailImageInBrowse.transform.localScale = Vector3.one * maxScale;
            detailScrollRect.horizontal = true;
            detailScrollMask.enabled = false;
        }
    }

    public void OnPinch(Gesture gesture)
    {
        Vector3 scale = detailImageInBrowse.transform.localScale + Vector3.one * gesture.deltaPinch * Time.deltaTime * pinchSensibility;
        if (scale.x > maxScale || scale.y > maxScale || scale.z > maxScale)
        {
            detailImageInBrowse.transform.localScale = maxScale * Vector3.one;
            return;
        }
        if (scale.x < minScale || scale.y < minScale || scale.z < minScale)
        {
            detailImageInBrowse.transform.localScale = minScale * Vector3.one;
            return;
        }
        detailImageInBrowse.transform.localScale = scale;
    }

    public void OnPinchEnd(Gesture gesture)
    {
        bool isMinScale = detailImageInBrowse.transform.localScale == Vector3.one * minScale;
        detailScrollRect.horizontal = !isMinScale;
        detailScrollMask.enabled = isMinScale;
    }
}
