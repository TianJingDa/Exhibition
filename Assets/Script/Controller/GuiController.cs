using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiController : Controller
{
    #region C#单例
    private static GuiController instance = null;
    private GuiController()
    {
        base.id = ControllerID.GuiController;
        guiAssetDict = new Dictionary<GuiFrameID, string>();
        resourceDict = new Dictionary<GuiFrameID, Object>();
        InitGuiData();
        MyDebug.LogWhite("Loading Controller:" + id.ToString());
    }
    public static GuiController Instance
    {
        get { return instance ?? (instance = new GuiController()); }
    }
    #endregion

    private Dictionary<GuiFrameID, string> guiAssetDict;//key：GuiFrameID，value：资源路径
    private Dictionary<GuiFrameID, Object> resourceDict;//key：GuiFrameID，value：资源
    private GameObject root;                            //UI对象的根对象
    private GameObject curWrapper;                      //当前激活的GuiWrapper


    /// <summary>
    /// 注册所有资源地址
    /// </summary>
    private void InitGuiData()
    {
        guiAssetDict.Add(GuiFrameID.StartFrameWrapper, "GuiWrapper/StartFrameWrapper");
        guiAssetDict.Add(GuiFrameID.BrowseFrameWrapper, "GuiWrapper/BrowseFrameWrapper");
        guiAssetDict.Add(GuiFrameID.CompareFrameWrapper, "GuiWrapper/CompareFrameWrapper");
        guiAssetDict.Add(GuiFrameID.RoamFrameWrapper, "GuiWrapper/RoamFrameWrapper");
    }

    /// <summary>
    /// 获取资源实例
    /// </summary>
    /// <param name="id">GuiFrameID</param>
    /// <returns>资源实例</returns>
    public Object GetGui(GuiFrameID id)
    {
        Object resouce = null;
        if (resourceDict.ContainsKey(id))
        {
            resouce = resourceDict[id];
        }
        else
        {
            resouce = Resources.Load(guiAssetDict[id]);
            resourceDict.Add(id, resouce);
        }
        return resouce;
    }
    /// <summary>
    /// 初始化UI
    /// </summary>
    public void InitUI()
    {
        root = GameObject.Find("UIRoot");
        curWrapper = Object.Instantiate(GetGui(GuiFrameID.StartFrameWrapper), root.transform) as GameObject;
    }
    /// <summary>
    /// GuiWrapper切换
    /// </summary>
    /// <param name="targetID"></param>
    public void SwitchWrapper(GuiFrameID targetID)
    {
        Object.Destroy(curWrapper);
        Object reource = GetGui(targetID);
        if (reource == null)
        {
            MyDebug.LogYellow("Can not load reousce:" + targetID.ToString());
            return;
        }
        curWrapper = Object.Instantiate(reource, root.transform) as GameObject;
    }
}
