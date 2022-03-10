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

    public PeerPlayerRunResult(PlayerStateManager state_manager) : base(state_manager)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        packetDequeState.Init();

        if (++runCnt % Defines.G_DROP_LOOP_TICK == 0)
        {
            packetDequeState.AddWillRecvPacketId(MsgId.SNextColsBubblePeer);
            //Debug.Log("Peer Send Next Bubble");
        }
        packetDequeState.AddWillRecvPacketId(MsgId.SNextBubblesPeer);
        // send packet state  ==>  none  send -> recv 
        //send
    }


    public override void OnUpdate()
    {
        base.OnUpdate();


        // TODO Packet Queue Observer 
        if (packetDequeState.PacketState == PacketDequeStates.ePkState.Processing)
        {
            {
                NetPacket pk = ((OnlinePlayer)Player).PacketDeQueue(MsgId.SNextColsBubblePeer);
                if (pk != null)
                {
                    //  Debug.Log("Peer nextbb dq com");
                    CSRotSlot csRotSlot = Player.RotSlot.GetComponent<CSRotSlot>();
                    S_NextColsBubblePeer packet = pk.Packet as S_NextColsBubblePeer;
                    csRotSlot.ActRotate(packet.BubbleTypes);
                    packetDequeState.Complete(MsgId.SNextColsBubblePeer);
                }
            }

            {
                NetPacket pk = ((OnlinePlayer)Player).PacketDeQueue(MsgId.SNextBubblesPeer);
                if (pk != null)
                {
                    S_NextBubblesPeer recvPacket = pk.Packet as S_NextBubblesPeer;
                    ((ShootBubbleManager)Player.GetBubbleManager()).EnqueueNextBubble(recvPacket.BubbleTypes);

                    packetDequeState.Complete(MsgId.SNextBubblesPeer);
                }
            }
        }

        if (packetDequeState.PacketState == PacketDequeStates.ePkState.Complete)
        {
            
            if (ResPools.Instance.IsStopAllBubble(GetPlayer().PlayerType))
            {
                Debug.Log("Peer SetPlayerState SHOOT_READY");
                GetPlayer().SetPlayerState(E_PLAYER_STATE.SHOOT_READY);
            }
        }

    }
}
