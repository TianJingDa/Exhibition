using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;

public class UITool : Editor 
{
    /// <summary>
    /// 使用时需现将物体放在其父物体中心，锚点在0.5，0.5处
    /// </summary>
    [MenuItem("Custom Editor/将锚点设为本身大小")]
    public static void AnchorFitter()
    {
        for(int i=0;i< Selection.gameObjects.Length; i++)
        {
            RectTransform target = Selection.gameObjects[i].transform as RectTransform;
            RectTransform targetParent = Selection.gameObjects[i].transform.parent as RectTransform;
            if (target != null && targetParent != null)
            {
                //Debug.Log("target.rect.min:" + target.rect.min.ToString());
                //Debug.Log("target.rect.max:" + target.rect.max.ToString());
                //Debug.Log("target.offsetMax:" + target.offsetMax.ToString());
                //Debug.Log("target.offsetMin:" + target.offsetMin.ToString());
                //Debug.Log("target.anchorMax:" + target.anchorMax.ToString());
                //Debug.Log("target.anchorMin:" + target.anchorMin.ToString());
                //Debug.Log("target.rect.x:" + target.rect.x.ToString());
                //Debug.Log("target.rect.y:" + target.rect.y.ToString());
                //Debug.Log("target.rect.xMin:" + target.rect.xMin.ToString());
                //Debug.Log("target.rect.xMax:" + target.rect.xMax.ToString());
                //Debug.Log("target.rect.yMin:" + target.rect.yMin.ToString());
                //Debug.Log("target.rect.yMax:" + target.rect.yMax.ToString());
                //Debug.Log("target.rect.position:" + target.rect.position.ToString());
                //Debug.Log("target.position:" + target.position.ToString());
                //Debug.Log("target.localPosition:" + target.localPosition.ToString());
                //Debug.Log("target.anchoredPosition:" + target.anchoredPosition.ToString());
                //return;
                float deltaX = target.localPosition.x;
                float deltaY = target.localPosition.y;
                float minX = 0.5f * (1 - target.rect.width / targetParent.rect.width) + deltaX / targetParent.rect.width;
                float minY = 0.5f * (1 - target.rect.height / targetParent.rect.height) + deltaY / targetParent.rect.height;
                float maxX = 0.5f * (1 + target.rect.width / targetParent.rect.width) + deltaX / targetParent.rect.width;
                float maxY = 0.5f * (1 + target.rect.height / targetParent.rect.height) + deltaY / targetParent.rect.height;
                target.anchorMin = new Vector2(minX, minY);
                target.anchorMax = new Vector2(maxX, maxY);
                target.offsetMin = Vector2.zero;
                target.offsetMax = Vector2.zero;
            }

        }
    }
    [MenuItem("Custom Editor/将锚点设为本身大小（水平）")]
    public static void AnchorFitter_H()
    {
        for(int i=0;i< Selection.gameObjects.Length; i++)
        {
            RectTransform target = Selection.gameObjects[i].transform as RectTransform;
            RectTransform targetParent = Selection.gameObjects[i].transform.parent as RectTransform;
            if (target != null && targetParent != null)
            {
                float width = target.rect.width;
                float height = target.rect.height;
                float deltaX = target.localPosition.x;
                float deltaY = target.localPosition.y;
                float minX = 0.5f * (1 - target.rect.width / targetParent.rect.width) + deltaX / targetParent.rect.width;
                float minY = 0.5f * (1 - target.rect.height / targetParent.rect.height) + deltaY / targetParent.rect.height;
                float maxX = 0.5f * (1 + target.rect.width / targetParent.rect.width) + deltaX / targetParent.rect.width;
                float maxY = 0.5f * (1 + target.rect.height / targetParent.rect.height) + deltaY / targetParent.rect.height;
                target.anchorMin = new Vector2(minX, (maxY + minY) / 2);
                target.anchorMax = new Vector2(maxX, (maxY + minY) / 2);
                target.offsetMin = new Vector2(0, -(height / 2));
                target.offsetMax = new Vector2(0, (height / 2));
            }
        }
    }
    [MenuItem("Custom Editor/将锚点设为本身大小（垂直）")]
    public static void AnchorFitter_V()
    {
        for(int i=0;i< Selection.gameObjects.Length; i++)
        {
            RectTransform target = Selection.gameObjects[i].transform as RectTransform;
            RectTransform targetParent = Selection.gameObjects[i].transform.parent as RectTransform;
            if (target != null && targetParent != null)
            {
                float width = target.rect.width;
                float height = target.rect.height;
                float deltaX = target.localPosition.x;
                float deltaY = target.localPosition.y;
                float minX = 0.5f * (1 - target.rect.width / targetParent.rect.width) + deltaX / targetParent.rect.width;
                float minY = 0.5f * (1 - target.rect.height / targetParent.rect.height) + deltaY / targetParent.rect.height;
                float maxX = 0.5f * (1 + target.rect.width / targetParent.rect.width) + deltaX / targetParent.rect.width;
                float maxY = 0.5f * (1 + target.rect.height / targetParent.rect.height) + deltaY / targetParent.rect.height;
                target.anchorMin = new Vector2((minX + maxX) / 2, minY);
                target.anchorMax = new Vector2((minX + maxX) / 2, maxY);
                target.offsetMin = new Vector2(0, -(width / 2));
                target.offsetMax = new Vector2(0, (width / 2));
            }
        }
    }
    [MenuItem("Custom Editor/将锚点设为本身中心")]
    public static void AnchorCenter()
    {
        for(int i=0;i< Selection.gameObjects.Length; i++)
        {
            RectTransform target = Selection.gameObjects[0].transform as RectTransform;
            RectTransform targetParent = Selection.gameObjects[0].transform.parent as RectTransform;
            if (target != null && targetParent != null)
            {
                float width = target.rect.width;
                float height = target.rect.height;
                float X = 0.5f + target.localPosition.x / targetParent.rect.width;
                float Y = 0.5f + target.localPosition.y / targetParent.rect.height;
                target.anchorMin = new Vector2(X, Y);
                target.anchorMax = new Vector2(X, Y);
                target.anchoredPosition = Vector2.zero;
            }
        }
    }
    [MenuItem("Custom Editor/生成多语言")]
    public static void MakeMutiLanguageJson()
    {
        StreamReader mutiLanguageAsset = new StreamReader(Application.dataPath + "/MutiLanguage.txt");
        if (mutiLanguageAsset == null)
        {
            MyDebug.LogYellow("Can not find MutiLanguage.txt !!");
            return;
        }
        Dictionary<string, string[]> mutiLanguageDict = new Dictionary<string, string[]>();
        char[] charSeparators = new char[] { "\r"[0], "\n"[0] };
        string asset = mutiLanguageAsset.ReadToEnd();
        string[] lineArray = asset.Split(charSeparators, System.StringSplitOptions.RemoveEmptyEntries);
        List<string> lineList;
        for (int i = 0; i < lineArray.Length; i++)
        {
            lineList = new List<string>(lineArray[i].Split(','));
            mutiLanguageDict.Add(lineList[0], lineList.GetRange(1, lineList.Count - 1).ToArray());
        }
        string path = Application.dataPath + "/Resources/Language/MutiLanguage.txt";
        if (File.Exists(path)) File.Delete(path);
        IOHelper.SetData(path, mutiLanguageDict);
    }
    [MenuItem("Custom Editor/转换prefab/Default")]
    public static void DefaultPrefab()
    {
        string spriteDir = Application.dataPath + "/Resources/Skin/Default";

        if (!Directory.Exists(spriteDir))
        {
            Directory.CreateDirectory(spriteDir);
        }

        DirectoryInfo rootDirInfo = new DirectoryInfo(Application.dataPath + "/Image/Default");
        foreach (DirectoryInfo dirInfo in rootDirInfo.GetDirectories())
        {
            foreach (FileInfo pngFile in dirInfo.GetFiles("*.png", SearchOption.AllDirectories))
            {
                string allPath = pngFile.FullName;
                string assetPath = allPath.Substring(allPath.IndexOf("Assets"));
                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
                GameObject go = new GameObject(sprite.name);
                go.AddComponent<SpriteRenderer>().sprite = sprite;
                allPath = spriteDir + "/" + sprite.name + ".prefab";
                if (File.Exists(allPath)) File.Delete(allPath);
                string prefabPath = allPath.Substring(allPath.IndexOf("Assets"));
                PrefabUtility.CreatePrefab(prefabPath, go);
                DestroyImmediate(go);
            }
        }
    }

