using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : StateManager
{
    // Start is called before the first frame update
    public enum E_GAME_STATE 
    {
        NONE  = -1, 
        READY,  // ready 
        SHOOT_READY ,
        RUN,
        RUN_RESULT,
        END,

        MAX
    }   


    //private E_GAME_STATE mGameState = E_GAME_STATE.NONE;

    public GameStateManager()
    {
        mStateMap = new Dictionary<int, State>()
        {
            {(int)E_GAME_STATE.READY , new Ready() }   ,
            {(int)E_GAME_STATE.SHOOT_READY , new ShootReady() }   ,
            {(int)E_GAME_STATE.RUN , new Run() }   ,
            {(int)E_GAME_STATE.RUN_RESULT , new RunResult() }   ,
            {(int)E_GAME_STATE.END , new End() }
        };
    }
    public void SetGameState(E_GAME_STATE gameState , Action<State> act = null )
    {
        SetState((int)gameState, act);

    }


    public E_GAME_STATE GetGameState()
    {
        return (E_GAME_STATE)GetState();
    }

    public State GetGameStateValue()
    {
        return GetStateValue();
    }

    public bool IsGameState(E_GAME_STATE game_state  )
    {
        return IsState((int)game_state);
    }
}
