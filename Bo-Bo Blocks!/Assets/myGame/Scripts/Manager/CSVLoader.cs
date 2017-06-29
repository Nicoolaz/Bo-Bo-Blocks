using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public enum CSVs
{
    Item
};
public class CSVLoader : MonoBehaviour {
    private readonly string[] CSVTable ={ "Item" } ;
    public Dictionary<string, string[][]> csvTableDtata { private set; get; }
    string[][] tableData;
    AssetBundle myAB;
    TextAsset tempCSV;
    void Awake()
    {
        GetTableData();
    }  
    void GetTableData()
    {
        csvTableDtata = new Dictionary<string, string[][]>();
        foreach (string str in CSVTable)
        {          
            tempCSV = GameMgr.Instance.bl.GetDataByName<TextAsset>(str);
            GetCSVData(tempCSV,out tableData);
            csvTableDtata.Add(tempCSV.name, tableData);
            break;
        }
    }

    void GetCSVData(TextAsset csv,out string[][] tableData)
    {
        string[] tempArray = csv.text.Split('\r');
        tableData = new string[tempArray.Length][];
        for(int i = 0; i < tempArray.Length; ++i)
        {
            tableData[i] = tempArray[i].Split(',');
        }
        for(int i = 0; i < tempArray.Length; ++i)
        {
            tableData[i][0].Replace("\n", "");
        }
    }

    //public void GetDataInCSV(CSVs csv,int id,out object obj )
    //{
    //    switch (csv)
    //    {
    //        case CSVs.Item:
    //            {
    //                obj = GetItemByID(id);
    //                break;
    //            }
    //        default:obj = null; break;
    //    }
    //}

    //Item GetItemByID(int id)
    //{
    //    if (csvTableDtata.ContainsKey("Item"))
    //    {
    //        tableData = csvTableDtata["Item"];
    //    }
    //    Item temp = new Item();

    //}
}
