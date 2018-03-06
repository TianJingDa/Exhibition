using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : Controller
{
    #region C#单例
    private static StateController instance = null;
    private StateController()
    {
        base.id = ControllerID.StateController;
        InitStateData();
        MyDebug.LogWhite("Loading Controller:" + id.ToString());
    }
    public static StateController Instance
    {
        get { return instance ?? (instance = new StateController()); }
    }
    #endregion

    private Dictionary<StateID, State> stateDict;
    private State lastState;

    private void InitStateData()
    {
        stateDict = new Dictionary<StateID, State>()
        {
            {StateID.StartState,StartState.Instance },
            {StateID.BrowseState,BrowseState.Instance },
            {StateID.CompareState,CompareState.Instance },
            {StateID.RoamState,RoamState.Instance }
        };
    }

    public void InitState()
    {
        stateDict[StateID.StartState].Enter();
        lastState = stateDict[StateID.StartState];
    }

    public void SwitchState(StateID targetID)
    {
        if (lastState != null)
        {
            lastState.Exit();
            stateDict[targetID].Enter();
            lastState = stateDict[targetID];
        }
        else
        {
            MyDebug.LogYellow("Last State is NULL!!");
        }
    }

}
