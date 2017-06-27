using UnityEngine;
using System.Collections;

public class SubBlocks : MonoBehaviour {

    MyBlocks mb;
    public int leftAble { private set; get; }
    public int rightAble { private set; get; }
    public int forwardAble { private set; get; }
    public int backAble { private set; get; }
    public int downAble { private set; get; }
    public int roXable { private set; get; }
    public int roYable { private set; get; }
    public int roZable { private set; get; }
    Vector3 roXPos;
    Vector3 roYPos;
    Vector3 roZPos;

    void OnEnable()
    {
        GameMgr.Instance.eventMaster.eventBlockStop += Stop;
    }

    void OnDisable()
    {
        GameMgr.Instance.eventMaster.eventBlockStop -= Stop;
    }

    // Use this for initialization
    void Start () {
	    mb = transform.parent.GetComponent<MyBlocks>();
        leftAble = 1;
        rightAble = 1;
        forwardAble = 1;
        backAble = 1;
        roXable = 1;
        roYable = 1;
        roZable = 1;
    }
    
	
	// Update is called once per frame
	void FixedUpdate () {
        CheckForMoveable();
        CheckForRotateable();


    }
    void CheckForMoveable()
    {
        
        if (!GameMgr.Instance.gridMgr.stonesDic.ContainsKey(transform.position+(mb.dirLeft * 5)))
        {
            int xPos = (int)(GameMgr.Instance.gridMgr.GetCellWithPos(transform.position) + mb.dirLeft).x;
            int zPos = (int)(GameMgr.Instance.gridMgr.GetCellWithPos(transform.position) + mb.dirLeft).z;
            if (xPos >= 0 && xPos < 8 && zPos >= 0 && zPos < 8)
            {
                leftAble = 1;
            }
            else
            {
                leftAble = 0;
            }
        }
        else
        {
            leftAble = 0;
        }
        if (!GameMgr.Instance.gridMgr.stonesDic.ContainsKey(transform.position + (mb.dirRight * 5)))
        {
            int xPos = (int)(GameMgr.Instance.gridMgr.GetCellWithPos(transform.position) + mb.dirRight).x;
            int zPos = (int)(GameMgr.Instance.gridMgr.GetCellWithPos(transform.position) + mb.dirRight).z;
            if (xPos >= 0 && xPos < 8 && zPos >= 0 && zPos < 8)
            {
                rightAble = 1;
            }
            else
            {
                rightAble = 0;
            }
        }
        else
        {
            rightAble = 0;
        }
        if (!GameMgr.Instance.gridMgr.stonesDic.ContainsKey(transform.position + (mb.dirForward * 5)))
        {
            int xPos = (int)(GameMgr.Instance.gridMgr.GetCellWithPos(transform.position) + mb.dirForward).x;
            int zPos = (int)(GameMgr.Instance.gridMgr.GetCellWithPos(transform.position) + mb.dirForward).z;
            if (xPos >= 0 && xPos < 8 && zPos >= 0 && zPos < 8)
            {
                forwardAble = 1;
            }
            else
            {
                forwardAble = 0;
            }
        }
        else
        {
            forwardAble = 0;
        }
        if (!GameMgr.Instance.gridMgr.stonesDic.ContainsKey(transform.position + (mb.dirBack * 5)))
        {
            int xPos = (int)(GameMgr.Instance.gridMgr.GetCellWithPos(transform.position) + mb.dirBack).x;
            int zPos = (int)(GameMgr.Instance.gridMgr.GetCellWithPos(transform.position) + mb.dirBack).z;
            if (xPos >= 0 && xPos < 8 && zPos >= 0 && zPos < 8)
            {
                backAble = 1;
            }
            else
            {
                backAble = 0;
            }
            
        }
        else
        {
            backAble = 0;
        }
        if(!GameMgr.Instance.gridMgr.stonesDic.ContainsKey(transform.position + (Vector3.down * 5)))
        {
            if((transform.position + Vector3.down * 5).y >= 7.3f)
            {
                downAble = 1;
            }
            else
            {
                //Debug.Log("ArrayOutOfRange");
                downAble = 0;
            }
        }
        else
        {
            //Debug.Log("DictionaryContainsKey");
            downAble = 0;
        }
    }

