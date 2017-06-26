using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UIStateMachine {
    private Dictionary<System.Type, UIStateBase> StateDic = new Dictionary<System.Type, UIStateBase>();
    private UIStateBase curState;
    public void AddState(UIStateBase state)
    {
        if (!StateDic.ContainsKey(state.GetType()))
        {
            StateDic.Add(state.GetType(), state);
        }
        if (StateDic.Values.Count == 1)
        {
            curState = state;
        }
    }
    public void DeleteState(UIStateBase state)
    {
        if (StateDic.ContainsKey(state.GetType()))
        {
            StateDic.Remove(state.GetType());
        }
        else
        {
            Debug.Log("The state you want to remove does not exist!");
        }
    }

    public void ChangeState(System.Type type)
    {
        curState.OnStateExit();
        if (StateDic.ContainsKey(type))
        {
            if (curState.GetType() == type)
            {
                curState.OnStateStart();
            }
            else
            {
                curState = StateDic[type];
                curState.OnStateStart();
            }
        }
        else
        {
            Debug.Log("The state you want to change does not exist!");
        }
    }

    //public void Update()
    //{
    //    curState.OnStateUpdate();
    //}

}
