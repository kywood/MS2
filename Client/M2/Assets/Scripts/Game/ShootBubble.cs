using Google.Protobuf.Protocol;
using RotSlot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ConstData;
using static Defines;
using static Player;
using static PlayerManager;
using static PlayerStateManager;

public class ShootBubble : Bubble
{
    private E_BUBBLE_TYPE mBubbleType = E_BUBBLE_TYPE.NONE;

    public E_BUBBLE_TYPE GetBubbleType()
    {
        return mBubbleType;
    }

    public void SetBubbleType( E_BUBBLE_TYPE bubble_type )
    {
        mBubbleType = bubble_type;

        SpriteRenderer sp = GetComponent<SpriteRenderer>();

        sp.sprite = ((ShootBubbleManager)GetBubbleManager()).GetSprite(bubble_type);

        //Debug.Log(bubble_type);
        //Debug.Log(c.ToString());


    }
        
    public void SetVisible(bool visible)
    {
        base.SetVisible(visible);

        if(visible == true )
        {
            Pick pick = GetPlayer().Pick.GetComponent<Pick>();

            transform.position = pick.ShootBody.transform.position;

            SetBubbleType( ((ShootBubbleManager)GetBubbleManager()).NextPop());
        }
    }


    private CSSlot FindNearPos(List<CSSlot> csslots)
    {

        csslots.Sort((cs1, cs2) =>
        {
            float dis_c1= Vector3.Distance(transform.position, cs1.transform.position);
            float dis_c2 = Vector3.Distance(transform.position, cs2.transform.position);

            //if (dis_c1 > dis_c2)
            if (dis_c1 < dis_c2)
                return -1;

            return 1;

        });
        return csslots[0];

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {     
        if (Player.StateManager.IsState((int)E_PLAYER_STATE.RUN) == false)
        {
            return;
        }

        if (collision.name.CompareTo(ConstData.GetPreFabProperty(E_PREFAB_TYPE.SLOT).mNM ) != 0 )
        {
            return;
        }

        CSSlot csSlot = collision.gameObject.GetComponent<CSSlot>();

        cPoint<int> stay_idx = new cPoint<int>();

        cPoint<int> out_top_stay_pos_idx = new cPoint<int>();
        List<cPoint<int>> out_stay_pos_idx_list = new List<cPoint<int>>();



        if (csSlot.GetcBubbleSlot().FindStaySlot(
                new cPoint<int>( csSlot.GetcSlot().GetID() , csSlot.GetcSlot().GetParentID()
            ),
            out_top_stay_pos_idx,
            out_stay_pos_idx_list
            ) == true)
        {

            CSRotSlot csRotSlot = GetPlayer().GetRotSlot().GetComponent<CSRotSlot>();
            cBubbleSlot bubbleSlot = csRotSlot.GetBubbleSlot();
            CSSlot finalCsSlot;

            if (out_stay_pos_idx_list.Count <= 0)
            {
                stay_idx = out_top_stay_pos_idx;
                cSlot<cBubble> cSlotTmp = bubbleSlot.GetSlotByIDX(out_top_stay_pos_idx);

                finalCsSlot = csRotSlot.GetCsSlot(cSlotTmp);
            }
            else
            {
             
                List<CSSlot> csSlotLists = new List<CSSlot>();

                foreach ( cPoint<int> cpos in out_stay_pos_idx_list)
                {
                    cSlot<cBubble> cSlotTmp = bubbleSlot.GetSlotByIDX(cpos);

                    if (cSlotTmp == null)
                        continue;

                    //cSlot 으로 실제 GameObject slot 를 찾는다.
                    CSSlot CsSlotTmp = csRotSlot.GetCsSlot(cSlotTmp);

                    csSlotLists.Add(CsSlotTmp);
                }
                finalCsSlot = FindNearPos(csSlotLists);
            }

            //MyPlayer
            // finalCsSlot 을 넷으로 전송 한다..
            // 그리고 다음으로 진행

            // peer 는 여기서 대기한다. 
            // 패킷이 오면 그때 1) 좌표 보정..
            // 2) 그리고 진행

            BubbleManager bubbleManager = GetBubbleManager();
            bubbleManager.SetVisible(false);
            ShootBubble bubble = ((ShootBubbleManager)bubbleManager).GetBubble();


            Player player = ((ShootBubbleManager)GetBubbleManager()).Player;

            if( PlayerManager.Instance.IsMyPlayer(player) )
            {
                C_FixedBubbleSlot fbs = new C_FixedBubbleSlot()
                {
                    ColsSlotId = finalCsSlot.GetColsSlotID(),
                    SlotId = finalCsSlot.GetID()
                };

                AppManager.Instance.NetworkManager.Send(fbs);

                //BubbleManager bubbleManager = GetBubbleManager();
                //bubbleManager.SetVisible(false);
                //ShootBubble bubble = ((ShootBubbleManager)bubbleManager).GetBubble();

                CSRotSlot.SetCsBubbleInCsSlot(GetPlayer(), finalCsSlot, bubble.GetBubbleType());

                Player.SetPlayerState(E_PLAYER_STATE.RUN_RESULT,
                    (state) =>
                    {
                        ((PlayerRunResult)state).SetCsSlot(finalCsSlot);
                    });
            }
            //((ShootBubbleManager)GetBubbleManager()).Player;
            //만약이렇게 했는데도 어색하다면 
            // 초당 4회 패킷 전송 해야함....
            //좌표로 CsSlot 을 찾아야 한다.

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.CompareTo(Defines.E_WALL_NM.WB.ToString()) == 0)
        {
            Player.SetPlayerState(PlayerStateManager.E_PLAYER_STATE.SHOOT_READY);
        }
    }


}
