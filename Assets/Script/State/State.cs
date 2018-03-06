/// <summary>
/// 所有游戏状态的基类
/// </summary>
public abstract class State
{
    public StateID id;                  //状态ID

    public abstract void Enter();       //进入该状态时的处理

    public abstract void Exit();        //退出该状态时的处理

    public abstract void Reset();       //重置该状态时的处理
}
