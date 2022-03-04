using RotSlot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static PlayerManager;
using static PlayerStateManager;

public class PlayerRunResult : PlayerState<PlayerStateManager>
{
    // Start is called before the first frame update
    //float timer;
    //int waitingTime;

    CSSlot mCsSlot;


    static int runCnt = 0;

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
            //Debug.Log(" =======  out_drop BEGIN ============== ");
            //foreach (cBubble bb in out_drop)
            //{
            //    Debug.Log(bb.ToString());
            //}
            //Debug.Log(" =======  out_drop END ============== ");

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
        //Debug.Log("Run OnEnter");
        //AppManager.Instance.BubbleManager.GetComponent<BubbleManager>().SetVisible(true);
        //timer = 0.0f;
        //waitingTime = 2;


        List<cBubble> out_pang = new List<cBubble>();
        List<cBubble> out_drop = new List<cBubble>();

        mCsSlot.Pang(out_pang, out_drop);

        PangAct(out_pang);
        DropAct(out_drop);

        //TODO

        Player p = GetPlayer();

        CSRotSlot csRotSlot = p.RotSlot.GetComponent<CSRotSlot>();
        if (++runCnt % Defines.G_DROP_LOOP_TICK == 0)
            csRotSlot.ActRotate();



        //PlayerManager.Instance.DualAct((p) => {

        //    CSRotSlot csRotSlot = p.RotSlot.GetComponent<CSRotSlot>();
        //    if (++runCnt % Defines.G_DROP_LOOP_TICK == 0)
        //        csRotSlot.ActRotate();
        //});


    }

    public override void OnLeave()
    {
        //AppManager.Instance.BubbleManager.GetComponent<BubbleManager>().SetVisible(false);
        //Debug.Log("Run OnLeave");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //Thread.Sleep(1 * 1000);

        //if (mCsSlot == null)
        //    Debug.Log("==");
        //else
        //    Debug.Log(mCsSlot);


        if (ResPools.Instance.IsStopAllBubble(GetPlayer().PlayerType))
        {
            GetPlayer().SetPlayerState(E_PLAYER_STATE.SHOOT_READY);
        
        }

        //Pool pool = ResPools.Instance.GetPool(MDefine.eResType.Bubble);
        //foreach (int k in pool.ResList.Keys)
        //{
        //    if (pool.ResList[k].activeSelf == false)
        //        continue;
        //    (pool.ResList[k].GetComponent<CSBubble>()).OnUpdate();
        //}

        //timer += Time.deltaTime;
        //if (timer > waitingTime)
        //{
        //    //Action
        //    AppManager.Instance.GetStateManager().SetGameState(StateManager.E_GAME_STATE.SHOOT_READY);
        //    timer = 0;
        //}



    }
}
