using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FontController : Controller
{
    #region C#单例
    private static FontController instance = null;
    private FontController()
    {
        base.id = ControllerID.FontController;
        MyDebug.LogWhite("Loading Controller:" + id.ToString());
    }
    public static FontController Instance
    {
        get { return instance ?? (instance = new FontController()); }
    }
    #endregion

    private Font font;

    public Font FontResource
    {
        get { return font ?? (font = GetFontResource()); }
    }

    private Font GetFontResource()
    {
        GameObject resouce = Resources.Load<GameObject>("Font/Font");
        return resouce.GetComponent<GUIText>().font;
    }
}
