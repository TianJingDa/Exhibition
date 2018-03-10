using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompareState : State
{
    #region C#单例
    private static CompareState instance = null;
    private CompareState()
    {
        base.id = StateID.CompareState;
        MyDebug.LogWhite("Loading State:" + id.ToString());
    }
    public static CompareState Instance
    {
        get { return instance ?? (instance = new CompareState()); }
    }
    #endregion

    public override void Enter(GameObject model = null)
    {
        GameManager.Instance.SetCameraActive(true, true);
        curMainModel = model;
        curMainModel.tag = "MainModel";
        curMainModel.AddComponent<ModelGesture>();
        curViceModel = GameManager.Instance.GetModel(0);
        curViceModel.tag = "ViceModel";
        curViceModel.AddComponent<ModelGesture>();
        initMainEulerAngles = curMainModel.transform.eulerAngles;
        initMainScale = curMainModel.transform.localScale;
        initViceEulerAngles = curViceModel.transform.eulerAngles;
        initViceScale = curViceModel.transform.localScale;
        GameManager.Instance.SetMainCameraViewport(new Rect(0, 0, 0.5f, 1));
        Reset();
    }

    public override void Exit()
    {
        Object.Destroy(curMainModel);
        Object.Destroy(curViceModel);
        GameManager.Instance.SetMainCameraViewport(new Rect(0, 0, 1, 1));
    }

    public override void SwitchMainModel(GameObject model)
    {
        Object.Destroy(curMainModel);
        curMainModel = model;
        curMainModel.tag = "MainModel";
        curMainModel.AddComponent<ModelGesture>();
        ResetMainModel();
    }

    public override void SwitchViceModel(GameObject model)
    {
        Object.Destroy(curViceModel);
        curViceModel = model;
        curViceModel.tag = "ViceModel";
        curViceModel.AddComponent<ModelGesture>();
        ResetViceModel();
    }

    public override void Reset()
    {
        ResetMainModel();
        ResetViceModel();
        GameManager.Instance.SetCameraAngel(0);
    }

    private void ResetMainModel()
    {
        curMainModel.transform.position = Vector3.zero;
        curMainModel.transform.eulerAngles = initMainEulerAngles;
        curMainModel.transform.localScale = initMainScale;
    }

    private void ResetViceModel()
    {
        curViceModel.transform.position = new Vector3(10000, 0, 0);
        curViceModel.transform.eulerAngles = initMainEulerAngles;
        curViceModel.transform.localScale = initMainScale;
    }
}

