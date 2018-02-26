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
        MyDebug.LogWhite("Loading Controller:" + id.ToString());
    }
    public static ModelController Instance
    {
        get { return instance ?? (instance = new ModelController()); }
    }
    #endregion

}
