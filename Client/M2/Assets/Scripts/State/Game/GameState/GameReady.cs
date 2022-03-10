using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReady : State<StateManager>
{
    //PacketState packetState = PacketState.None;
    Player Player;

    PacketDequeStates packetDequeState = new PacketDequeStates();
    public GameReady(StateManager state_manager) : base(state_manager)
    {
    }

    public override void OnEnter()
    {
        PlayerManager.Instance.DualAct((p) => p.SetPlayerState(PlayerStateManager.E_PLAYER_STATE.PRE_READY));


        Player = PlayerManager.Instance.GetPlayer(PlayerManager.E_PLAYER_TYPE.MY_PLAYER);

        C_NextColsBubbleList nextColsBubblePacket = new C_NextColsBubbleList()
        {
            ColsCount = Defines.G_BUBBLE_START_ROW_COUNT
        };
        //packetState = PacketState.Sended;
        AppManager.Instance.NetworkManager.Send(nextColsBubblePacket);

        C_NextBubbles nextBubbles = new C_NextBubbles()
        {
            ReqCount = 5,
        };

        AppManager.Instance.NetworkManager.Send(nextBubbles);

        packetDequeState.Init();

        packetDequeState.AddWillRecvPacketId(MsgId.SNextColsBubbleList);
        packetDequeState.AddWillRecvPacketId(MsgId.SNextBubbles);
        packetDequeState.AddWillRecvPacketId(MsgId.SNextBubblesPeer);

    }

    public override void OnLeave()
    {
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if(packetDequeState.PacketState == PacketDequeStates.ePkState.Processing)
        {
            {
                NetPacket pk = ((OnlinePlayer)Player).PacketDeQueue(MsgId.SNextColsBubbleList);
                if (pk != null)
                {
                    S_NextColsBubbleList nextColsPacket = pk.Packet as S_NextColsBubbleList;
                    GameManager.Instance.SetColsBubbles(nextColsPacket.ColsBubbles);

                    packetDequeState.Complete(MsgId.SNextColsBubbleList);

                    //packetState = PacketState.None;
                    //PlayerManager.Instance.DualAct((p) => p.SetPlayerState(PlayerStateManager.E_PLAYER_STATE.READY));
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
            {
                NetPacket pk = ((OnlinePlayer)Player).PacketDeQueue(MsgId.SNextBubblesPeer);
                if (pk != null)
                {
                    S_NextBubblesPeer recvPacket = pk.Packet as S_NextBubblesPeer;

                    Player p = PlayerManager.Instance.GetPlayer(recvPacket.PlayerId);

                    ((ShootBubbleManager)p.GetBubbleManager()).EnqueueNextBubble(recvPacket.BubbleTypes);

                    packetDequeState.Complete(MsgId.SNextBubblesPeer);
                }
            }
        }

        if(packetDequeState.PacketState == PacketDequeStates.ePkState.Complete)
        {
            packetDequeState.Init();
            PlayerManager.Instance.DualAct((p) => p.SetPlayerState(PlayerStateManager.E_PLAYER_STATE.READY));
            GameManager.Instance.GetGameStateManager().SetGameState(GameStateManager.E_GAME_STATE.RUN);

            //PlayerManager.Instance.DualAct((p) => p.SetPlayerState(PlayerStateManager.E_PLAYER_STATE.READY));
        }

    }
}
