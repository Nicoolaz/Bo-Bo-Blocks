using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameSceneUICtrl : MonoBehaviour {
    private Button forward;
    private Button back;
    private Button left;
    private Button right;
    private Button roX;
    private Button roY;
    private Button roZ;
    private Button fast;
    private Text scoreShow;
    private Transform gameOverPanel;
    private Transform gamePausePanel;
    private Button pause;
    private GameObject ScoreDouble;

    GameObject ChoseBlockPanel;

    void Awake () {
        InitialReferences();

    }

    void OnEnable()
    {

        //Debug.Log(GameMgr.Instance);
        GameMgr.Instance.eventMaster.eventScoreChanged += UpdateScore;
        GameMgr.Instance.eventMaster.eventPlayerDataChanged += UpdateScore;
        GameMgr.Instance.eventMaster.eventGameOver += ToggleGameOverPanel;
        GameMgr.Instance.eventMaster.eventGameRestart += ToggleGamePausePanel;
        GameMgr.Instance.eventMaster.eventGameRestart += ToggleGameOverPanel;
        GameMgr.Instance.eventMaster.eventGamePause += ToggleGamePausePanel;
        GameMgr.Instance.eventMaster.eventChangeToGameStartScene += ToggleGamePausePanel;
        GameMgr.Instance.eventMaster.eventChangeToGameStartScene += ToggleGameOverPanel;
        GameMgr.Instance.eventMaster.eventToggleChoseBlockPanel += ToggleChoseBlockPanel;
        GameMgr.Instance.eventMaster.eventChoseNextBlock +=delegate(int id) { ToggleChoseBlockPanel(); };
        
    }
    void OnDisable()
    {
        GameMgr.Instance.eventMaster.eventScoreChanged -= UpdateScore;
        GameMgr.Instance.eventMaster.eventPlayerDataChanged -= UpdateScore;
        GameMgr.Instance.eventMaster.eventGameOver -= ToggleGameOverPanel;
        GameMgr.Instance.eventMaster.eventGameRestart -= ToggleGamePausePanel;
        GameMgr.Instance.eventMaster.eventGameRestart -= ToggleGameOverPanel;
        GameMgr.Instance.eventMaster.eventGamePause -= ToggleGamePausePanel;
        GameMgr.Instance.eventMaster.eventChangeToGameStartScene -= ToggleGamePausePanel;
        GameMgr.Instance.eventMaster.eventChangeToGameStartScene -= ToggleGameOverPanel;
        GameMgr.Instance.eventMaster.eventToggleChoseBlockPanel -= ToggleChoseBlockPanel;
        GameMgr.Instance.eventMaster.eventChoseNextBlock -= delegate (int id) { ToggleChoseBlockPanel(); };

    }
	
    void InitialReferences()
    {
        forward = transform.Find("MovePanel/Forward").GetComponent<Button>();
        back = transform.Find("MovePanel/Back").GetComponent<Button>();
        left = transform.Find("MovePanel/Left").GetComponent<Button>();
        right = transform.Find("MovePanel/Right").GetComponent<Button>();
        roX = transform.Find("RotatePanel/RoX").GetComponent<Button>();
        roY = transform.Find("RotatePanel/RoY").GetComponent<Button>();
        roZ = transform.Find("RotatePanel/RoZ").GetComponent<Button>();
        fast = transform.Find("RotatePanel/Fast").GetComponent<Button>();
        scoreShow = transform.Find("ScorePanel/ScoreText").GetComponent<Text>();
        gameOverPanel = transform.Find("GameOver");
        pause = transform.Find("Pause").GetComponent<Button>();
        gamePausePanel = transform.Find("Menu");
        ChoseBlockPanel = transform.Find("ChoseBlockPanel").gameObject;
        ScoreDouble = transform.Find("ScorePanel/Double").gameObject;
        
        forward.onClick.AddListener(Forward);
        back.onClick.AddListener(Back);
        left.onClick.AddListener(Left);
        right.onClick.AddListener(Right);
        roX.onClick.AddListener(RoX);
        roY.onClick.AddListener(RoY);
        roZ.onClick.AddListener(RoZ);
        fast.onClick.AddListener(Fast);
        pause.onClick.AddListener(Pause);
    }
    #region ButtonCallBack
    void Left()
    {
        Debug.Log("aa");
        GameMgr.Instance.eventMaster.CallEventBlockMove(Directions.Left);
    }
    void Right()
    {
        GameMgr.Instance.eventMaster.CallEventBlockMove(Directions.Right);
    }
    void Forward()
    {
        GameMgr.Instance.eventMaster.CallEventBlockMove(Directions.Forward);
    }
    void Back()
    {
        GameMgr.Instance.eventMaster.CallEventBlockMove(Directions.Back);
    }
    void RoX()
    {
        GameMgr.Instance.eventMaster.CallEventBlockRotate(RotateDir.X);
    }
    void RoY()
    {
        GameMgr.Instance.eventMaster.CallEventBlockRotate(RotateDir.Y);
    }
    void RoZ()
    {
        GameMgr.Instance.eventMaster.CallEventBlockRotate(RotateDir.Z);
    }
    void Fast()
    {
        GameMgr.Instance.eventMaster.CallEventBlockSpeedUp();
    }
    void Pause()
    {
        GameMgr.Instance.eventMaster.CallEventGamePause();
    }

    #endregion


    void UpdateScore()
    {
        if (GameMgr.Instance.playerMgr.isScoreDouble)
        {
            ScoreDouble.SetActive(true);
        }
        else
        {
            ScoreDouble.SetActive(false);
        }
        scoreShow.text = GameMgr.Instance.scoreMgr.curScore.ToString();
    }
    void ToggleGameOverPanel()
    {
        if (!gamePausePanel.gameObject.activeSelf&&GameMgr.Instance.isGameOver)
        {
            gameOverPanel.gameObject.SetActive(!gameOverPanel.gameObject.activeSelf);
        }
    }
    void ToggleGamePausePanel()
    {
        if (!GameMgr.Instance.isGameOver&&!gameOverPanel.gameObject.activeSelf)
        {
            gamePausePanel.gameObject.SetActive(!gamePausePanel.gameObject.activeSelf);
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
        }
    }

    void ToggleChoseBlockPanel()
    {
        ChoseBlockPanel.SetActive(!ChoseBlockPanel.activeSelf);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}
