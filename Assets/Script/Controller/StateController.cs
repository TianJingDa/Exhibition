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
    private State curState;

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
        curState = stateDict[StateID.StartState];
    }

    public void SwitchState(StateID targetID,GameObject model)
    {
        if (curState != null)
        {
            curState.Exit();
            stateDict[targetID].Enter(model);
            curState = stateDict[targetID];
        }
        else
        {
            MyDebug.LogYellow("Last State is NULL!!");
        }
    }

    public void SwitchMainModel(GameObject model)
    {
        curState.SwitchMainModel(model);
    }

    public void SwitchViceModel(GameObject model)
    {
        curState.SwitchViceModel(model);
    }

    public void ResetState()
    {
        curState.Reset();
    }

    public StateID GetCurStateID()
    {
        return curState.id;
    }

    public void SetModelActive(bool bMainModel, bool bViceModel)
    {
        curState.SetModelActive(bMainModel, bViceModel);
    }
}
