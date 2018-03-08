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
        curMainModel = model;
        Reset();
    }

    public override void Exit()
    {
    }

    public override void SwitchMainModel(GameObject model)
    {
        Object.Destroy(curMainModel);
        curMainModel = model;
        Reset();
    }

    public override void SwitchViceModel(GameObject model)
    {
    }

    public override void Reset()
    {
        curMainModel.transform.position = Vector3.zero;
        curMainModel.transform.rotation = Quaternion.identity;
        GameManager.Instance.SetMainCameraAngle(0);
    }
}
