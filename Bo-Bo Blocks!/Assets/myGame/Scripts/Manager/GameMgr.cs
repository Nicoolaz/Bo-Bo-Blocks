﻿using UnityEngine;
using System.Collections;

public class GameMgr : MonoBehaviour {

    private static GameMgr Inst;
    public static GameMgr Instance
    {
        get
        {
            return Inst;
        }
    }

    public BundleLoader bl
    {
        get;
        private set;
    }
    public GridMgr gridMgr
    {
        private set;
        get;
    }
    public EventMaster eventMaster
    {
        private set;
        get;
    }
    public ScoreMgr scoreMgr { private set; get; }
    public BlocksPool blocksPool { private set; get; }
    public PlayerDataMgr playerMgr { private set; get; }
    public CSVLoader csvLoader{ private set;get; }
    float timeToClear = 3f;
    float timer;

    public bool isGameOver = false;

    void Awake()
    {

        if (GameMgr.Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        { 
            Inst = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnEnable()
    {
       // Debug.Log("1");
        
        bl = gameObject.AddComponent<BundleLoader>();
        if (!bl._isAllFull)
        {
            bl.StartLoad();
        }
        eventMaster = gameObject.AddComponent<EventMaster>();
        eventMaster.eventBundleLoaded += InitialReferences;
        eventMaster.eventGameOver += GameOver;
    }

    void OnDisable()
    {
        if (eventMaster != null)
        {
            eventMaster.eventBundleLoaded -= InitialReferences;
            eventMaster.eventGameOver -= GameOver;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToClear)
        {
            System.GC.Collect();
            timer = 0;
        }
    }

    private void InitialReferences()
    {
        gridMgr = gameObject.AddComponent<GridMgr>();
        blocksPool = gameObject.AddComponent<BlocksPool>();
        scoreMgr = gameObject.AddComponent<ScoreMgr>();
        csvLoader = gameObject.AddComponent<CSVLoader>();
        playerMgr = gameObject.AddComponent<PlayerDataMgr>();
        

    }

    void GameOver()
    {
        Debug.Log("GameOver");
        isGameOver = true;
        Time.timeScale = 0;
    }

}
