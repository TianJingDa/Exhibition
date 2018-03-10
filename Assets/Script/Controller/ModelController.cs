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
    private Dictionary<string, string> modelDict;

    /// <summary>
    /// 初始化模型字典
    /// </summary>
    private void InitModelData()
    {
        string path = "Model/Model";
        modelDict = (Dictionary<string, string>)IOHelper.GetDataFromResources(path, typeof(Dictionary<string, string>));
    }

    public GameObject GetModelResource(int id)
    {
        GameObject resouce = Resources.Load<GameObject>("Model/" + id.ToString());
        return Object.Instantiate(resouce);
    }

    public List<string> GetAllModelNames()
    {
        return new List<string>(modelDict.Values);
    }

}
