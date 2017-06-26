using UnityEngine;
using System.Collections;
public enum Directions
{
    Left,
    Right,
    Forward,
    Back
}
public enum RotateDir
{
    X,
    Y,
    Z
}
public class MyBlocks : MonoBehaviour
{

    public Vector3 dirLeft
    {
        private set;
        get;
    }
    public Vector3 dirRight
    {
        private set;
        get;
    }
    public Vector3 dirForward
    {
        private set;
        get;
    }
    public Vector3 dirBack
    {
        private set;
        get;
    }
    private bool canLeft;
    private bool canRight;
    private bool canForward;
    private bool canBack;
    private bool canDown;
    public bool canRotateX { private set; get; }
    public bool canRotateY { private set; get; }
    public bool canRotateZ { private set; get; }

    private SubBlocks[] subBlocks;
    bool stoping = false;
    public float downRate
    {
        set; get;
    }
    private float timer = 0;
    void Awake()
    {
        subBlocks = new SubBlocks[transform.childCount];
        for (int i = 0; i < transform.childCount; ++i)
        {
            subBlocks[i] = transform.GetChild(i).GetComponent<SubBlocks>();
        }
        downRate = 1.5f;
    }

    void OnEnable()
    {
        GameMgr.Instance.eventMaster.eventBlockMove += Move;
        GameMgr.Instance.eventMaster.eventBlockRotate += RotateSubs;
        GameMgr.Instance.eventMaster.eventBlockStop += Stop;
        GameMgr.Instance.eventMaster.eventBlockSpeedUp += SpeedUp;
    }

    void OnDisable()
    {
        GameMgr.Instance.eventMaster.eventBlockMove -= Move;
        GameMgr.Instance.eventMaster.eventBlockRotate -= RotateSubs;
        GameMgr.Instance.eventMaster.eventBlockStop -= Stop;
        GameMgr.Instance.eventMaster.eventBlockSpeedUp -= SpeedUp;
    }
    void FixedUpdate()
    {
        CalculateCurrentDir();
        CheckMoveable();
    }
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= downRate && canDown)
        {
            transform.position += Vector3.down * 5;
            timer = 0;
        }
        if (!canDown && !stoping)
        {
            StartCoroutine("Stopping");
        }
    }

    void CalculateCurrentDir()
    {
        Vector2 camPos = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.z);
        camPos = camPos.normalized;
        float angle = Vector2.Angle(camPos, new Vector2(0, -1));
        if (camPos.x <= 0)
        {
            angle = -angle;
        }
        if (-45 <= angle && 45 >= angle)
        {
            dirLeft = Vector3.left;
            dirRight = Vector3.right;
            dirForward = Vector3.forward;
            dirBack = Vector3.back;
        }
        else if (-135 <= angle && angle < -45)
        {
            dirLeft = Vector3.forward;
            dirRight = Vector3.back;
            dirForward = Vector3.right;
            dirBack = Vector3.left;
        }
        else if (45 < angle && 135 >= angle)
        {
            dirLeft = Vector3.back;
            dirRight = Vector3.forward;
            dirForward = Vector3.left;
            dirBack = Vector3.right;
        }
        else
        {
            dirLeft = Vector3.right;
            dirRight = Vector3.left;
            dirForward = Vector3.back;
            dirBack = Vector3.forward;
        }
        //Debug.Log("dirLeft :" + dirLeft + " dirRight : " + dirRight + " dirForward : " + dirForward + " dirBack : " + dirBack) ;
    }

    int leftSign = 1;
    int rightSign = 1;
    int forwardSign = 1;
    int backSign = 1;
    int downSign = 1;
    int roXsign = 1;
    int roYsign = 1;
    int roZsign = 1;
    void CheckMoveable()
    {
        leftSign = 1;
        rightSign = 1;
        forwardSign = 1;
        backSign = 1;
        downSign = 1;
        for (int i = 0; i < subBlocks.Length; ++i)
        {
            leftSign *= subBlocks[i].leftAble;
            rightSign *= subBlocks[i].rightAble;
            forwardSign *= subBlocks[i].forwardAble;
            backSign *= subBlocks[i].backAble;
            downSign *= subBlocks[i].downAble;
        }
        canLeft = leftSign == 0 ? false : true;
        canRight = rightSign == 0 ? false : true;
        canForward = forwardSign == 0 ? false : true;
        canBack = backSign == 0 ? false : true;
        canDown = downSign == 0 ? false : true;
    }
    void CheckRotateable()
    {
        roXsign = 1;
        roYsign = 1;
        roZsign = 1;
        for (int i = 0; i < subBlocks.Length; ++i)
        {
            roXsign *= subBlocks[i].roXable;
            roYsign *= subBlocks[i].roYable;
            roZsign *= subBlocks[i].roZable;
        }
        canRotateX = roXsign == 1 ? true : false;
        canRotateY = roYsign == 1 ? true : false;
        canRotateZ = roZsign == 1 ? true : false;
    }

    void Move(Directions dir)
    {
        
        switch (dir)
        {
            case Directions.Left:
                {
                    if (canLeft)
                    {
                        transform.position += dirLeft * 5;
                    }
                    break;
                }
            case Directions.Right:
                {
                    if (canRight)
                    {
                        transform.position += dirRight * 5;
                    }
                    break;
                }
            case Directions.Forward:
                {
                    if (canForward)
                    {
                        transform.position += dirForward * 5;
                    }
                    break;
                }
            case Directions.Back:
                {
                    if (canBack)
                    {
                        transform.position += dirBack * 5;
                    }
                    break;
                }
        }
    }

    void RotateSubs(RotateDir dir)
    {
        CheckRotateable();
        for (int i = 0; i < subBlocks.Length; ++i)
        {
            subBlocks[i].Rotate_m(dir);
        }
    }

    void SpeedUp()
    {
        downRate = 0.1f;
    }

    IEnumerator Stopping()
    {
        stoping = true;
        yield return new WaitForSeconds(0.5f);
        if (!canDown)
        {
            GameMgr.Instance.eventMaster.CallEventBlockStop();
        }
        else
        {
            stoping = false;
        }
    }
    void Stop()
    {
        this.enabled = false;
    }
    public bool CheckBlocks()
    {
        for (int i = 0; i < subBlocks.Length; ++i)
        {
            if (GameMgr.Instance.gridMgr.stonesDic.ContainsKey(subBlocks[i].transform.position))
            {
                return true;
            }
        }
        return false;
    }
}
