using UnityEngine;
using System.Collections;

public class TestGUI : MonoBehaviour {

	void OnGUI()
    {
        if(GUI.Button(new Rect(10, 10, 100, 40), "Left"))
        {
            GameMgr.Instance.eventMaster.CallEventBlockMove(Directions.Left);
        }
        if (GUI.Button(new Rect(10, 60, 100, 40), "Right"))
        {
            GameMgr.Instance.eventMaster.CallEventBlockMove(Directions.Right);
        }
        if (GUI.Button(new Rect(10, 110, 100, 40), "Forward"))
        {
            GameMgr.Instance.eventMaster.CallEventBlockMove(Directions.Forward);
        }
        if (GUI.Button(new Rect(10, 160, 100, 40), "Back"))
        {
            GameMgr.Instance.eventMaster.CallEventBlockMove(Directions.Back);
        }
        if (GUI.Button(new Rect(120, 10, 100, 40), "RoX"))
        {
            GameMgr.Instance.eventMaster.CallEventBlockRotate(RotateDir.X);
        }
        if (GUI.Button(new Rect(230, 10, 100, 40), "RoY"))
        {
            GameMgr.Instance.eventMaster.CallEventBlockRotate(RotateDir.Y);
        }
        if (GUI.Button(new Rect(340, 10, 100, 40), "RoZ"))
        {
            GameMgr.Instance.eventMaster.CallEventBlockRotate(RotateDir.Z);
        }
        if (!GameMgr.Instance.bl._allAbs.ContainsKey("p001"))
        {
            GUI.TextArea(new Rect(160, 160, 200, 60), "BundleLoadFaild");
        }
        else
        {
            GUI.TextArea(new Rect(160, 160, 200, 60), "BundleLoadDone");
        }
    }
}
