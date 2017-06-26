using UnityEngine;
using System.Collections;
using UnityEditor;
public class MyBundleBuilder : MonoBehaviour {
    [MenuItem("myBuildAB/BuildForWin")]
    static void BuildForWin()
    {
        string outputPath = Application.streamingAssetsPath;
        BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

    }
    [MenuItem("myBuildAB/BuildForiOS")]
    static void BuildForiOS()
    {
        string outputPath = Application.streamingAssetsPath;
        BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.None, BuildTarget.iOS);
    }
    [MenuItem("myBuildAB/BuildForAndr")]
    static void BuildForAndr()
    {
        string outputPath = Application.streamingAssetsPath;
        BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.None, BuildTarget.Android);
    }
}
