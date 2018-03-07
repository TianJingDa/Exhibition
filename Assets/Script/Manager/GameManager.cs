using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// 游戏控制层
/// </summary>
public class GameManager : MonoBehaviour
{
    private LanguageController      c_LanguageCtrl;         //多语言控制器
    private FontController          c_FontCtrl;             //字体控制器
    private ImageController         c_ImageCtrl;            //图片控制器
    private ModelController         c_ModelCtrl;            //模型控制器
    private GuiController           c_GuiCtrl;              //UI控制器
    private StateController         c_StateCtrl;            //状态控制器


    public static GameManager Instance//单例
    {
        get;
        private set;
    }
    void Awake()
    {
        if (Instance == null){ Instance = this; }
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 30;

        c_LanguageCtrl  = LanguageController.Instance;
        c_FontCtrl      = FontController.Instance;
        c_GuiCtrl       = GuiController.Instance;
        c_ImageCtrl     = ImageController.Instance;
        c_ModelCtrl     = ModelController.Instance;
        c_StateCtrl     = StateController.Instance;
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);

        c_StateCtrl.InitState();
        c_GuiCtrl.InitUI();
    }

    public string GetMutiLanguage(string index)
    {
        return c_LanguageCtrl.GetMutiLanguage(index);
    }
    public Font GetFont()
    {
        return c_FontCtrl.FontResource;
    }
    public Sprite GetSprite(string index)
    {
        return c_ImageCtrl.GetSpriteResource(index);
    }

}
