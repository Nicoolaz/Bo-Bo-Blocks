﻿using UnityEngine;
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
        blocksDic.Clear();
        int i = 1;
        while (true)
        {
            string name = string.Format("Block0{0}", i);
            GameObject tempGo = GameMgr.Instance.bl.GetDataByName<GameObject>(name);
            if (tempGo == null)
            {
                break;
            }
            blocksDic.Add(name, tempGo);
            i++;
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
