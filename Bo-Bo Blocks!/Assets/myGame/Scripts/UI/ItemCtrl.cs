using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemCtrl : MonoBehaviour {
    Image icon;
    public Text count;
    Button myButton;
	public void Init(Item item)
    {
        icon = GetComponent<Image>();
        count = transform.Find("Count").GetComponent<Text>();
        icon.sprite = item._sprite;
        count.text = string.Format("X{0}", item._count);
        if (item._count <= 0)
        {
            Destroy(gameObject);
        }
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(delegate { MyButtonCallBack(item._id); });
    }

    void MyButtonCallBack(int id)
    {
        GameMgr.Instance.eventMaster.CallEventPlayerUseItem(id);
    }

}
