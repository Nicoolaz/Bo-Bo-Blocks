using UnityEngine;
using System.Collections;
using System;

public class GameLoadingState : UIStateBase
{
    public GameLoadingState(Transform t) : base(t)
    {
    }

    public override void OnStateExit()
    {
        myTransform.Find("GameLoadingCanvas").gameObject.SetActive(false);
    }

    public override void OnStateStart()
    {
        myTransform.Find("GameLoadingCanvas").gameObject.SetActive(true);
    }
}
