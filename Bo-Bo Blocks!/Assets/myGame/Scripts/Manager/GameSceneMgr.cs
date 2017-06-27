using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameSceneMgr : MonoBehaviour {
    private static GameSceneMgr Inst;
    public static GameSceneMgr Instance
    {
        get
        {
            return Inst;
        }
    }
    public Material myMaterial;

    void Awake()
    {
        Inst = this;
    }

    void OnEnable()
    {
        GameMgr.Instance.eventMaster.eventGameStart += CreateFirstBlock;
        GameMgr.Instance.eventMaster.eventBlockStop += CreateBlock;
        //GameMgr.Instance.eventMaster.eventGameOver += GameOver;
        GameMgr.Instance.eventMaster.eventGameRestart += Restart;
        GameMgr.Instance.eventMaster.eventChangeToGameStartScene += ExitToMenu;
    }

    void OnDisable()
    {
        GameMgr.Instance.eventMaster.eventGameStart -= CreateFirstBlock;
        GameMgr.Instance.eventMaster.eventBlockStop -= CreateBlock;
        //GameMgr.Instance.eventMaster.eventGameOver -= GameOver;
        GameMgr.Instance.eventMaster.eventGameRestart -= Restart;
        GameMgr.Instance.eventMaster.eventChangeToGameStartScene -= ExitToMenu;
    }

	void Start () {
        GameStart();
	}
	
    void GameStart()
    {
        GameMgr.Instance.eventMaster.CallEventGameStart();
    }

    void CreateFirstBlock()
    {
        GameMgr.Instance.isGameOver = false;
        Invoke("CreateBlock", 1f);
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    void CreateBlock()
    {
        GameObject block;
        int id = Random.Range(1, 6);
        block = GameMgr.Instance.blocksPool.GetBlockByName(string.Format("Block0{0}",id));
        //GameObject temp = Instantiate(block, new Vector3(2.5f, 47.5f, -2.5f), Quaternion.identity) as GameObject;
        GameObject temp = Instantiate(block, CheckStartPos(block), Quaternion.identity) as GameObject;
        for (int i = 0; i < temp.transform.childCount; ++i)
        {
            temp.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material = myMaterial;
        }
        if (temp.GetComponent<MyBlocks>().CheckBlocks())
        {
            GameMgr.Instance.eventMaster.CallEventGameOver();
        }

    }


    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ExitToMenu()
    {
        SceneManager.LoadScene("GameStart");
    }
    Vector3 CheckStartPos(GameObject block)
    {
        for(int i = 0; i < block.transform.childCount; ++i)
        {
            if(block.transform.GetChild(i).localPosition == Vector3.zero)
            {
                return new Vector3(2.5f, 47.5f, -2.5f);
            }
        }
        return new Vector3(5f, 50f, 5f);
    }
}
