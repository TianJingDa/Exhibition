using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : Controller
{
    #region C#单例
    private static ModelController instance = null;
    private ModelController()
    {
        base.id = ControllerID.ModelController;
        InitModelData();
        MyDebug.LogWhite("Loading Controller:" + id.ToString());
    }
    public static ModelController Instance
    {
        get { return instance ?? (instance = new ModelController()); }
    }
    #endregion
    /// <summary>
    /// 模型字典，key是模型枚举，value是模型地址
    /// </summary>
    private Dictionary<ModelID, string> modelDict;

    /// <summary>
    /// 初始化模型字典
    /// </summary>
    private void InitModelData()
    {
        modelDict = new Dictionary<ModelID, string>()
        {
            { ModelID.Test, "Model/Test" }
        };
    }

    public GameObject GetModelResource(ModelID id)
    {
        GameObject resouce = Resources.Load<GameObject>(modelDict[id]);
        return resouce;
    }

}
