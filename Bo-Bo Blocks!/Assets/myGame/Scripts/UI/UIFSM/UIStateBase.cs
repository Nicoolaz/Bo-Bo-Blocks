using UnityEngine;
using System.Collections;

public abstract class UIStateBase {
    protected Transform myTransform;
    public UIStateBase(Transform t)
    {
        myTransform = t;
    }

    public abstract void OnStateStart();
    //public abstract void OnStateUpdate();
    public abstract void OnStateExit();

}
