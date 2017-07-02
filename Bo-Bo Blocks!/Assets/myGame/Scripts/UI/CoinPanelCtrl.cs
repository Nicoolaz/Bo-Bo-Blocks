using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CoinPanelCtrl : MonoBehaviour {
    Text count;
    GameObject coinDouble;
    void Awake()
    {
        count = transform.Find("Count").GetComponent<Text>();
        coinDouble = transform.Find("Double").gameObject;
        UpdateCoins();
    }

    void OnEnable()
    {
        GameMgr.Instance.eventMaster.eventPlayerDataChanged += UpdateCoins;
        GameMgr.Instance.eventMaster.eventGameStart += UpdateCoins;
    }

    void OnDisable()
    {
        GameMgr.Instance.eventMaster.eventPlayerDataChanged -= UpdateCoins;
        GameMgr.Instance.eventMaster.eventGameStart -= UpdateCoins;
    }

    void UpdateCoins()
    {
        if (GameMgr.Instance.playerMgr.isCoinsDouble)
        {
            coinDouble.SetActive(true);
        }
        else
        {
            coinDouble.SetActive(false);
        }
        count.text = GameMgr.Instance.playerMgr.myCoins.ToString();
    }
}
