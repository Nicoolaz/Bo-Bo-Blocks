  using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ScoreMgr : MonoBehaviour {
    private Dictionary<string, int> scoreSequence = new Dictionary<string, int>();
    public int curScore { private set; get; }

    void Awake()
    {
        scoreSequence.Clear();
        for(int i = 1; i < 11; ++i)
        {
            if (PlayerPrefs.HasKey(i.ToString()))
            {
                scoreSequence.Add(i.ToString(), PlayerPrefs.GetInt(i.ToString()));
            }
        }
        if(scoreSequence.Values.Count == 0)
        {
            PlayerPrefs.SetInt("1", 0);
        }
        curScore = 0;
    }

    void OnEnable()
    {
        GameMgr.Instance.eventMaster.eventGameStart += InitScore;
        GameMgr.Instance.eventMaster.eventGameOver += InsertScore;
    }
    void OnDisable()
    {
        GameMgr.Instance.eventMaster.eventGameStart -= InitScore;
        GameMgr.Instance.eventMaster.eventGameOver += InsertScore;
    }

    public void InsertScore()
    {
        if (GameMgr.Instance.playerMgr.isScoreDouble)
        {
            curScore *= 2;
        }
        if (scoreSequence.Count == 0)
        {
            scoreSequence.Add("1", curScore);
        }
        else
        {
            for(int i=1;i <= scoreSequence.Count; ++i)
            {
                if (curScore > scoreSequence[i.ToString()])
                {
                    int temp = scoreSequence[i.ToString()];
                    scoreSequence[i.ToString()] = curScore;
                    for(int j = i + 1; j <= scoreSequence.Count+1; ++j)
                    {
                        if (j > 10)
                        {
                            SaveScore();
                            return;
                        }
                        if (scoreSequence.ContainsKey(j.ToString()))
                        {
                            int temp1 = scoreSequence[j.ToString()];
                            scoreSequence.Add(j.ToString(), temp);
                            temp = temp1;
                        }
                        else
                        {
                            scoreSequence.Add(j.ToString(), temp);
                            SaveScore();
                            return;
                        }
                    }
                }
            }
            if (scoreSequence.Count < 10)
            {
                scoreSequence.Add((scoreSequence.Count + 1).ToString(), curScore);
                SaveScore();
                return;
            }
            return;
        }
    }

    void SaveScore()
    {
        for(int i = 1; i <= scoreSequence.Count; ++i)
        {
            PlayerPrefs.SetInt(i.ToString(), scoreSequence[i.ToString()]);
        }
    }

    public void GetScore(int value)
    {
        curScore += value;
        GameMgr.Instance.eventMaster.CallEventScoreChanged();
    }

    void InitScore()
    {
        curScore = 0;
        GameMgr.Instance.eventMaster.CallEventScoreChanged();
    }

    public bool GetScoreByIndex(int index ,out int score)
    {
        if (scoreSequence.ContainsKey(index.ToString()))
        {
            score = scoreSequence[index.ToString()];
            return true;
        }
        score = 0;
        return false;
    }

}
