using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopItemCtrl : MonoBehaviour {
    Button increase;
    Button decrease;
    Button buy;
    Image itemIcon;
    Text buyCount;
    Text itemName;
    Text itemPrice;
    Item thisItem;
    int curBuyCount;
    void Awake()
    {
        InitialReferences();
        CheckButton();
    }
	
    void InitialReferences()
    {
        increase = transform.Find("Increase").GetComponent<Button>();
        decrease = transform.Find("Decrease").GetComponent<Button>();
        buy = transform.Find("Buy").GetComponent<Button>();
        itemIcon = transform.Find("Image").GetComponent<Image>();
        buyCount = transform.Find("BuyCount/bg/Count").GetComponent<Text>();
        itemName = transform.Find("Name").GetComponent<Text>();
        itemPrice = transform.Find("Price").GetComponent<Text>();

        increase.onClick.AddListener(IncreaseCallBack);
        decrease.onClick.AddListener(DecreaseCallBack);
        buy.onClick.AddListener(BuyCallBack);
    }

    public void CreatItem(int id)
    {
        thisItem = new Item(id);
        itemName.text = thisItem._name;
        itemPrice.text =string.Format("Price:{0}", thisItem._price);
        curBuyCount = 0;
        buyCount.text = curBuyCount.ToString();
        itemIcon.sprite = thisItem._sprite;

    } 

    void CheckButton()
    {
        int playCoins = GameMgr.Instance.playerMgr.myCoins;
        if (curBuyCount == 0)
        {
            decrease.interactable = false;
            if (playCoins < thisItem._price)
            {
                buy.interactable = false;
                increase.interactable = false;
            }
        }
        else if (playCoins < thisItem._price * (curBuyCount+1))
        {
            buy.interactable = true;
            increase.interactable = false;
            decrease.interactable = true;
        }
        else
        {
            buy.interactable = true;
            increase.interactable = true;
            decrease.interactable = true;
        }
    }

    void IncreaseCallBack()
    {
        curBuyCount += 1;
        UpdateBuyCount();
        CheckButton();
    }

    void DecreaseCallBack()
    {
        curBuyCount -= 1;
        UpdateBuyCount();
        CheckButton();
    }

    void BuyCallBack()
    {
        GameMgr.Instance.eventMaster.CallEventPlayerItemChange(thisItem._id, curBuyCount);
        GameMgr.Instance.eventMaster.CallEventPlayerCoinsChange(-curBuyCount * thisItem._price);
    }

    void UpdateBuyCount()
    {
        buyCount.text = curBuyCount.ToString();
    }

}
