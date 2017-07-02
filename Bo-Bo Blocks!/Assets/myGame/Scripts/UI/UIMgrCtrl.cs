using UnityEngine;
using System.Collections;

public class UIMgrCtrl: MonoBehaviour {
    UIStateMachine uiStateMachine;
    GameObject shopCanvas;
    GameObject RankCanvas;
    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("UICtrl").Length > 1)
        {
            Destroy(gameObject);
        }        
        else
        {
            shopCanvas = transform.Find("ShopCanvas").gameObject;
            RankCanvas = transform.Find("RankCanvas").gameObject;
            uiStateMachine = new UIStateMachine();
            GameStartState gameStartState = new GameStartState(transform);
            uiStateMachine.AddState(gameStartState);

            GameSceneState gameSceneState = new GameSceneState(transform);
            uiStateMachine.AddState(gameSceneState);

            GameLoadingState gameLoadingState = new GameLoadingState(transform);
            uiStateMachine.AddState(gameLoadingState);

            uiStateMachine.ChangeState(typeof(GameLoadingState));

            DontDestroyOnLoad(gameObject);
        }
    }
	void Start()
    {
        GameMgr.Instance.eventMaster.eventGameStart += ChangeStateToGameScene;
        GameMgr.Instance.eventMaster.eventChangeToGameStartScene += ChangeStateToGameStart;
        GameMgr.Instance.eventMaster.eventBundleLoaded += ChangeStateToGameStart;
        GameMgr.Instance.eventMaster.eventToggleShopCanvas += ToggleShopPanel;
        GameMgr.Instance.eventMaster.eventToggleRankCanvas += ToggleRankCanvas;
    }

    void OnDisable()
    {
        GameMgr.Instance.eventMaster.eventGameStart -= ChangeStateToGameScene;
        GameMgr.Instance.eventMaster.eventChangeToGameStartScene -= ChangeStateToGameStart;
        GameMgr.Instance.eventMaster.eventBundleLoaded -= ChangeStateToGameStart;
        GameMgr.Instance.eventMaster.eventToggleShopCanvas -= ToggleShopPanel;
        GameMgr.Instance.eventMaster.eventToggleRankCanvas -= ToggleRankCanvas;
    }
    void ChangeStateToGameScene()
    {
        uiStateMachine.ChangeState(typeof(GameSceneState));
    }

    void ChangeStateToGameStart()
    {
        uiStateMachine.ChangeState(typeof(GameStartState));
    }
    void ChangeStateToGameLoading()
    {
        uiStateMachine.ChangeState(typeof(GameLoadingState));
    }
    void ToggleShopPanel()
    {
        //Debug.Log(shopCanvas.activeSelf);
        shopCanvas.SetActive(!shopCanvas.activeSelf);
    }
    void ToggleRankCanvas()
    {
        RankCanvas.SetActive(!RankCanvas.activeSelf);
    }
}
