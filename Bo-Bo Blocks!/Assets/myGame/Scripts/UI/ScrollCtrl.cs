using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScrollCtrl : MonoBehaviour {
    Slider m_s;
    Text m_t;
    void Awake()
    {
        m_s =transform.Find("Slider").GetComponent<Slider>();
        m_t = transform.Find("Text").GetComponent<Text>();
    }
    // Use this for initialization
    void Start()
    {
        m_s.onValueChanged.AddListener(delegate { ChangeText(); });
    }

    // Update is called once per frame
    void Update()
    {      
        if (!GameMgr.Instance.bl._isFull)
        {
            if (GameMgr.Instance.bl.deb_Bundle != null)
            {
                m_s.value = GameMgr.Instance.bl.deb_Bundle.progress;
            }
        }
        else if (!GameMgr.Instance.bl._isAllFull)
        {
            if (GameMgr.Instance.bl.temp_ab != null)
            {
                m_s.value = GameMgr.Instance.bl.temp_ab.progress;              
            }
        }
        else
        {
                        
            m_t.text = "加载完成";
            GameMgr.Instance.eventMaster.CallEventBundleLoaded();
        }
    }
    void ChangeText()
    {
        m_t.text = m_s.value * 100 + "%";
    }
}
