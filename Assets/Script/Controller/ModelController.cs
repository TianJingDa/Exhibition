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
    private List<ModelInstance> modelList;

    /// <summary>
    /// 初始化模型字典
    /// </summary>
    private void InitModelData()
    {
        string path = "Model/Model";
        modelList = (List<ModelInstance>)IOHelper.GetDataFromResources(path, typeof(List<ModelInstance>));
    }

    public GameObject GetModelResource(int id)
    {
        GameObject resouce = Resources.Load<GameObject>("Model/" + id.ToString());
        return Object.Instantiate(resouce);
    }

    public List<string> GetAllModelNames()
    {
        List<string> nameList = new List<string>();
        for(int i = 0; i < modelList.Count; i++)
        {
            nameList.Add(modelList[i].name);
        }
        return nameList;
    }

    public string GetModelDetailImage(int id)
    {
        ModelInstance instance = modelList.Find(x => x.index == id.ToString());
        return instance.image;
    }
}
[System.Serializable]
public class ModelInstance
{
    public string index;
    public string name;
    public string image;
}
