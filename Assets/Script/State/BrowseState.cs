using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowseState : State
{
    #region C#单例
    private static BrowseState instance = null;
    private BrowseState()
    {
        base.id = StateID.BrowseState;
        MyDebug.LogWhite("Loading State:" + id.ToString());
    }
    public static BrowseState Instance
    {
        get { return instance ?? (instance = new BrowseState()); }
    }
    #endregion

    public override void Enter(GameObject model = null)
    {
        GameManager.Instance.SetCameraActive(true, false);
        initMainEulerAngles = model.transform.eulerAngles;
        initMainScale = model.transform.localScale;
        SwitchMainModel(model);
    }

    public override void Exit()
    {
        Object.Destroy(curMainModel);
    }

    public override void SwitchMainModel(GameObject model)
    {
        Object.Destroy(curMainModel);
        curMainModel = model;
        curMainModel.AddComponent<ModelGesture>();
        Reset();
    }

    public override void SwitchViceModel(GameObject model)
    {
    }

    public override void Reset()
    {
        curMainModel.transform.position = Vector3.zero;
        curMainModel.transform.eulerAngles = initMainEulerAngles;
        curMainModel.transform.localScale = initMainScale;
        GameManager.Instance.SetMainCameraAngle(0);
    }

    public override void SetModelActive(bool bMainModel, bool bViceModel)
    {
        curMainModel.SetActive(bMainModel);
    }
}
