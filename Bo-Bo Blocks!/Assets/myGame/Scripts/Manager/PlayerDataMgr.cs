using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerDataMgr : MonoBehaviour
{
    public bool isScoreDouble
    {
        private set; get;
    }

    public bool isCoinsDouble { private set; get; }

    public bool isChoseNextBlock { private set; get; }

    public int nextBlock { private set; get; }

    public int myCoins { private set; get; }

    public List<Item> myItem { private set; get; }

    public void Awake()
    {
        PlayerPrefs.SetInt("Coins", 2000);
        InitialReferences();
    }

    void OnEnable()
    {
        GameMgr.Instance.eventMaster.eventPlayerCoinsChange += ChangeCoin;
        GameMgr.Instance.eventMaster.eventPlayerItemChange += ChangeItem;
        GameMgr.Instance.eventMaster.eventPlayerUseItem += ItemUsed;
        GameMgr.Instance.eventMaster.eventChoseNextBlock += delegate (int id) { ChoseBlockItemUsed(); };
        GameMgr.Instance.eventMaster.eventGameStart += RefreshFlag;
        GameMgr.Instance.eventMaster.eventGameRestart += RefreshFlag;
    }

    void OnDisable()
    {
        GameMgr.Instance.eventMaster.eventPlayerCoinsChange -= ChangeCoin;
        GameMgr.Instance.eventMaster.eventPlayerItemChange -= ChangeItem;
        GameMgr.Instance.eventMaster.eventPlayerUseItem -= ItemUsed;
        GameMgr.Instance.eventMaster.eventChoseNextBlock -= delegate (int id) { ChoseBlockItemUsed(); };
        GameMgr.Instance.eventMaster.eventGameStart -= RefreshFlag;
        GameMgr.Instance.eventMaster.eventGameRestart -= RefreshFlag;
    }

    void ChangeItem(int id, int num)
    {
        Debug.Log("Change Item");
        for (int i = 0; i < myItem.Count; ++i)
        {
            if (myItem[i]._id == id)
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
        if (myCoins >= 1000000)
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
        if (PlayerPrefs.HasKey("Coins"))
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
            if (temp._name == null)
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

    void ItemUsed(int id)
    {
        for (int i = 0; i < myItem.Count; ++i)
        {
            if (myItem[i]._id == id)
            {
                switch (myItem[i]._func)
                {
                    case Function.choseBlock:
                        {
                            GameMgr.Instance.eventMaster.CallEventToggleChoseBlockPanel();
                            break;
                        }
                    case Function.coinTwice:
                        {
                            if (isCoinsDouble)
                            {
                                return;
                            }
                            isCoinsDouble = true;
                            myItem[i]._count -= 1;
                            break;
                        }
                    case Function.scoreTwice:
                        {
                            if (isScoreDouble)
                            {
                                return;
                            }
                            isScoreDouble = true;
                            myItem[i]._count -= 1;
                            break;
                        }
                }

            }
        }
        GameMgr.Instance.eventMaster.CallEventPlayerDataChanged();
    }

    void Save()
    {
        PlayerPrefs.SetInt("Coins", myCoins);
        for (int i = 0; i < myItem.Count; ++i)
        {
            PlayerPrefs.SetInt(myItem[i]._name, myItem[i]._count);
        }

    }

    void ChoseBlockItemUsed()
    {
        for (int i = 0; i < myItem.Count; ++i)
        {
            if(myItem[i]._func == Function.choseBlock)
            {
                myItem[i]._count -= 1;
            }
        }
        GameMgr.Instance.eventMaster.CallEventPlayerDataChanged();
    }

    public void OnApplicationQuit()
    {
        Save();
    }

    void RefreshFlag()
    {
        isScoreDouble = false;
        isCoinsDouble = false;
    }
}
