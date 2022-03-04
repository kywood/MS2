using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : PlayerState<PlayerStateManager>
{
    public PlayerRun(PlayerStateManager state_manager)
        : base(state_manager)
    {
    }
    public override void OnEnter()
    {
        //Debug.Log("Run OnEnter");
    }

    public override void OnLeave()
    {
        GetPlayer().BubbleManager.GetComponent<BubbleManager>().SetVisible(false);

        //PlayerManager.Instance.DualAct((p) => p.BubbleManager.GetComponent<BubbleManager>().SetVisible(false));

    }
}
