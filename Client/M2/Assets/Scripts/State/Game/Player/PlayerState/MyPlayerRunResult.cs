using Google.Protobuf.Protocol;
using RotSlot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static PlayerManager;
using static PlayerStateManager;

public class MyPlayerRunResult : PlayerRunResult
{

    //PacketState packetState = PacketState.None;
    public MyPlayerRunResult(PlayerStateManager state_manager) : base(state_manager)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        packetDequeState.Init();

        //packetState = PacketState.None;

        if (++runCnt % Defines.G_DROP_LOOP_TICK == 0)
        {
            //packetState = PacketState.Sended;
            // csRotSlot.ActRotate();
            C_NextColsBubble nextPacket = new C_NextColsBubble();
            AppManager.Instance.NetworkManager.Send(nextPacket);

            packetDequeState.AddWillRecvPacketId(MsgId.SNextColsBubble);

            //Debug.Log("Send Next Bubble");
        }

        C_NextBubbles nextBubbles = new C_NextBubbles()
        {
            ReqCount = 1,
        };

        AppManager.Instance.NetworkManager.Send(nextBubbles);

        
        packetDequeState.AddWillRecvPacketId(MsgId.SNextBubbles);
        // send packet state  ==>  none  send -> recv 
        //send
    }


    public override void OnUpdate()
    {
        base.OnUpdate();


        if(packetDequeState.PacketState == PacketDequeStates.ePkState.Processing)

        // TODO Packet Queue Observer 
        //if( packetState == PacketState.Sended )
        {
            {
                NetPacket pk = ((OnlinePlayer)Player).PacketDeQueue(MsgId.SNextColsBubble);
                if (pk != null)
                {
                    CSRotSlot csRotSlot = Player.RotSlot.GetComponent<CSRotSlot>();
                    S_NextColsBubble nextBubble = pk.Packet as S_NextColsBubble;

                    csRotSlot.ActRotate(nextBubble.BubbleTypes);

                    packetDequeState.Complete(MsgId.SNextColsBubble);
                }
            }

            {
                NetPacket pk = ((OnlinePlayer)Player).PacketDeQueue(MsgId.SNextBubbles);
                if (pk != null)
                {
                    S_NextBubbles recvPacket = pk.Packet as S_NextBubbles;
                    ((ShootBubbleManager)Player.GetBubbleManager()).EnqueueNextBubble(recvPacket.BubbleTypes);

                    packetDequeState.Complete(MsgId.SNextBubbles);
                }
            }
            
        }

        if (packetDequeState.PacketState == PacketDequeStates.ePkState.Complete)
        {
            
            if (ResPools.Instance.IsStopAllBubble(GetPlayer().PlayerType))
            {
                Debug.Log("SetPlayerState SHOOT_READY");
                GetPlayer().SetPlayerState(E_PLAYER_STATE.SHOOT_READY);
            }
        }

    }
}
