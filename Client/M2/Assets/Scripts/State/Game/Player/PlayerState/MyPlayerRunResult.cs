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

    PacketState packetState = PacketState.None;
    public MyPlayerRunResult(PlayerStateManager state_manager) : base(state_manager)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        packetState = PacketState.None;

        if (++runCnt % Defines.G_DROP_LOOP_TICK == 0)
        {
            packetState = PacketState.Sended;
            // csRotSlot.ActRotate();
            C_NextBubble nextPacket = new C_NextBubble();
            AppManager.Instance.NetworkManager.Send(nextPacket);

            Debug.Log("Send Next Bubble");
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
            NetPacket pk = ((OnlinePlayer)Player).PacketDeQueue(MsgId.SNextBubble);
            //Debug.Log("nextbb dq try");
            if (pk != null)
            {
                //Debug.Log("nextbb dq com");
                CSRotSlot csRotSlot = Player.RotSlot.GetComponent<CSRotSlot>();

                S_NextBubble nextBubble = pk.Packet as S_NextBubble;

                csRotSlot.ActRotate(nextBubble.BubbleTypes);

                packetState = PacketState.None;
            }
        }

        if (packetState == PacketState.None)
        {
            
            if (ResPools.Instance.IsStopAllBubble(GetPlayer().PlayerType))
            {
                Debug.Log("SetPlayerState SHOOT_READY");
                GetPlayer().SetPlayerState(E_PLAYER_STATE.SHOOT_READY);
            }
        }

    }
}
