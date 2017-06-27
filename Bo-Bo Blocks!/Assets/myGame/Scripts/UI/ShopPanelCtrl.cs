using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopPanelCtrl : MonoBehaviour {
    GameObject item;
    Transform bottom;
    Button close;
    void Awake()
    {
        item = transform.Find("Item").gameObject;
        bottom = transform.Find("Bottom");
        close = transform.Find("Close").GetComponent<Button>();
        close.onClick.AddListener(CloseCallBack);

        InitShopPanel();
    }
    void InitShopPanel()
    {
        for(int i = 1001; i < 1004; ++i)
        {
            GameObject temp = Instantiate(item, bottom) as GameObject;
            ShopItemCtrl tempCtrl = temp.AddComponent<ShopItemCtrl>();
            tempCtrl.CreatItem(i);
            temp.transform.localScale = Vector3.zero;
            temp.SetActive(true);
        }
    }

    void CloseCallBack()
    {
        GameMgr.Instance.eventMaster.CallEventToggleShopCanvas();
    }


}
