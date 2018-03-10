using UnityEngine;
/// <summary>
/// 所有游戏状态的基类
/// </summary>
public abstract class State
{
    public StateID id;                                          //状态ID

    protected GameObject curMainModel;                          //当前显示的主模型

    protected GameObject curViceModel;                          //当前显示的副模型

    protected Vector3 initMainEulerAngles;                         //主模型的初始旋转角
    
    protected Vector3 initViceEulerAngles;                         //副模型的初始旋转角

    protected Vector3 initMainScale;                            //主模型的初始大小

    protected Vector3 initViceScale;                            //副模型的初始大小

    public abstract void Enter(GameObject model = null);        //进入该状态时的处理

    public abstract void Exit();                                //退出该状态时的处理

    public abstract void Reset();                               //重置该状态时的处理

    public abstract void SwitchMainModel(GameObject model);     //刷新主屏幕模型

    public abstract void SwitchViceModel(GameObject model);     //刷新副屏幕模型
}
