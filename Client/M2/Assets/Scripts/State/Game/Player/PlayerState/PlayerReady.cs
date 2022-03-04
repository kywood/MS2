using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStateManager;

public class PlayerReady : PlayerState<PlayerStateManager>
{
    public PlayerReady(PlayerStateManager state_manager) 
        : base(state_manager)
    {
    }

    public override void OnEnter()
    {
        GetPlayer().RotSlot.GetComponent<CSRotSlot>().InitRotSlot();

        GameManager.Instance.StartCoroutine(EffectStartRow());
    }

    IEnumerator EffectStartRow()
    {
        for (int i = 0; i < Defines.G_BUBBLE_START_ROW_COUNT; i++)
        {
            GetPlayer().RotSlot.GetComponent<CSRotSlot>().ActRotate();
            yield return new WaitForSeconds(0.2f);
        }

        GetPlayer().SetPlayerState(E_PLAYER_STATE.SHOOT_READY);
        //GameManager.Instance.GetGameStateManager().SetGameState(GameStateManager.E_GAME_STATE.SHOOT_READY);

        yield return null;
    }

    public override void OnLeave()
    {

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
