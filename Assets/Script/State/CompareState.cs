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
        curViceModel = GameManager.Instance.GetModel(0);
        Reset();
    }

    public override void Exit()
    {
    }

    public override void SwitchMainModel(GameObject model)
    {
        Object.Destroy(curMainModel);
        curMainModel = model;

        curMainModel.transform.position = Vector3.zero;
        curMainModel.transform.rotation = Quaternion.identity;
        GameManager.Instance.SetMainCameraAngle(0);
    }

    public override void SwitchViceModel(GameObject model)
    {
        Object.Destroy(curViceModel);
        curViceModel = model;

        curViceModel.transform.position = new Vector3(0, -10000, 0);
        curViceModel.transform.rotation = Quaternion.identity;
        GameManager.Instance.SetViceCameraAngle(0);
    }

    public override void Reset()
    {
        curMainModel.transform.position = Vector3.zero;
        curMainModel.transform.rotation = Quaternion.identity;
        GameManager.Instance.SetMainCameraAngle(0);

        curViceModel.transform.position = new Vector3(0, -10000, 0);
        curViceModel.transform.rotation = Quaternion.identity;
        GameManager.Instance.SetViceCameraAngle(0);
    }
}
