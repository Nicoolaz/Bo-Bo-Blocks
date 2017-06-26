using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventMaster : MonoBehaviour {

    #region Events
    public delegate void ActionEventHandle();
    public event ActionEventHandle eventBundleLoaded;

    public event ActionEventHandle eventDeleteStones;

    public event ActionEventHandle eventCheckForDelete;

    public event ActionEventHandle eventGameStart;

    public event ActionEventHandle eventBlockStop;

    public event ActionEventHandle eventBlockSpeedUp;

    public event ActionEventHandle eventGameOver;

    public event ActionEventHandle eventScoreChanged;

    public event ActionEventHandle eventGamePause;

    public event ActionEventHandle eventChangeToGameStartScene;
    
    public event ActionEventHandle eventGameRestart;

    public delegate void BlockMoveEventHandle(Directions dir);
    public event BlockMoveEventHandle eventBlockMove;

    public delegate void BlockRotateEventHandle(RotateDir dir);
    public event BlockRotateEventHandle eventBlockRotate;

    #endregion

    #region CallEventsFunction
    public void CallEventCheckForDelete()
    {
        if (eventCheckForDelete != null)
        {
            eventCheckForDelete();
        }
    }

    public void CallEventDeleteStones()
    {
        if (eventDeleteStones != null)
        {
            eventDeleteStones();
        }
    }

    public void CallEventBundleLoaded()
    {
        if (eventBundleLoaded != null)
        {
            eventBundleLoaded();
        }
    }

    public void CallEventBlockMove(Directions dir)
    {
        if(eventBlockMove != null)
        {
            eventBlockMove(dir);
        }
    }

    public void CallEventGameStart()
    {
        if (eventGameStart != null)
        {
            eventGameStart();
        }
    }

    public void CallEventBlockRotate(RotateDir dir)
    {
        if (eventBlockRotate != null)
        {
            eventBlockRotate(dir);
        }
    }

    public void CallEventBlockStop()
    {
        if (eventBlockStop != null)
        {
            eventBlockStop();
        }
    }

    public void CallEventBlockSpeedUp()
    {
        if (eventBlockSpeedUp != null)
        {
            eventBlockSpeedUp();
        }
    }

    public void CallEventGameOver()
    {
        if (eventGameOver != null)
        {
            eventGameOver();
        }
    }

    public void CallEventScoreChanged()
    {
        if(eventScoreChanged != null)
        {
            eventScoreChanged();
        }
    }

    public void CallEventGamePause()
    {
        if (eventGamePause != null)
        {
            eventGamePause();
        }
    }

    public void CallEventGameRestart()
    {
        if (eventGameRestart != null)
        {
            eventGameRestart();
        }
    }

    public void CallEventChangeToGameStartScene()
    {
        if (eventChangeToGameStartScene != null)
        {
            eventChangeToGameStartScene();
        }
    }
    #endregion

}

