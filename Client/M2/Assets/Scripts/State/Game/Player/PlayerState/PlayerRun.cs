using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : PlayerState<PlayerStateManager>
{
    protected Player Player;

    protected PacketDequeStates packetDequeState = new PacketDequeStates();

    public PlayerRun(PlayerStateManager state_manager)
        : base(state_manager)
    {
    }
    public override void OnEnter()
    {
        //Debug.Log("Run OnEnter");
        base.OnEnter();
        Player = GetPlayer();
        packetDequeState.Init();

    }

    public override void OnLeave()
    {
        base.OnLeave();
        GetPlayer().BubbleManager.GetComponent<BubbleManager>().SetVisible(false);
        //PlayerManager.Instance.DualAct((p) => p.BubbleManager.GetComponent<BubbleManager>().SetVisible(false));

    }

    public override void OnUpdate()
    {
        base.OnUpdate();


    }
}
