using Google.Protobuf.Collections;
using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static PlayerStateManager;

public class PlayerReady : PlayerState<PlayerStateManager>
{
    //PacketState packetState = PacketState.None;
    Player Player;

    Queue<IEnumerator> _coRoutines = new Queue<IEnumerator>();

    public PlayerReady(PlayerStateManager state_manager) 
        : base(state_manager)
    {
    }

    public override void OnEnter()
    {

        Player = GetPlayer();
        Player.RotSlot.GetComponent<CSRotSlot>().InitRotSlot();

        //C_NextColsBubble nextColsBubblePacket = new C_NextColsBubble()
        //{
        //    ColsCount = Defines.G_BUBBLE_START_ROW_COUNT
        //};

        ////packetState = PacketState.Sended;
        //AppM anager.Instance.NetworkManager.Send(nextColsBubblePacket);

        GameManager.Instance.StartCoroutine(EffectStartRow());

        Player.SetPlayerState(E_PLAYER_STATE.SHOOT_READY);

        Player.SetVisiblePick(true);
    }

    IEnumerator EffectStartRow()
    {
        foreach(ColsBubbles colsBubbles in  GameManager.Instance.ColsBubbles)
        {
            yield return new WaitForSeconds(0.2f);
            Player.RotSlot.GetComponent<CSRotSlot>().ActRotate(colsBubbles.BubbleTypes);
        }

        yield return null;
    }

    //IEnumerator NextState()
    //{
    //    Player.SetPlayerState(E_PLAYER_STATE.SHOOT_READY);
    //    yield return null;
    //}


    public override void OnUpdate()
    {
        base.OnUpdate();

        //if (packetState == PacketState.Sended)
        //{
        //    NetPacket pk = ((OnlinePlayer)Player).PacketDeQueue(MsgId.SNextColsBubble);
        //    //Debug.Log("PR nextbb dq try");
        //    if (pk != null)
        //    {
        //        //Debug.Log("PR nextbb dq com");

        //        S_NextColsBubble nextColsPacket = pk.Packet as S_NextColsBubble;

        //        foreach(ColsBubbles colsBubble in nextColsPacket.ColsBubbles )
        //        {

        //            _coRoutines.Enqueue(EffectStartRow(colsBubble.BubbleTypes));

        //            //GameManager.Instance.StartCoroutine(EffectStartRow(colsBubble.BubbleTypes));

        //            //CSRotSlot csRotSlot = Player.RotSlot.GetComponent<CSRotSlot>();
        //            //csRotSlot.ActRotate(colsBubble.BubbleTypes);

        //            //Thread.Sleep(500);
        //        }

        //        packetState = PacketState.None;
        //        _coRoutines.Enqueue(NextState());
        //        //GameManager.Instance.StartCoroutine(NextState());
        //        //Player.SetPlayerState(E_PLAYER_STATE.SHOOT_READY);
        //    }

        //    GameManager.Instance.StartCoroutine(_coRoutines.Dequeue());
        //}
    }
}
