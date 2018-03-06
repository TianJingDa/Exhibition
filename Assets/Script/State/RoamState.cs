using System;
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


    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Reset()
    {
    }
}
