using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFrameWrapper : GuiFrameWrapper
{

    void Start()
    {
        id = GuiFrameID.StartFrameWrapper;
        Init();
    }

    protected override void OnStart(Dictionary<string, GameObject> gameObjectDict)
    {

    }

}
