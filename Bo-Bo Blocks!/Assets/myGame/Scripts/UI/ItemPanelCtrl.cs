using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemPanelCtrl : MonoBehaviour {
    Dictionary<string,GameObject> childDic = new Dictionary<string, GameObject>();
    GameObject item;

    public void Awake()
    {
        item = transform.Find("Item").gameObject;
        UpdateItem();
    }

    void OnEnable()
    {
        GameMgr.Instance.eventMaster.eventPlayerDataChanged += UpdateItem;
        GameMgr.Instance.eventMaster.eventGameStart += UpdateItem;
    }
    void OnDisable()
    {
        GameMgr.Instance.eventMaster.eventPlayerDataChanged -= UpdateItem;
        GameMgr.Instance.eventMaster.eventGameStart -= UpdateItem;
    }
    void UpdateItem()
    {
        Debug.Log("Call");
        for(int i = 0; i < GameMgr.Instance.playerMgr.myItem.Count; ++i)
        {
            if (GameMgr.Instance.playerMgr.myItem[i]._count > 0)
            {
                if (!childDic.ContainsKey(GameMgr.Instance.playerMgr.myItem[i]._name))
                {
                    GameObject temp = Instantiate(item, transform) as GameObject;
                    temp.SetActive(true);
                    ItemCtrl myItemCtrl = temp.GetComponent<ItemCtrl>();
                    temp.transform.localScale = Vector3.one;              
                    myItemCtrl.Init(GameMgr.Instance.playerMgr.myItem[i]);
                    childDic.Add(GameMgr.Instance.playerMgr.myItem[i]._name,temp);
                }
                else
                {
                    childDic[GameMgr.Instance.playerMgr.myItem[i]._name].GetComponent<ItemCtrl>().count.text = string.Format("X{0}", GameMgr.Instance.playerMgr.myItem[i]._count);
                }
                
            }
            else
            {
                if (childDic.ContainsKey(GameMgr.Instance.playerMgr.myItem[i]._name))
                {
                    Destroy(childDic[GameMgr.Instance.playerMgr.myItem[i]._name]);
                }
            }
        }
    }
	
}
