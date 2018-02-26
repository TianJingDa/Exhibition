using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public static class CommonTool 
{

    public static T GetComponentByName<T>(GameObject root, string name) where T : Component
    {
        T[] array = root.GetComponentsInChildren<T>(true);
        T result = null;
        if (array != null)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].name == name)
                {
                    result = array[i];
                    break;
                }
            }
        }
        if (result == null)
        {
            MyDebug.LogYellow("Can not find :" + name);
        }
        return result;
    }
    public static T GetComponentContainsName<T>(GameObject root, string name) where T : Component
    {
        T[] array = root.GetComponentsInChildren<T>(true);
        T result = null;
        if (array != null)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].name.Contains(name))
                {
                    result = array[i];
                    break;
                }
            }
        }
        if (result == null)
        {
            MyDebug.LogYellow("Can not find :" + name);
        }
        return result;
    }
    public static List<T> GetComponentsContainName<T>(GameObject root, string name) where T : Component
    {
        List<T> result = new List<T>();
        T[] array = root.GetComponentsInChildren<T>(true);
        if (array != null)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].name.Contains(name))
                {
                    result.Add(array[i]);
                }
            }
        }
        if (result.Count == 0)
        {
            MyDebug.LogYellow("Can not find :" + name);
        }
        return result;
    }
    public static GameObject GetGameObjectByName(GameObject root, string name)
    {
        GameObject result = null;
        if (root != null)
        {
            Transform[] objArray = root.GetComponentsInChildren<Transform>(true);
            if (objArray != null)
            {
                for (int i = 0; i < objArray.Length; i++)
                {
                    if (objArray[i].name == name)
                    {
                        result = objArray[i].gameObject;
                        break;
                    }
                }
            }
        }
        if (result == null)
        {
            MyDebug.LogYellow("Can not find :" + name);
        }
        return result;
    }
    public static GameObject GetGameObjectContainsName(GameObject root, string name)
    {
        GameObject result = null;
        if (root != null)
        {
            Transform[] objArray = root.GetComponentsInChildren<Transform>(true);
            if (objArray != null)
            {
                for (int i = 0; i < objArray.Length; i++)
                {
                    if (objArray[i].name.Contains(name))
                    {
                        result = objArray[i].gameObject;
                        break;
                    }
                }
            }
        }
        if (result == null)
        {
            MyDebug.LogYellow("Can not find :" + name);
        }
        return result;
    }
    public static List<GameObject> GetGameObjectsContainName(GameObject root, string name)
    {
        List<GameObject> result = new List<GameObject>();
        Transform[] objArray = root.GetComponentsInChildren<Transform>(true);
        if (objArray != null)
        {
            for (int i = 0; i < objArray.Length; i++)
            {
                if (objArray[i].name.Contains(name))  
                {
                    result.Add(objArray[i].gameObject);
                }
            }
        }
        if (result.Count == 0)
        {
            MyDebug.LogYellow("Can not find :" + name);
        }
        return result;
    }
    public static GameObject GetParentByName(GameObject child,string name)
    {
        GameObject result = null;

        if(child.transform.parent.gameObject.name == name)
        {
            result = child.transform.parent.gameObject;
        }
        else if(child.transform.parent != null)
        {
            result = GetParentByName(child.transform.parent.gameObject, name);
        }

        if (result == null)
        {
            MyDebug.LogYellow("Can not find :" + name);
        }

        return result;


    }
}
