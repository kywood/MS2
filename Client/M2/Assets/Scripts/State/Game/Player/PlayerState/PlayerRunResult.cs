using Google.Protobuf.Protocol;
using RotSlot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static PlayerManager;
using static PlayerStateManager;

public abstract class PlayerRunResult : PlayerState<PlayerStateManager>
{
    CSSlot mCsSlot;


    protected static int runCnt = 0;
    protected Player Player;

    

    public PlayerRunResult(PlayerStateManager state_manager) : base(state_manager)
    {
    }

    void PangAct(List<cBubble> out_pang)
    {
        
        if (out_pang.Count > 0)
        {
            //Debug.Log(" =======  out_pang BEGIN ============== ");
            //foreach (cBubble bb in out_pang)
            //{
            //    Debug.Log(bb.ToString());
            //}
            //Debug.Log(" =======  out_pang END ============== ");

            Pool pool = ResPools.Instance.GetPool(GetPlayer().PlayerType, MDefine.eResType.Bubble);

            foreach (int k in pool.ResList.Keys)
            {
                if (pool.ResList[k].activeSelf == false)
                    continue;

                CSBubble csBubble = pool.ResList[k].GetComponent<CSBubble>();

                foreach (cBubble bb in out_pang)
                {
                    if (csBubble.IsEqBubble(bb))
                    {
                        csBubble.PangAct();

                    }
                    //Debug.Log(bb.ToString());
                }
            }
        }
    }

    void DropAct(List<cBubble> out_drop)
    {
        if (out_drop.Count > 0)
        {

            Pool pool = ResPools.Instance.GetPool(GetPlayer().PlayerType, MDefine.eResType.Bubble);

            foreach (int k in pool.ResList.Keys)
            {
                if (pool.ResList[k].activeSelf == false)
                    continue;

                CSBubble csBubble = pool.ResList[k].GetComponent<CSBubble>();

                foreach (cBubble bb in out_drop)
                {
                    if (csBubble.IsEqBubble(bb))
                    {
                        (pool.ResList[k].GetComponent<CSBubble>()).SetMoving();
                        //pool.ResList[k].transform.position.y--;
                    }
                    //Debug.Log(bb.ToString());
                }
            }
        }
    }
    public void SetCsSlot(CSSlot csslot)
    {
        mCsSlot = csslot;
    }
    public override void OnEnter()
    {
        
        Player = GetPlayer();

        List<cBubble> out_pang = new List<cBubble>();
        List<cBubble> out_drop = new List<cBubble>();

        mCsSlot.Pang(out_pang, out_drop);

        PangAct(out_pang);
        DropAct(out_drop);

        
        packetDequeState.Init();

        //packetDequeState.AddWillRecvPacketId(MsgId.SNextBubbles);
        //packetDequeState.AddWillRecvPacketId(MsgId.SNextBubblesPeer);


    }


    public override void OnUpdate()
    {
        base.OnUpdate();



    }
}
