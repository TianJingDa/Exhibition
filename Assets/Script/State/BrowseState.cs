using System;
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
