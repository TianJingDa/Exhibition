using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageController : Controller
{
    #region C#单例
    private static ImageController instance = null;
    private ImageController()
    {
        base.id = ControllerID.ImageController;
        MyDebug.LogWhite("Loading Controller:" + id.ToString());
    }
    public static ImageController Instance
    {
        get { return instance ?? (instance = new ImageController()); }
    }
    #endregion

    public Sprite GetSpriteResource(string index)
    {
        GameObject resouce = Resources.Load<GameObject>("Image/" + index);
        if (resouce) return resouce.GetComponent<SpriteRenderer>().sprite;
        else return null;
    }

}
