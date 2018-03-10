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

        curMainModel.transform.position = Vector3.zero;
        curMainModel.transform.rotation = Quaternion.identity;
        //GameManager.Instance.SetMainCameraAngle(0);
    }

    public override void SwitchViceModel(GameObject model)
    {
        Object.Destroy(curViceModel);
        curViceModel = model;

        curViceModel.transform.position = new Vector3(10000, 0, 0);
        curViceModel.transform.rotation = Quaternion.identity;
        //GameManager.Instance.SetViceCameraAngle(0);
    }

    public override void Reset()
    {
        curMainModel.transform.position = Vector3.zero;
        curMainModel.transform.rotation = Quaternion.identity;

        curViceModel.transform.position = new Vector3(10000, 0, 0);
        curViceModel.transform.rotation = Quaternion.identity;

        GameManager.Instance.SetCameraAngel(0);
    }
}
