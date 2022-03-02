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

    public PlayerStateManager()
    {
        mStateMap = new Dictionary<int, State>()
        {

        };
    }

}
