using UnityEngine;
using System.Collections;

public enum Function
{
    scoreTwice,
    choseBlock,
    coinTwice
};
public class Item {
    public int _id { private set; get; }
    public int _price { private set; get; }
    public Function _func { private set; get; }
    public int _count { get; set; }
    public string _name { private set; get; }
    public Sprite _sprite { private set; get; }
    public Item (int id)
    {
        this._id = id;
        _count = 0;
        string[][] table = GameMgr.Instance.csvLoader.csvTableDtata["Item"];
        for(int i = 1; i < table.Length; ++i)
        {
            if (int.Parse(table[i][0]) == id)
            {
                for(int j = 1; j < table[0].Length; ++j)
                {
                    switch (table[0][j])
                    {
                        case "Name":
                            {
                                _name = table[i][j];
                                break;
                            }
                        case "Function":
                            {
                                switch (table[i][j])
                                {
                                    case "ScoreTwice":
                                        {
                                            _func = Function.scoreTwice;
                                            break;
                                        }
                                    case "ChoseBlock":
                                        {
                                            _func = Function.choseBlock;
                                            break;
                                        }
                                    case "CoinTwice":
                                        {
                                            _func = Function.coinTwice;
                                            break;
                                        }
                                }
                                break;
                            }
                        case "Price":
                            {
                                _price = int.Parse(table[i][j]);
                                break;
                            }
                    }
                    _sprite = GameMgr.Instance.bl.GetDataByName<Sprite>(_name);
                }
            }

        }
        if (_name == null)
        {
            Debug.Log("You do not define the item");
        }
    }
}
