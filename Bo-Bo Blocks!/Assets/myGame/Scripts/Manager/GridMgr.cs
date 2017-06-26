using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GridMgr : MonoBehaviour {
    public int[,,] gridArray ;
    public int width = 8;
    public int length = 8;
    public int height = 10;
    public Dictionary<Vector3, GameObject> stonesDic = new Dictionary<Vector3, GameObject>();
    List<int> layersForDelete = new List<int>();
    GameObject[] allStones;


    void Awake()
    {
        CreateGrid();
    }
    void OnEnable()
    {
        //GameMgr.Instance.eventMaster.eventBlockStop += UpdateDictionary;
        GameMgr.Instance.eventMaster.eventDeleteStones += DeleteStones;
        GameMgr.Instance.eventMaster.eventCheckForDelete += LateCheck;
        GameMgr.Instance.eventMaster.eventGameStart += ClearDictionary;
        //GameMgr.Instance.eventMaster.eventGameStart += UpdateGrid;
        GameMgr.Instance.eventMaster.eventBlockStop += LateCheck;
        GameMgr.Instance.eventMaster.eventGameOver += ClearGrid;
        

    }

    void OnDisable()
    {
        GameMgr.Instance.eventMaster.eventDeleteStones -= DeleteStones;
        GameMgr.Instance.eventMaster.eventCheckForDelete -= LateCheck;
        GameMgr.Instance.eventMaster.eventGameStart -= ClearDictionary;
        //GameMgr.Instance.eventMaster.eventGameStart -= UpdateGrid;
        GameMgr.Instance.eventMaster.eventBlockStop -= LateCheck;
        GameMgr.Instance.eventMaster.eventGameOver -= ClearGrid;
       // GameMgr.Instance.eventMaster.eventBlockStop -= UpdateDictionary;
    }

    void CreateGrid()
    {
        gridArray = new int[width, height, length];
        for(int i = 0; i < width; ++i)
        {
            for(int j = 0; j < height; ++j)
            {
                for(int k = 0; k < length; ++k)
                {
                    gridArray[i, j, k] = 0;
                }
            }
        }
    }

    void ClearGrid()
    {
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                for (int k = 0; k < length; ++k)
                {
                    gridArray[i, j, k] = 0;
                }
            }
        }
    }
    //重写！
    void UpdateGrid()
    {
        
        ClearGrid();
        if (stonesDic.Values.Count == 0)
        {
            return;
        }
        foreach(GameObject go in stonesDic.Values)
        {
            if(go == null)
            {
                return;
            }
            Vector3 cell = GetCellWithPos(go.transform.position);
            gridArray[(int)cell.x, (int)cell.y, (int)cell.z] = 1;
        }
         
    }
    void UpdateDictionary()
    {
        allStones = GameObject.FindGameObjectsWithTag("stone");
        for(int i = 0; i < allStones.Length; ++i)
        {
            if (!stonesDic.ContainsKey(allStones[i].transform.position))
            {
                stonesDic.Add(allStones[i].transform.position, allStones[i]);
            }
        }
        UpdateGrid();
    }

    void CheckForDelete()
    {

        if (GameMgr.Instance.isGameOver)
        {
            return;
        }
        Debug.Log("aa");
        UpdateDictionary();
        //UpdateGrid();
        layersForDelete.Clear();
        //print(stonesDic.Values.Count);
        for (int i = 0; i < height; i++)
        {
            int layerCount = 0;
            for(int j = 0; j < width; ++j)
            {
                for(int k = 0; k < length; ++k)
                {
                    layerCount += gridArray[j, i, k];
                }
            }
            if (layerCount == 64)
            {
                layersForDelete.Add(i);
            }    
        }
        if (layersForDelete.Count > 0)
        {
            switch (layersForDelete.Count)
            {
                case 1:
                    {
                        GameMgr.Instance.scoreMgr.GetScore(10);
                        break;
                    }
                case 2:
                    {
                        GameMgr.Instance.scoreMgr.GetScore(30);
                        break;
                    }
                case 3:
                    {
                        GameMgr.Instance.scoreMgr.GetScore(50);
                        break;
                    }
                default:
                    {
                        GameMgr.Instance.scoreMgr.GetScore(100);
                        break;
                    }

            }
            GameMgr.Instance.eventMaster.CallEventDeleteStones();
        }
    }

    void DeleteStones()
    {

        for(int i = 0; i < layersForDelete.Count; ++i)
        {
            for(int j = 0; j < width; ++j)
            {
                for(int k = 0; k < length; ++k)
                {
                    Vector3 pos = GetPosWithCell(new Vector3(j, layersForDelete[i], k));
                    if (stonesDic.ContainsKey(pos))
                    {
                        Destroy(stonesDic[pos]);
                        stonesDic.Remove(pos);
                    }
                    else
                    {
                        Debug.Log("Can't find the stone for delete!");
                    }
                    gridArray[j, layersForDelete[i], k] = 0;
                }
            }
        }
        MoveStonesAfterDelete();
    }

    void MoveStonesAfterDelete()
    {

        //allStones = GameObject.FindGameObjectsWithTag("stone");
        //Debug.Log(allStones.Length);
        
        foreach(GameObject go in stonesDic.Values)
        {
            int moveCount = 0;
            for(int j = 0; j < layersForDelete.Count; ++j)
            {
                if (GetCellWithPos(go.transform.position).y > layersForDelete[j])
                {
                    moveCount++;
                }
            }
            Debug.Log(moveCount);
            if (moveCount > 0)
            {
                GameObject temp = go;
                stonesDic.Remove(go.transform.position);             
                Vector3 pos = temp.transform.position;
                pos.y -= 5 * moveCount;
                temp.transform.position = pos;
                stonesDic.Add(go.transform.position, temp);
            }
        }
        Debug.Log("aa1");
        UpdateGrid();
        GameMgr.Instance.eventMaster.CallEventCheckForDelete();
    }

    public Vector3 GetPosWithCell(Vector3 cell)
    {
        Vector3 pos = new Vector3();
        pos.x = cell.x * 5 - 17.5f;
        pos.z = cell.z * 5 - 17.5f;
        pos.y = cell.y * 5 + 7.5f;
        return pos;
    }

    public Vector3 GetCellWithPos(Vector3 pos)
    {
        Vector3 cell = new Vector3();
        cell.x = FloatToInt((pos.x + 17.5f) * 0.2f);
        cell.y = FloatToInt((pos.y - 7.5f) * 0.2f);
        cell.z = FloatToInt((pos.z + 17.5f) * 0.2f);
        
        return cell;
    }

    int FloatToInt(float f)
    {
        int i = 0;
        if (f > 0) //正数  
            i = (int)(f * 10 + 5) / 10;
        else if (f < 0) //负数  
            i = (int)(f * 10 - 5) / 10;
        else i = 0;

        return i;
    }

    void LateCheck()
    {
        if (GameMgr.Instance.isGameOver)
        {
            return;
        } 
        Invoke("CheckForDelete", 0.2f);
    }

    void CheckDelete()
    {
        GameMgr.Instance.eventMaster.CallEventCheckForDelete();
    }

    void ClearDictionary()
    {
        stonesDic.Clear();
        UpdateDictionary();
    }

}
