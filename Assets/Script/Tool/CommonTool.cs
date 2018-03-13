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
    public static Dictionary<string, GameObject> InitGameObjectDict(GameObject root)
    {
        Dictionary<string, GameObject> gameObjectDict = new Dictionary<string, GameObject>();
        Transform[] gameObjectArray = root.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < gameObjectArray.Length; i++)
        {
            try
            {
                gameObjectDict.Add(gameObjectArray[i].name, gameObjectArray[i].gameObject);
            }
            catch
            {
                MyDebug.LogYellow(gameObjectArray[i].name);
            }
        }
        return gameObjectDict;
    }
    public static void InitText(GameObject root)
    {
        Text[] textArray = root.GetComponentsInChildren<Text>(true);
        if (textArray.Length == 0)
        {
            return;
        }
        Font curFont = GameManager.Instance.GetFont();
        for (int i = 0; i < textArray.Length; i++)
        {
            textArray[i].font = curFont;
            //textArray[i].color = GameManager.Instance.GetColor(textArray[i].index);
            if (string.IsNullOrEmpty(textArray[i].index))
            {
                textArray[i].text = "";
            }
            else
            {
                textArray[i].text = GameManager.Instance.GetMutiLanguage(textArray[i].index);
            }
        }
    }
    public static void InitImage(GameObject root)
    {
        Image[] imageArray = root.GetComponentsInChildren<Image>(true);
        if (imageArray.Length == 0)
        {
            return;
        }
        for (int i = 0; i < imageArray.Length; i++)
        {
            if (string.IsNullOrEmpty(imageArray[i].index)) continue;
            Sprite sprite = GameManager.Instance.GetSprite(imageArray[i].index);
            if (sprite != null)
            {
                float a = imageArray[i].color.a;
                imageArray[i].color = new Color(1, 1, 1, a);
                imageArray[i].sprite = sprite;
            }
            //else
            //{
            //    MyDebug.LogYellow("Can not load Sprite:" + imageArray[i].index);
            //}
        }
    }

    public static void AdjustContent(Image content, Sprite sprite)
    {
        float contentWidth = content.rectTransform.rect.width;
        float imageWidth = sprite.rect.width;
        float imageHeigh = sprite.rect.height;

        float contentHeigh = imageHeigh * contentWidth / imageWidth;
        content.rectTransform.offsetMin = new Vector2(0, -contentHeigh);

        content.sprite = sprite;
    }
}

