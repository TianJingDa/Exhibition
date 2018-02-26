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

    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
    }

}
