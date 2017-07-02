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

    public event ActionEventHandle eventPlayerDataChanged;

    public event ActionEventHandle eventToggleShopCanvas;

    public event ActionEventHandle eventToggleChoseBlockPanel;

    public event ActionEventHandle eventNextBlockChanged;

    public event ActionEventHandle eventToggleRankCanvas;

    public delegate void BlockMoveEventHandle(Directions dir);
    public event BlockMoveEventHandle eventBlockMove;

    public delegate void BlockRotateEventHandle(RotateDir dir);
    public event BlockRotateEventHandle eventBlockRotate;

    public delegate void PlayerItemChangeEventHandle(int id, int num);
    public event PlayerItemChangeEventHandle eventPlayerItemChange;

    public delegate void PlayerCoinsChangeEventHandle(int num);
    public event PlayerCoinsChangeEventHandle eventPlayerCoinsChange;

    public delegate void PlayerUseItemEventHandle(int id);
    public event PlayerUseItemEventHandle eventPlayerUseItem;

    public delegate void ChoseNextBlockEventHandle(int id);
    public event ChoseNextBlockEventHandle eventChoseNextBlock;


    #endregion

    #region CallEventsFunction

    public void CallEventToggleRankCanvas()
    {
        if (eventToggleRankCanvas != null)
        {
            eventToggleRankCanvas();
        }
    }

    public void CallEventNextBlockChanged()
    {
        if (eventNextBlockChanged != null)
        {
            eventNextBlockChanged();
        }
    }

    public void CallEventChoseNextBlock(int id)
    {
        if (eventChoseNextBlock != null)
        {
            eventChoseNextBlock(id);
        }
    }

    public void CallEventToggleChoseBlockPanel()
    {
        if (eventToggleChoseBlockPanel != null)
        {
            eventToggleChoseBlockPanel();
        }
    }

    public void CallEventToggleShopCanvas()
    {
        if (eventToggleShopCanvas != null)
        {
            eventToggleShopCanvas();
        }
    }

    public void CallEventPlayerCoinsChange(int num)
    {
        if (eventPlayerCoinsChange != null)
        {
            eventPlayerCoinsChange(num);
        }
    }

    public void CallEventPlayerItemChange(int id,int num)
    {
        if (eventPlayerItemChange != null)
        {
            eventPlayerItemChange(id, num);
        }
    }

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

    public void CallEventPlayerDataChanged()
    {
        if (eventPlayerDataChanged != null)
        {
            eventPlayerDataChanged();
        }
    }

    public void CallEventPlayerUseItem(int id)
    {
        if (eventPlayerUseItem != null)
        {
            eventPlayerUseItem(id);
        }
    }
    #endregion

}

