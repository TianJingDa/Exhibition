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

    private int                     minCameraAngel;         //摄像机最小俯角
    private int                     maxCameraAngel;         //摄像机最大俯角
    private GameObject              mainCamera;             //主摄像机
    private GameObject              viceCamera;             //副摄像机


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

        minCameraAngel = 30;
        maxCameraAngel = 90;

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
    /// <param name="sID">状态ID</param>
    /// <param name="mID">模型ID</param>
    public void SwitchStateAndModel(StateID sID, int mID)
    {
        GameObject model = sID == StateID.StartState? null : c_ModelCtrl.GetModelResource(mID);
        if (sID != StateID.StartState && !model)
        {
            MyDebug.LogYellow("Can Not Get Model!!!");
            return;
        }
        c_GuiCtrl.SwitchWrapper((GuiFrameID)sID);
        c_StateCtrl.SwitchState(sID, model);
    }
    /// <summary>
    /// 切换主模型
    /// </summary>
    public void SwitchMainModel(int mID)
    {
        GameObject model = c_ModelCtrl.GetModelResource(mID);
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
    public void SwitchViceModel(int mID)
    {
        GameObject model = c_ModelCtrl.GetModelResource(mID);
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
        float angleX = (maxCameraAngel - minCameraAngel) * angle + minCameraAngel;
        mainCamera.transform.eulerAngles = new Vector3(angleX, 0, 0);
    }
    /// <summary>
    /// 设置副摄像机角度
    /// </summary>
    /// <param name="angle"></param>
    public void SetViceCameraAngle(float angle)
    {
        float angleX = (maxCameraAngel - minCameraAngel) * angle + minCameraAngel;
        viceCamera.transform.eulerAngles = new Vector3(angleX, 0, 0);
    }
    /// <summary>
    /// 重置状态
    /// </summary>
    public void ResetState()
    {
        c_StateCtrl.ResetState();
    }
}
