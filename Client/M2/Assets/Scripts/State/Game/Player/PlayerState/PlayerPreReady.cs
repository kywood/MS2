using Google.Protobuf.Collections;
using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static PlayerStateManager;

public class PlayerPreReady : PlayerState<PlayerStateManager>
{

    public PlayerPreReady(PlayerStateManager state_manager) 
        : base(state_manager)
    {
        //int a = 10;
    }

    public override void OnEnter()
    {
        //int a = 100;
        
    }


    public override void OnUpdate()
    {
        base.OnUpdate();

    }
}