    [MenuItem("Custom Editor/查找无用UISprite")]
    public static void FindUISprite()
    {
        Transform[] objArray = Selection.gameObjects[0].GetComponentsInChildren<Transform>(true);
        for(int i = 0; i < objArray.Length; i++)
        {
            Image image = objArray[i].GetComponent<Image>();
            if (image && image.sprite && image.sprite.name == "UISprite" && image.color == Color.white)
            {
                string path = GetPath(objArray[i]);
                MyDebug.LogYellow(path); 
            } 

        }
    }

    private static string GetPath(Transform tra)
    {
        string path = tra.name;
        Transform parent = tra.parent;
        while (parent)
        {
            path = parent.name + "/" + path;
            parent = parent.parent;
        }
        return path;
    }


    [MenuItem("Assets/转换prefab-Default")]
    public static void AssetsDefaultPrefab()
    {
        string spriteDir = Application.dataPath + "/Resources/Skin/Default";

        if (!Directory.Exists(spriteDir))
        {
            Directory.CreateDirectory(spriteDir);
        }

        for(int i = 0; i < Selection.objects.Length; i++)
        {
            string path = AssetDatabase.GetAssetPath(Selection.objects[i]);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
            if (!sprite)
            {
                MyDebug.LogYellow("Can not get SPRITE!");
                return;
            }
            GameObject go = new GameObject(sprite.name);
            go.AddComponent<SpriteRenderer>().sprite = sprite;
            string allPath = spriteDir + "/" + sprite.name + ".prefab";
            if (File.Exists(allPath)) File.Delete(allPath);
            string prefabPath = allPath.Substring(allPath.IndexOf("Assets"));
            PrefabUtility.CreatePrefab(prefabPath, go);
            DestroyImmediate(go);
        }
    }
}
