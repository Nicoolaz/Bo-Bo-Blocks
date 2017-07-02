using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class RankListCanvasCtrl : MonoBehaviour {
    GameObject score;
    Transform scoreScroll;
    Transform content;
    Button close;
    void Awake()
    {
        scoreScroll = transform.Find("Panel/ScoreScroll");
        content = scoreScroll.Find("Viewport/Content");
        score = scoreScroll.Find("ScoreParent").gameObject;
        close = transform.Find("Close").GetComponent<Button>();
        close.onClick.AddListener(CloseCallBack);
        print(content);

    }

    void OnEnable()
    {
        InitRank();
    }

	void InitRank()
    {
        int mScore = 0;
        int index = 1;
        while (GameMgr.Instance.scoreMgr.GetScoreByIndex(index,out mScore))
        {
            
            if (index<=content.childCount)
            {
                content.GetChild(index - 1).Find("Score").GetComponent<Text>().text = mScore.ToString();
                ++index;
                continue;
            }
            else
            {
                GameObject temp = Instantiate(score, content) as GameObject;
                temp.SetActive(true);
                temp.transform.Find("Name").GetComponent<Text>().text = "Player";
                temp.transform.Find("Score").GetComponent<Text>().text = mScore.ToString();
                temp.transform.localScale = Vector3.one;             
            }
        }
    }

    void CloseCallBack()
    {
        GameMgr.Instance.eventMaster.CallEventToggleRankCanvas();
    }
}
