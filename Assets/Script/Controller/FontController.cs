﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FontController : Controller
{
    #region C#单例
    private static FontController instance = null;
    private FontController()
    {
        base.id = ControllerID.FontController;
        path = "Font/{0}";
        MyDebug.LogWhite("Loading Controller:" + id.ToString());
    }
    public static FontController Instance
    {
        get { return instance ?? (instance = new FontController()); }
    }
    #endregion
    private string path;

    public Font GetFontResource(LanguageID lID)
    {
        GameObject resouce = Resources.Load<GameObject>(string.Format(path, lID));
        return resouce.GetComponent<GUIText>().font;
    }
}