    void CheckForRotateable()
    {
        //Vector3 m_Cell = GameMgr.Instance.gridMgr.GetCellWithPos(transform.position);
        //Vector3 p_Cell = GameMgr.Instance.gridMgr.GetCellWithPos(transform.parent.position);
        Quaternion qX = Quaternion.Euler(mb.dirRight * 90);
        Quaternion qY = Quaternion.Euler(0,90,0);
        Quaternion qZ = Quaternion.Euler(mb.dirForward * 90);
        roXPos = qX * (transform.position-transform.parent.position);
        roXPos = FixedPos(roXPos + transform.parent.position);
        roYPos = qY * (transform.position - transform.parent.position);
        roYPos = FixedPos(roYPos + transform.parent.position);
        roZPos = qZ * (transform.position - transform.parent.position);
        roZPos = FixedPos(roZPos + transform.parent.position);
        if (!GameMgr.Instance.gridMgr.stonesDic.ContainsKey(roXPos))
        {
            float x = roXPos.x;
            float y = roXPos.y;
            float z = roXPos.z;
            //Debug.Log(GameMgr.Instance.gridMgr.GetCellWithPos(roXPos));
            //Debug.Log(roXPos);
            //Debug.Log(transform.parent.position.x);
            if (x >= -17.7f && x <= 17.7f && y >= 7.3f && y <= 52.7f && z >= -17.7f && z <= 17.7f)
            {
                roXable = 1;
            }
            else
            {
                roXable = 0;
            }
        }
        else
        {
            roXable = 0;
        }
        if (!GameMgr.Instance.gridMgr.stonesDic.ContainsKey(roYPos))
        {
            float x = roYPos.x;
            float y = roYPos.y;
            float z = roYPos.z;
            if (x >= -17.7f && x <= 17.7f && y >= 7.3f && y <= 52.7f && z >= -17.7f && z <= 17.7f)
            {
                roYable = 1;
            }
            else
            {
                roYable = 0;
            }
        }
        else
        {
            roYable = 0;
        }
        if (!GameMgr.Instance.gridMgr.stonesDic.ContainsKey(roZPos))
        {
            float x = roZPos.x;
            float y = roZPos.y;
            float z = roZPos.z;
            if (x >= -17.7f && x <= 17.7f && y >= 7.3f && y <= 52.7f && z >= -17.7f && z <= 17.7f)
            {
                roZable = 1;
            }
            else
            {
                roZable = 0;
            }
        }
        else
        {
            roZable = 0;
        }
    }

    public void Rotate_m(RotateDir dir)
    {
        switch (dir)
        {
            case RotateDir.X:
                {
                    if (mb.canRotateX)
                    {
                        transform.position = roXPos;
                    }
                    break;
                }
            case RotateDir.Y:
                {
                    if (mb.canRotateY)
                    {
                        transform.position = roYPos;
                    }
                    break;
                }
            case RotateDir.Z:
                {
                    if (mb.canRotateZ)
                    {
                        transform.position = roZPos;
                    }
                    break;
                }
        }
    }

    void Stop()
    {
        gameObject.tag = "stone";
        enabled = false;
    }
    Vector3 FixedPos(Vector3 pos)
    {
        int a = (int)Mathf.Round(pos.x * 100); //小数点后两位前移，并四舍五入
        pos.x = a * 0.01f; //还原小数点后两位
        int b = (int)Mathf.Round(pos.y * 100); //小数点后两位前移，并四舍五入
        pos.y = b * 0.01f; //还原小数点后两位
        int c = (int)Mathf.Round(pos.z * 100); //小数点后两位前移，并四舍五入
        pos.z = c * 0.01f; //还原小数点后两位
        return pos;
    }
}
