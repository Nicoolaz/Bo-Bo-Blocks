using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NextBlockImageCtrl : MonoBehaviour {

    Image mImage;

    void Awake()
    {
        mImage = GetComponent<Image>();
    }

    void OnEnable()
    {
        GameMgr.Instance.eventMaster.eventNextBlockChanged += NextBlockChanged;
    }

    void OnDisable()
    {
        GameMgr.Instance.eventMaster.eventNextBlockChanged -= NextBlockChanged;
    }

	void NextBlockChanged()
    {
        int next = GameSceneMgr.Instance.nextID;
        string name = string.Format("Block0{0}", next);
        mImage.sprite = GameMgr.Instance.blocksPool.GetBlockSpriteByName(name);

    }
}
