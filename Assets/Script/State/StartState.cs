using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : State
{
    #region C#单例
    private static StartState instance = null;
    private StartState()
    {
        base.id = StateID.StartState;
        MyDebug.LogWhite("Loading State:" + id.ToString());
    }
    public static StartState Instance
    {
        get { return instance ?? (instance = new StartState()); }
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
