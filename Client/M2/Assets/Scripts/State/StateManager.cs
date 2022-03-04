using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager
{
    protected Dictionary<int, State<StateManager>> mStateMap;// = new Dictionary<int, State>();


    public static readonly int NONE = -1;

    protected int mState = -1;

    public void SetState( int state , Action<State<StateManager>> act = null )
    {
        if (mState == state)
            return;

        if (mState == NONE)     
        {
            mState = state;
            mStateMap[state].OnEnter(act);
        }
        else
        {
            mStateMap[mState].OnLeave();
            mState = state;
            mStateMap[state].OnEnter(act);
        }
    }



    //public void SetGameState(E_GAME_STATE  gameState )
    //{
    //    if (mGameState == gameState)
    //        return;

    //    if (mGameState == E_GAME_STATE.NONE)
    //    {
    //        mGameState = gameState;
    //        mStateMap[gameState].OnEnter();
    //    }
    //    else
    //    {
    //        mStateMap[mGameState].OnLeave();
    //        mGameState = gameState;
    //        mStateMap[gameState].OnEnter();
    //    }
    //}

    public int GetState()
    {
        return mState;
    }

    public State<StateManager> GetStateValue()
    {
        return mStateMap[mState];
    }

    public bool IsState(int game_state  )
    {
        if (mState == game_state)
            return true;

        return false;
    }

    public void OnUpdate()
    {
        if (mState == NONE)
            return;

        mStateMap[mState].OnUpdate();
    }

}
