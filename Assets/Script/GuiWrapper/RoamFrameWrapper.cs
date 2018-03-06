using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamFrameWrapper : GuiFrameWrapper
{

    void Start()
    {
        id = GuiFrameID.RoamFrameWrapper;
        Init();
    }

    protected override void OnStart(Dictionary<string, GameObject> gameObjectDict)
    {

    }
}
