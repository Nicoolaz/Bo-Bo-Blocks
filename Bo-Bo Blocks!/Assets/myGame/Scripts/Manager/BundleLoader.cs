using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class BundleLoader : MonoBehaviour
{
    string win_streamingAssetPath;

    public WWW deb_Bundle;//依赖包
    public WWW temp_ab;//剩余包
    public Dictionary<string, AssetBundle> _allAbs = new Dictionary<string, AssetBundle>();
    public bool _isFull = false;
    public bool _isAllFull = false;

    void Awake()
    {
        Caching.CleanCache();
        win_streamingAssetPath =
        #if UNITY_ANDROID
             "jar:file://" + Application.dataPath + "!/assets/";
        #elif UNITY_IPHONE
             Application.dataPath + "/Raw/";  
        #elif UNITY_STANDALONE_WIN || UNITY_EDITOR
             "file://" + Application.streamingAssetsPath+"/";  
        #else
             string.Empty;  
        #endif

    }

    public void StartLoad()
    {
        StartCoroutine(LoadAbManifest(win_streamingAssetPath + "StreamingAssets"));
        StartCoroutine(LoadAllAbs());

    }
    List<string> _AbStr = new List<string>();
    List<string> _DependenciesStr = new List<string>();
    
    private IEnumerator LoadAbManifest(string path)
    {
        _isFull = false;
        _isAllFull = false;
        WWW bundle = WWW.LoadFromCacheOrDownload(path, 0);
        yield return bundle;
        if(bundle.assetBundle == null)
        {
            _isAllFull = true;
            _isFull = true;
            GameMgr.Instance.eventMaster.CallEventBundleLoaded();
            yield break;
        }
        AssetBundle _manifest_ab = bundle.assetBundle;
        AssetBundleManifest amList = (AssetBundleManifest)_manifest_ab.LoadAsset("AssetBundleManifest");
        _AbStr.Clear();
        for (int i = 0; i < amList.GetAllAssetBundles().Length; ++i)
        {
            _AbStr.Add(amList.GetAllAssetBundles()[i]);

            _DependenciesStr = amList.GetAllDependencies(amList.GetAllAssetBundles()[i]).ToList();

            if (_DependenciesStr.Count > 0)
            {
                for (int j = 0; j < _DependenciesStr.Count; ++j)
                {
                    if (_allAbs.ContainsKey(_DependenciesStr[j]))
                    {
                        continue;
                    }
                    string Dab_path = string.Format("{0}{1}", win_streamingAssetPath, _DependenciesStr);
                    deb_Bundle = WWW.LoadFromCacheOrDownload(Dab_path, 0);
                    yield return deb_Bundle;
                    AssetBundle _dep_ab = deb_Bundle.assetBundle;
                    if (_dep_ab != null)
                    {
                        _allAbs.Add(_DependenciesStr[j], _dep_ab);
                    }

                }
            }
        }
        _isFull = true;

    }

    private IEnumerator LoadAllAbs()
    {

        while (!_isFull)
        {
            yield return new WaitForSeconds(0.1f);
        }
        if (_isAllFull)
        {
            yield break;
        }
        for (int i = 0; i < _AbStr.Count; ++i)
        {
            if (_allAbs.ContainsKey(_AbStr[i]))
            {
                continue;
            }
            else
            {
                string path = string.Format("{0}{1}", win_streamingAssetPath, _AbStr[i]);
                WWW temp = WWW.LoadFromCacheOrDownload(path, 0);
                yield return temp;
                AssetBundle _ab = temp.assetBundle;
                if (_ab != null)
                {
                    _allAbs.Add(_AbStr[i], _ab);
                }
            }

        }
        
        _isAllFull = true;
        GameMgr.Instance.eventMaster.CallEventBundleLoaded();

    }
}
