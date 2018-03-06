using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompareFrameWrapper : GuiFrameWrapper
{
    void Start()
    {
        id = GuiFrameID.CompareFrameWrapper;
        Init();
    }

    protected override void OnStart(Dictionary<string, GameObject> gameObjectDict)
    {

    }
}
