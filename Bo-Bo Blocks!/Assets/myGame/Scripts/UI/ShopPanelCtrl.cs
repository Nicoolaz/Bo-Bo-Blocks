using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopPanelCtrl : MonoBehaviour {
    GameObject item;
    Transform bottom;
    Button close;
    Dictionary<int,GameObject> itemList = new Dictionary<int, GameObject>();
    void OnEnable()
    {
        item = transform.Find("Item").gameObject;
        bottom = transform.Find("Bottom");
        close = transform.Find("Close").GetComponent<Button>();
        close.onClick.AddListener(CloseCallBack);

        InitShopPanel();
    }
    void OnDisable()
    {
        close.onClick.RemoveAllListeners();
    }
    void InitShopPanel()
    {

        for(int i = 1001; i < 1004; ++i)
        {
            if (itemList.ContainsKey(i))
            {
                continue;
            }
            GameObject temp = Instantiate(item, bottom) as GameObject;
            temp.SetActive(true);
            temp.GetComponent<ShopItemCtrl>().CreatItem(i);
            temp.transform.localScale = Vector3.one;
            itemList.Add(i, temp); 
            
        }
    }

    void CloseCallBack()
    {
        GameMgr.Instance.eventMaster.CallEventToggleShopCanvas();
    }


}
