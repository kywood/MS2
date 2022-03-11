using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : StateManager
{
    public enum E_PLAYER_STATE
    {
        NONE = -1,
        PRE_READY,  // ready 
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

        if(player.PlayerType == PlayerManager.E_PLAYER_TYPE.MY_PLAYER)
        {
            mStateMap = new Dictionary<int, State<StateManager>>()
        {
            {(int)E_PLAYER_STATE.PRE_READY , new PlayerPreReady(this) }   ,
            {(int)E_PLAYER_STATE.READY , new PlayerReady(this) }   ,
            {(int)E_PLAYER_STATE.SHOOT_READY , new MyPlayerShootReady(this) }   ,
            {(int)E_PLAYER_STATE.RUN , new PlayerRun(this) }   ,
            {(int)E_PLAYER_STATE.RUN_RESULT , new MyPlayerRunResult(this) }   ,
            {(int)E_PLAYER_STATE.END , new PlayerEnd(this) }
        };
        }
        else
        {
            mStateMap = new Dictionary<int, State<StateManager>>()
        {
            {(int)E_PLAYER_STATE.PRE_READY , new PlayerPreReady(this) }   ,
            {(int)E_PLAYER_STATE.READY , new PlayerReady(this) }   ,
            {(int)E_PLAYER_STATE.SHOOT_READY , new PeerPlayerShootReady(this) }   ,
            {(int)E_PLAYER_STATE.RUN , new PeerPlayerRun(this) }   ,
            {(int)E_PLAYER_STATE.RUN_RESULT , new PeerPlayerRunResult(this) }   ,
            {(int)E_PLAYER_STATE.END , new PlayerEnd(this) }
        };
        }

        


    }

}
