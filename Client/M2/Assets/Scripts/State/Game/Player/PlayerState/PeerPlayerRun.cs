using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStateManager;

public class PeerPlayerRun : PlayerRun
{
    public PeerPlayerRun(PlayerStateManager state_manager)
        : base(state_manager)
    {
    }
    public override void OnEnter()
    {
        base.OnEnter();

        packetDequeState.AddWillRecvPacketId(MsgId.SFixedBubbleSlotPeer);
    }

    

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (packetDequeState.PacketState == PacketDequeStates.ePkState.Processing)
        {
            {
                NetPacket pk = ((OnlinePlayer)Player).PacketDeQueue(MsgId.SFixedBubbleSlotPeer);
                if (pk != null)
                {

                    S_FixedBubbleSlotPeer packet = pk.Packet as S_FixedBubbleSlotPeer;

                    BubbleManager bubbleManager = Player.GetBubbleManager();
                    bubbleManager.SetVisible(false);
                    ShootBubble bubble = ((ShootBubbleManager)bubbleManager).GetBubble();


                    CSSlot finalCsSlot = Player.GetRotSlot()
                        .GetComponent<CSRotSlot>()
                        .GetCsSlot(
                            packet.ColsSlotId , packet.SlotId
                        );

                    CSRotSlot.SetCsBubbleInCsSlot(GetPlayer(), finalCsSlot, bubble.GetBubbleType());

                    packetDequeState.Complete(MsgId.SFixedBubbleSlotPeer);

                    Player.SetPlayerState(E_PLAYER_STATE.RUN_RESULT,
                    (state) =>
                    {
                        ((PlayerRunResult)state).SetCsSlot(finalCsSlot);
                    });
                }
            }
        }
    }
}
