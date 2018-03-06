using System;
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
