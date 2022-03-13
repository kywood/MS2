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
        RESULT,

        MAX
    }   


    //private E_GAME_STATE mGameState = E_GAME_STATE.NONE;

    public GameStateManager()
    {
        mStateMap = new Dictionary<int, State<StateManager>>()
        {
            {(int)E_GAME_STATE.READY , new GameReady(this) }   ,
            {(int)E_GAME_STATE.SHOOT_READY , new GameShootReady(this) }   ,
            {(int)E_GAME_STATE.RUN , new GameRun(this) }   ,
            {(int)E_GAME_STATE.RUN_RESULT , new GameRunResult(this) }   ,
            {(int)E_GAME_STATE.END , new GameEnd(this) } ,
            {(int)E_GAME_STATE.RESULT , new GameStateResult(this) }
        };
    }
    public void SetGameState(E_GAME_STATE gameState , Action<State<StateManager>> act = null )
    {
        SetState((int)gameState, act);

    }



    public bool IsGameState(E_GAME_STATE game_state  )
    {
        return IsState((int)game_state);
    }
}
