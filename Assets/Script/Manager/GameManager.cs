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

    private GameObject              mainCamera;             //主摄像机
    private GameObject              viceCamera;             //副摄像机


    public int CurMainModelID
    {
        private get;
        set;
    }

    public int CurViceModelID
    {
        private get;
        set;
    }

    public StateID CurStateID
    {
        private get;
        set;
    }



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

        mainCamera = GameObject.Find("Main Camera");
        viceCamera = GameObject.Find("Vice Camera");

        c_GuiCtrl.InitUI();
        c_StateCtrl.InitState();
    }
    /// <summary>
    /// 获取多语言
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string GetMutiLanguage(string index)
    {
        return c_LanguageCtrl.GetMutiLanguage(index);
    }
    /// <summary>
    /// 获取字体
    /// </summary>
    /// <returns></returns>
    public Font GetFont()
    {
        return c_FontCtrl.FontResource;
    }
    /// <summary>
    /// 获取图片
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Sprite GetSprite(string index)
    {
        return c_ImageCtrl.GetSpriteResource(index);
    }
    /// <summary>
    /// 获取模型
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public GameObject GetModel(int id)
    {
        GameObject model = c_ModelCtrl.GetModelResource(id);
        if (!model) MyDebug.LogYellow("Can Not Get Model!!!");
        return model;
    }
    public List<string> GetAllModelNames()
    {
        return c_ModelCtrl.GetAllModelNames();
    }
    /// <summary>
    /// 切换模型和状态
    /// </summary>
    public void SwitchModelAndState()
    {
        GameObject model = c_ModelCtrl.GetModelResource(CurMainModelID);
        if (!model)
        {
            MyDebug.LogYellow("Can Not Get Model!!!");
            return;
        }
        c_GuiCtrl.SwitchWrapper((GuiFrameID)CurStateID);
        c_StateCtrl.SwitchState(CurStateID, model);
    }
    /// <summary>
    /// 切换主模型
    /// </summary>
    public void SwitchMainModel()
    {
        GameObject model = c_ModelCtrl.GetModelResource(CurMainModelID);
        if (!model)
        {
            MyDebug.LogYellow("Can Not Get Main Model!!!");
            return;
        }
        c_StateCtrl.SwitchMainModel(model);
    }
    /// <summary>
    /// 切换副模型
    /// </summary>
    public void SwitchViceModel()
    {
        GameObject model = c_ModelCtrl.GetModelResource(CurViceModelID);
        if (!model)
        {
            MyDebug.LogYellow("Can Not Get Vice Model!!!");
            return;
        }
        c_StateCtrl.SwitchViceModel(model);
    }
    /// <summary>
    /// 设置摄像机的状态
    /// </summary>
    /// <param name="mainCameraActive"></param>
    /// <param name="viceCameraActive"></param>
    public void SetCameraActive(bool mainCameraActive,bool viceCameraActive)
    {
        mainCamera.SetActive(mainCameraActive);
        viceCamera.SetActive(viceCameraActive);
    }
    /// <summary>
    /// 设置主摄像机角度
    /// </summary>
    /// <param name="angle"></param>
    public void SetMainCameraAngle(float angle)
    {

    }
    /// <summary>
    /// 设置副摄像机角度
    /// </summary>
    /// <param name="angle"></param>
    public void SetViceCameraAngle(float angle)
    {

    }
    /// <summary>
    /// 重置状态
    /// </summary>
    public void ResetState()
    {
        c_StateCtrl.ResetState();
    }
}
