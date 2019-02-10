using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using cn.sharesdk.unity3d;

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
    private ShareSDK                m_ShareSDK;             //用于分享成就和成绩


    public int MainModelID//主模型的ID
    {
        get;
        private set;
    }

    public int ViceModelID//副模型的ID
    {
        get;
        private set;
    }

    public Transform Player//漫游模式中控制的物体
    {
        get;
        private set;
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

        minCameraAngel = 30;
        maxCameraAngel = 90;

        c_GuiCtrl.InitUI();
        c_StateCtrl.InitState();

        m_ShareSDK = GetComponent<ShareSDK>();
        m_ShareSDK.shareHandler = OnShareResultHandler;
        InitShareIcon();

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
    public void SwitchStateAndModel(StateID sID, int mID = -1)
    {
        if (mID < 0) mID = MainModelID;
        else MainModelID = mID;
        GameObject model = sID == StateID.StartState? null : c_ModelCtrl.GetModelResource(mID);
        if (sID != StateID.StartState && !model)
        {
            MyDebug.LogYellow("Can Not Get Model!!!");
            return;
        }
        if (sID == StateID.RoamState)
        {
            Player = model.transform.Find("Player");
            if (!Player)
            {
                MyDebug.LogYellow("Can Not Find Player!!!");
                return;
            }
            Player.gameObject.SetActive(true);
        }
        if (sID == StateID.CompareState) ViceModelID = 0;
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
        Player = model.transform.Find("Player");
        StateID sID = c_StateCtrl.GetCurStateID();
        if (sID == StateID.RoamState)
        {
            Player = model.transform.Find("Player");
            if (!Player)
            {
                MyDebug.LogYellow("Can Not Find Player!!!");
                return;
            }
            Player.gameObject.SetActive(true);
        }
        c_StateCtrl.SwitchMainModel(model);
        MainModelID = mID;
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
        ViceModelID = mID;
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
    /// 同时改变主副摄像机的角度
    /// </summary>
    /// <param name="angle"></param>
    public void SetCameraAngel(float angle)
    {
        float angleX = (maxCameraAngel - minCameraAngel) * angle + minCameraAngel;
        mainCamera.transform.eulerAngles = new Vector3(angleX, 0, 0);
        viceCamera.transform.eulerAngles = new Vector3(angleX, 0, 0);
    }
    /// <summary>
    /// 设置主摄像机的范围
    /// </summary>
    /// <param name="rect"></param>
    public void SetMainCameraViewport(Rect rect)
    {
        mainCamera.GetComponentInChildren<Camera>().rect = rect;
    }
    /// <summary>
    /// 重置状态
    /// </summary>
    public void ResetState()
    {
        c_StateCtrl.ResetState();
    }
    public StateID GetCurStateID()
    {
        return c_StateCtrl.GetCurStateID();
    }
    /// <summary>
    /// 获取主模型的详情图片
    /// </summary>
    /// <returns></returns>
    public Sprite GetMainDetailSprite()
    {
        string index = c_ModelCtrl.GetModelDetailImage(MainModelID);
        return c_ImageCtrl.GetSpriteResource(index);
    }
    /// <summary>
    /// 获取副模型的详情图片
    /// </summary>
    /// <returns></returns>
    public Sprite GetViceDetailSprite()
    {
        string index = c_ModelCtrl.GetModelDetailImage(ViceModelID);
        return c_ImageCtrl.GetSpriteResource(index);
    }

    public void SetModelActive(bool bMainModel, bool bViceModel = false)
    {
        c_StateCtrl.SetModelActive(bMainModel, bViceModel);
    }

    public void ShareUrl(PlatformType type)
    {
        string title = GetMutiLanguage("Text_40000");
        string description = GetMutiLanguage("Text_40001");
        ShareContent content = new ShareContent();
        content.SetImagePath(Application.persistentDataPath + "/Image/ShareIcon.png");
        if (type == PlatformType.WeChatMoments || type == PlatformType.WeChat)
        {
            content.SetText(description);
            content.SetTitle(title);
            content.SetUrl("https://www.baidu.com");
            content.SetShareType(ContentType.Webpage);
        }
        else if (type == PlatformType.SinaWeibo)
        {
            content.SetText(title + "https://www.baidu.com");//text是Url
        }
        m_ShareSDK.ShareContent(type, content);
    }

    private void InitShareIcon()
    {
        string path = Application.persistentDataPath + "/Image/ShareIcon.png";
        if (!File.Exists(path)) StartCoroutine(AssetHelper.CopyImage("ShareIcon.png"));
    }

    /// <summary>
    /// ShareSDK分享回调
    /// </summary>
    /// <param name="reqID"></param>
    /// <param name="state"></param>
    /// <param name="type"></param>
    /// <param name="result"></param>
    private void OnShareResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            //print("share successfully - share result :");
            //print(MiniJSON.jsonEncode(result));
            //Hide Share Panel!
        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
			print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }

}
