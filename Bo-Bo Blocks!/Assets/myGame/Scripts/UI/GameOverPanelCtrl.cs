using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverPanelCtrl: MonoBehaviour {

    private Text newScore;
    private Text bestScore;
    private Button gameOverPlayAgain;
    private Button gameOverExit;
    public void Awake()
    {
        newScore = transform.Find("GameOverBG/New").GetComponent<Text>();
        bestScore = transform.Find("GameOverBG/Best").GetComponent<Text>();
        gameOverPlayAgain = transform.Find("GameOverBG/Restart").GetComponent<Button>();
        gameOverExit = transform.Find("GameOverBG/Exit").GetComponent<Button>();
    }

    void OnEnable()
    {
        gameOverPlayAgain.onClick.AddListener(PlayAgain);
        gameOverExit.onClick.AddListener(Exit);
        ShowScore();
    }
    void OnDisable()
    {
        gameOverExit.onClick.RemoveAllListeners();
        gameOverPlayAgain.onClick.RemoveAllListeners();
    }
    void ShowScore()
    {
        newScore.text = GameMgr.Instance.scoreMgr.curScore.ToString();
        if (GameMgr.Instance.scoreMgr.curScore > PlayerPrefs.GetInt("1"))
        {
            bestScore.text = GameMgr.Instance.scoreMgr.curScore.ToString();
        }
        else
        {
            bestScore.text = PlayerPrefs.GetInt("1").ToString();
        }
    }
    void Exit()
    {
        SceneManager.LoadScene("GameStart");
        GameMgr.Instance.eventMaster.CallEventChangeToGameStartScene();
    }

    
    void PlayAgain()
    {
        GameMgr.Instance.eventMaster.CallEventGameRestart();
    }
}
