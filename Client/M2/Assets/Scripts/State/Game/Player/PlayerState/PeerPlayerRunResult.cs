using Google.Protobuf.Protocol;
using RotSlot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static PlayerManager;
using static PlayerStateManager;

public class PeerPlayerRunResult : PlayerRunResult
{

    PacketState packetState = PacketState.None;
    public PeerPlayerRunResult(PlayerStateManager state_manager) : base(state_manager)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        packetState = PacketState.None;

        if (++runCnt % Defines.G_DROP_LOOP_TICK == 0)
        {
            packetState = PacketState.Sended;

            Debug.Log("Peer Send Next Bubble");
        }
        // send packet state  ==>  none  send -> recv 
        //send
    }


    public override void OnUpdate()
    {
        base.OnUpdate();


        // TODO Packet Queue Observer 
        if( packetState == PacketState.Sended )
        {
            NetPacket pk = ((OnlinePlayer)Player).PacketDeQueue(MsgId.SNextBubblePeer);
            //Debug.Log("Peer nextbb dq try");
            if (pk != null)
            {
              //  Debug.Log("Peer nextbb dq com");
                CSRotSlot csRotSlot = Player.RotSlot.GetComponent<CSRotSlot>();

                S_NextBubblePeer packet = pk.Packet as S_NextBubblePeer;

                csRotSlot.ActRotate(packet.BubbleTypes);

                packetState = PacketState.None;
            }
        }

        if (packetState == PacketState.None)
        {
            
            if (ResPools.Instance.IsStopAllBubble(GetPlayer().PlayerType))
            {
                Debug.Log("Peer SetPlayerState SHOOT_READY");
                GetPlayer().SetPlayerState(E_PLAYER_STATE.SHOOT_READY);
            }
        }

    }
}
