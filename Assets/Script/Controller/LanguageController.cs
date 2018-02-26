using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageController : Controller
{
    #region C#单例
    private static LanguageController instance = null;
    private LanguageController()
    {
        base.id = ControllerID.LanguageController;
        MyDebug.LogWhite("Loading Controller:" + id.ToString());
    }
    public static LanguageController Instance
    {
        get { return instance ?? (instance = new LanguageController()); }
    }
    #endregion
}
