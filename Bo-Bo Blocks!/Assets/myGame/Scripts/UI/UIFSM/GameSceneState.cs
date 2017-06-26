using UnityEngine;
using System.Collections;
using System;

public class GameSceneState : UIStateBase {
    public GameSceneState(Transform t) : base(t)
    {
    }

    public override void OnStateExit()
    {
        myTransform.Find("GameSceneCanvas").gameObject.SetActive(false);
    }

    public override void OnStateStart()
    {
        myTransform.Find("GameSceneCanvas").gameObject.SetActive(true);
    }
}
