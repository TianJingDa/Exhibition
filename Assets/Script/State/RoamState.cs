﻿using System.Collections;
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

    private Vector3 playerPostiion;     //起始位置
    private Vector3 playerAngle;        //起始角度

    public override void Enter(GameObject model = null)
    {
        GameManager.Instance.SetCameraActive(false, false);
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
        playerPostiion = GameManager.Instance.Player.position;
        playerAngle = GameManager.Instance.Player.eulerAngles;
        Reset();
    }

    public override void SwitchViceModel(GameObject model)
    {
    }

    public override void Reset()
    {
        GameManager.Instance.Player.position = playerPostiion;
        GameManager.Instance.Player.eulerAngles = playerAngle;
    }

    public override void SetModelActive(bool bMainModel, bool bViceModel)
    {
        curMainModel.SetActive(bMainModel);
    }
}
