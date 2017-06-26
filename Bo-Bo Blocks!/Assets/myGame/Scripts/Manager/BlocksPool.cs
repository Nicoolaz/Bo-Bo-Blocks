using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlocksPool : MonoBehaviour {
    Dictionary<string, GameObject> blocksDic = new Dictionary<string, GameObject>();

    void Awake()
    {
        InitialReferences();
    }

    void InitialReferences()
    {
        AssetBundle ab = null;
        blocksDic.Clear();
        if (GameMgr.Instance.bl._allAbs.ContainsKey("p001"))
        {
            ab = GameMgr.Instance.bl._allAbs["p001"];
        }
        if (ab != null)
        {
            for (int i = 0; i < ab.LoadAllAssets().Length; ++i)
            {
                if (!blocksDic.ContainsKey(ab.LoadAllAssets()[i].name))
                {
                    if (ab.LoadAllAssets()[i].name.Contains("Block"))
                    {
                        blocksDic.Add(ab.LoadAllAssets()[i].name, ab.LoadAllAssets()[i] as GameObject);
                    }
                }
            }
        }
    }

    public GameObject GetBlockByName(string name)
    {
        if (blocksDic.ContainsKey(name))
        {
            return blocksDic[name];
        }
        else
        {
            Debug.Log("Can not find a GameObject with the name!");
        }
        return null;
    }
}
