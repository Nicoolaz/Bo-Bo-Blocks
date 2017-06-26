using UnityEngine;
using System.Collections;
using System;

public class GameStartState : UIStateBase {

    public GameStartState(Transform t) : base(t)
    {
    }

    public override void OnStateExit()
    {
        
        myTransform.Find("GameStartSceneCanvas").gameObject.SetActive(false);
    }

    public override void OnStateStart()
    {
        myTransform.Find("GameStartSceneCanvas").gameObject.SetActive(true);
    }

    //public override void OnStateUpdate()
    //{
        
    //}

}
