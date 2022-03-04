using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReady : State<StateManager>
{
    public GameReady(StateManager state_manager) : base(state_manager)
    {
    }

    public override void OnEnter()
    {
        PlayerManager.Instance.DualAct((p) => p.SetPlayerState(PlayerStateManager.E_PLAYER_STATE.READY)); 
    }

    public override void OnLeave()
    {
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}