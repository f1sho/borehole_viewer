using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateAssetBundle
{
    [MenuItem("Tools/CreateAssetBundle for Android")]
    private static void buildAllAssetBundles()
    {
        string path = "Assets/StreamingAssets";
        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.Android);
        Debug.Log("Android finish");
    }
}
