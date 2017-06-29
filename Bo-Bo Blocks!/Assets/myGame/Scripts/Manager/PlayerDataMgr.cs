using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerDataMgr : MonoBehaviour {
    public int myCoins { private set; get; }
    public List<Item> myItem { private set; get; }
    public void Awake()
    {
        PlayerPrefs.SetInt("Coins", 2000);
        InitialReferences();
    }
    void onEnable()
    {
        GameMgr.Instance.eventMaster.eventPlayerCoinsChange += ChangeCoin;
        GameMgr.Instance.eventMaster.eventPlayerItemChange += ChangeItem;
    }
    void OnDisable()
    {
        GameMgr.Instance.eventMaster.eventPlayerCoinsChange -= ChangeCoin;
        GameMgr.Instance.eventMaster.eventPlayerItemChange -= ChangeItem;
    }
	
    void ChangeItem(int id,int num)
    {
        for(int i = 0; i < myItem.Count; ++i)
        {
            if(myItem[i]._id == id)
            {
                myItem[i]._count += num;
                if (myItem[i]._count <= 0)
                {
                    myItem[i]._count -= num;
                    print("You don't have enough item to use!");
                }
           }
        }
        GameMgr.Instance.eventMaster.CallEventPlayerDataChanged();
    }

    void ChangeCoin(int value)
    {
        myCoins += value;
        if(myCoins >= 1000000)
        {
            myCoins = 999999;
        }
        else if (myCoins < 0)
        {
            myCoins -= value;
            print("You don't have enough coins to use!");
        }
        GameMgr.Instance.eventMaster.CallEventPlayerDataChanged();
    }

    void InitialReferences()
    {
        myItem = new List<Item>();
        if (PlayerPrefs.HasKey("Coins") )
        {
            myCoins = PlayerPrefs.GetInt("Coins");
        }
        else
        {
            PlayerPrefs.SetInt("Coins", 2000);
            myCoins = 2000;
        }
        int id = 1001;
        while (true)
        {
            Item temp = new Item(id);
            if(temp._name == null)
            {
                break;
            }
            if (PlayerPrefs.HasKey(temp._name))
            {
                temp._count = PlayerPrefs.GetInt(temp._name);
            }
            else
            {
                PlayerPrefs.SetInt(temp._name, 0);
            }          
            myItem.Add(temp);
            id++;
        }
       
    }

    

}
