using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ChoseBlockPanelCtrl : MonoBehaviour {
    Transform blockPanel;
    Button thisBtn;
    void Awake()
    {       
        Init();
    }

    void Init()
    {
        //int[] idArray = { 1, 2, 3, 4, 5 };
        thisBtn = GetComponent<Button>();
        thisBtn.onClick.AddListener(thisBtnCallBack);
        blockPanel = transform.Find("Panel");
        for (int i = 0; i < blockPanel.childCount; ++i)
        {
            ID id = new ID(i + 1);
            Button temp = blockPanel.GetChild(i).GetComponent<Button>();
            temp.onClick.AddListener(delegate { BlockButtonCallBack(id.id); });
        }
    }
    void thisBtnCallBack()
    {
        GameMgr.Instance.eventMaster.CallEventToggleChoseBlockPanel();
    }
    void BlockButtonCallBack(int id)
    {
        print(id);
        GameMgr.Instance.eventMaster.CallEventChoseNextBlock(id);
    }

	
}
public class ID
{
    public int id;
    public ID(int id)
    {
        this.id = id;
    }
}
