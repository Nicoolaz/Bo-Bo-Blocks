using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStartSceneCanvasCtrl : MonoBehaviour {
    Button startBtn;
    Button shop;
    Button exit;
    Button setting;
    Button rank;
    void Awake()
    {
        InitialReferences();
    }

    void InitialReferences()
    {
        startBtn = transform.Find("GameStartPanel/StartGame").GetComponent<Button>();
        startBtn.onClick.AddListener(StartGame);

        shop = transform.Find("GameStartPanel/Shop").GetComponent<Button>();
        shop.onClick.AddListener(ShopBtnCallBack);
        setting = transform.Find("GameStartPanel/Setting").GetComponent<Button>();

        exit = transform.Find("GameStartPanel/ExitGame").GetComponent<Button>();
        exit.onClick.AddListener(ExitButtonCallBack);

        rank = transform.Find("GameStartPanel/Rank").GetComponent<Button>();
        rank.onClick.AddListener(RankBtnCallBack);


    }
    void StartGame()
    {
        SceneManager.LoadScene("LowPolyWater");
    }
    void ShopBtnCallBack()
    {
        GameMgr.Instance.eventMaster.CallEventToggleShopCanvas();
    }

    void RankBtnCallBack()
    {
        GameMgr.Instance.eventMaster.CallEventToggleRankCanvas();
    }

    void ExitButtonCallBack()
    {
        Application.Quit();
    }
}
