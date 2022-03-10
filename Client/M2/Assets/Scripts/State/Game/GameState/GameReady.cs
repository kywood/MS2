using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReady : State<StateManager>
{
    PacketState packetState = PacketState.None;
    Player Player;
    public GameReady(StateManager state_manager) : base(state_manager)
    {
    }

    public override void OnEnter()
    {
        PlayerManager.Instance.DualAct((p) => p.SetPlayerState(PlayerStateManager.E_PLAYER_STATE.PRE_READY));


        Player = PlayerManager.Instance.GetPlayer(PlayerManager.E_PLAYER_TYPE.MY_PLAYER);

        C_NextColsBubble nextColsBubblePacket = new C_NextColsBubble()
        {
            ColsCount = Defines.G_BUBBLE_START_ROW_COUNT
        };
        packetState = PacketState.Sended;
        AppManager.Instance.NetworkManager.Send(nextColsBubblePacket);
    }

    public override void OnLeave()
    {
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (packetState == PacketState.Sended)
        {
            NetPacket pk = ((OnlinePlayer)Player).PacketDeQueue(MsgId.SNextColsBubble);
            if (pk != null)
            {
                S_NextColsBubble nextColsPacket = pk.Packet as S_NextColsBubble;

                GameManager.Instance.SetColsBubbles(nextColsPacket.ColsBubbles);

                //foreach (ColsBubbles colsBubble in nextColsPacket.ColsBubbles)
                //{
                //    //colsBubble.BubbleTypes;

                //    // 각각 플레이어에 아니면 매니저에 어디든 저장 해서 스테이트 넘어 간다.
                //}
                packetState = PacketState.None;
                PlayerManager.Instance.DualAct((p) => p.SetPlayerState(PlayerStateManager.E_PLAYER_STATE.READY));
            }
            
        }

    }
}
