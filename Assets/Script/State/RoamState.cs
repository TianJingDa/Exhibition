using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamState : State
{
    #region C#单例
    private static RoamState instance = null;
    private RoamState()
    {
        base.id = StateID.RoamState;
        MyDebug.LogWhite("Loading State:" + id.ToString());
    }
    public static RoamState Instance
    {
        get { return instance ?? (instance = new RoamState()); }
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
