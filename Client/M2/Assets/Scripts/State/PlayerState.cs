using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerState<T> : State<StateManager>
{

    protected PacketDequeStates packetDequeState = new PacketDequeStates();


    public PlayerState(PlayerStateManager state_manager) 
        : base(state_manager)
    {
        
    }

    protected Player GetPlayer()
    {
        return ((PlayerStateManager)stateManager).Player;
    }

    public override  void OnEnter()
    {

    }

    public override void OnLeave()
    {

    }

    public override void OnUpdate()
    {
        

    }


}
