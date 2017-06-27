using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PausePanelCtrl : MonoBehaviour {
    Button restartBtn;
    Button exitToMenuBtn;
    Button settingBtn;
    Button exitGameBtn;
    Button shopBtn;
    void Awake()
    {
        InitialReferences();
    }

    void InitialReferences()
    {
        restartBtn = transform.Find("BG/Bottom/Restart").GetComponent<Button>();
        exitToMenuBtn = transform.Find("BG/Bottom/ExitToMenu").GetComponent<Button>();
        settingBtn = transform.Find("BG/Bottom/Setting").GetComponent<Button>();
        exitGameBtn = transform.Find("BG/Bottom/ExitGame").GetComponent<Button>();
        shopBtn = transform.Find("BG/Bottom/Shop").GetComponent<Button>();

        restartBtn.onClick.AddListener(Restart);
        exitToMenuBtn.onClick.AddListener(ExitToMenu);
        exitGameBtn.onClick.AddListener(ExitGame);
        shopBtn.onClick.AddListener(ShopBtnCallBack);
    }

    void Restart()
    {
        GameMgr.Instance.eventMaster.CallEventGameRestart();
    }

    void ExitToMenu()
    {
        GameMgr.Instance.eventMaster.CallEventChangeToGameStartScene();
    }

    void ExitGame()
    {
        Application.Quit();
    }

    void ShopBtnCallBack()
    {
        GameMgr.Instance.eventMaster.CallEventToggleShopCanvas();
    }
}
