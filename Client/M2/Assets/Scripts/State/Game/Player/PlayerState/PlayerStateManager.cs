using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : StateManager
{
    public enum E_PLAYER_STATE
    {
        NONE = -1,
        READY,  // ready 
        SHOOT_READY,
        RUN,
        RUN_RESULT,
        END,

        MAX
    }

    Player _player;

    public Player Player { get { return _player; } }

    public PlayerStateManager(Player player )
    {
        _player = player;

        mStateMap = new Dictionary<int, State<StateManager>>()
        {
            {(int)E_PLAYER_STATE.READY , new PlayerReady(this) }   ,
            {(int)E_PLAYER_STATE.SHOOT_READY , new PlayerShootReady(this) }   ,
            {(int)E_PLAYER_STATE.RUN , new PlayerRun(this) }   ,
            {(int)E_PLAYER_STATE.RUN_RESULT , new PlayerRunResult(this) }   ,
            {(int)E_PLAYER_STATE.END , new PlayerEnd(this) }
        };
    }

}
