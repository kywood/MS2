using RotSlot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ConstData;
using static Defines;
using static Player;
using static PlayerManager;

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
        
    public void SetVisible(bool value)
    {
        base.SetVisible(value);

        if( value == true )
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
        if (GameManager.Instance.GetGameStateManager().IsGameState(GameStateManager.E_GAME_STATE.RUN) == false)
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

                //cSlot ���� ���� GameObject slot �� ã�´�.
                finalCsSlot = csRotSlot.GetCSSclot(cSlotTmp);
            }
            else
            {
             
                List<CSSlot> csSlotLists = new List<CSSlot>();

                foreach ( cPoint<int> cpos in out_stay_pos_idx_list)
                {
                    cSlot<cBubble> cSlotTmp = bubbleSlot.GetSlotByIDX(cpos);

                    if (cSlotTmp == null)
                        continue;

                    //Debug.Log("cpos : " + cpos.ToString() );
                    //Debug.Log("cSlotTmp: " + cSlotTmp.ToString());

                    //cSlot ���� ���� GameObject slot �� ã�´�.
                    CSSlot CsSlotTmp = csRotSlot.GetCSSclot(cSlotTmp);

                    csSlotLists.Add(CsSlotTmp);
                }

                finalCsSlot = FindNearPos(csSlotLists);

            }


            BubbleManager bubbleManager = GetBubbleManager();
            bubbleManager.SetVisible(false);
            ShootBubble bubble = ((ShootBubbleManager)bubbleManager).GetBubble();

            CSRotSlot.SetCsBubbleInCsSlot(GetPlayer(), finalCsSlot, bubble.GetBubbleType());

            //���� �Լ�
            GameManager.Instance.GetGameStateManager().SetGameState(GameStateManager.E_GAME_STATE.RUN_RESULT,
                (State state) =>
                {
                    ((RunResult)state).SetCsSlot(finalCsSlot);
                });

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);

        if (collision.gameObject.name.CompareTo(Defines.E_WALL_NM.WB.ToString()) == 0)
        {
            //Debug.Log(collision.gameObject.name);
            GameManager.Instance.GetGameStateManager().SetGameState(GameStateManager.E_GAME_STATE.SHOOT_READY);
        }
    }


}
