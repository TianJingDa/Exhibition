using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowseFrameWrapper : GuiFrameWrapper
{

    void Start()
    {
        id = GuiFrameID.BrowseFrameWrapper;
        Init();
    }

    protected override void OnStart(Dictionary<string, GameObject> gameObjectDict)
    {

    }

}
