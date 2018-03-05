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
        InitLanguageData();
        MyDebug.LogWhite("Loading Controller:" + id.ToString());
    }
    public static LanguageController Instance
    {
        get { return instance ?? (instance = new LanguageController()); }
    }
    #endregion
    private Dictionary<string, string[]> mutiLanguageDict;  //存储多语言的字典，key：序号，value：文字

    /// <summary>
    /// 初始化多语言字典
    /// </summary>
    private void InitLanguageData()
    {
        string path = "Language/MutiLanguage";
        mutiLanguageDict = (Dictionary<string, string[]>)IOHelper.GetDataFromResources(path, typeof(Dictionary<string, string[]>));
    }
    /// <summary>
    /// 获取多语言
    /// </summary>
    /// <param name="index">序号</param>
    /// <returns>内容</returns>
    public string GetMutiLanguage(string index, LanguageID language)
    {
        string[] languageArray = null;
        mutiLanguageDict.TryGetValue(index, out languageArray);
        if (languageArray == null)
        {
            return index;
        }
        else
        {
            string text = languageArray[(int)language];
            if (text.Contains("\\n")) text = text.Replace("\\n", "\n");
            if (text.Contains("\\u3000")) text = text.Replace("\\u3000", "\u3000");
            return text;
        }
    }

}
