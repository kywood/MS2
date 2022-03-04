using RotSlot;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBubbleSlot : cRotSlot<cBubble>{
    // META DATA -> res
    static Dictionary<E_COLS_TYPE, Dictionary<E_SLOT_CHK_DIR, cPoint<int>>> ChkOffSet =
        new Dictionary<E_COLS_TYPE, Dictionary<E_SLOT_CHK_DIR, cPoint<int>>>()
        {
                //홀수
                { E_COLS_TYPE.ODD , new Dictionary<E_SLOT_CHK_DIR, cPoint<int>>()
                {
                    {E_SLOT_CHK_DIR.LEFT , new cPoint<int>(-1,0) } ,
                    {E_SLOT_CHK_DIR.RIGHT , new cPoint<int>(1,0) } ,
                    {E_SLOT_CHK_DIR.UP_LEFT , new cPoint<int>(0,-1) } ,
                    {E_SLOT_CHK_DIR.UP_RIGHT , new cPoint<int>(1,-1) } ,
                    {E_SLOT_CHK_DIR.DOWN_LEFT , new cPoint<int>(0,1) } ,
                    {E_SLOT_CHK_DIR.DOWN_RIGHT , new cPoint<int>(1,1) } ,
                } } ,
                //짝수
                { E_COLS_TYPE.EVEN , new Dictionary<E_SLOT_CHK_DIR, cPoint<int>>()
                {
                    {E_SLOT_CHK_DIR.LEFT , new cPoint<int>(-1,0) } ,
                    {E_SLOT_CHK_DIR.RIGHT , new cPoint<int>(1,0) } ,
                    {E_SLOT_CHK_DIR.UP_LEFT , new cPoint<int>(-1,-1) } ,
                    {E_SLOT_CHK_DIR.UP_RIGHT , new cPoint<int>(0,-1) } ,
                    {E_SLOT_CHK_DIR.DOWN_LEFT , new cPoint<int>(-1,1) } ,
                    {E_SLOT_CHK_DIR.DOWN_RIGHT , new cPoint<int>(0,1) } ,
                } } ,
        };

    public cBubbleSlot(int row_cnt, int cols_cnt)
        : base(row_cnt, cols_cnt)
    {

    }

    private cBubble GetBubbleByIDX(cPoint<int> pos)
    {
        //if (pos.x < 0 || pos.y < 0)
        //    return null;
        cSlot<cBubble> slot = GetSlotByIDX(pos);

        return slot == null ? null : slot.GetItem();
    }

    private bool ExistByID(cPoint<int> pos)
    {
        cSlot<cBubble> slot = GetSlotByID(pos);

        return (slot == null) ? false : true;
    }

    private bool ExistBubbleByID(cPoint<int> pos)
    {
        cSlot<cBubble> slot = GetSlotByID(pos);

        if (slot.GetItem() != null)
            return true;

        return false;
    }

    private bool ChkDropStartBubbleByIDX(cPoint<int> pos, cColsSlot<cBubble> cols_slot, cBubble bb)
    {
        List<E_SLOT_CHK_DIR> chkList = new List<E_SLOT_CHK_DIR>()
        { 
            E_SLOT_CHK_DIR.LEFT ,
            E_SLOT_CHK_DIR.RIGHT ,
            E_SLOT_CHK_DIR.UP_LEFT ,
            E_SLOT_CHK_DIR.UP_RIGHT 
        };


        // HACK 수정해야함.....
        //for (int i = (int)E_SLOT_CHK_DIR.LEFT; i <= (int)E_SLOT_CHK_DIR.UP_RIGHT; i++)

        foreach(E_SLOT_CHK_DIR chkDir in chkList )
        {
            cPoint<int> new_pos = new cPoint<int>(pos.x + ChkOffSet[cols_slot.GetColsType()][chkDir].x,
                                    pos.y + ChkOffSet[cols_slot.GetColsType()][chkDir].y);

            cBubble newbb = GetBubbleByIDX(new_pos);

            if (newbb != null)
                return false;
        }

        return true;
    }


    private void GetFindDropBubble(List<cBubble> out_drop)
    {
        cCalcQueue cq = new cCalcQueue();
        for (int i = 1; i < GetColsSlotCount(); i++)
        {
            cColsSlot<cBubble> colsSlot = GetColsSlotByIDX(i);
            for (int slotIdx = 0; slotIdx < colsSlot.GetCount(); slotIdx++)
            {
                cPoint<int> pos = new cPoint<int>(slotIdx, i);

                cBubble bb = GetBubbleByIDX(pos);

                if (bb == null)
                    continue;

                // 버블을 찾으면
                if (ChkDropStartBubbleByIDX(pos, colsSlot, bb))
                {
                    Console.WriteLine("GetFindDropBubble : [{0}] [{1} , {2}]", bb.GetType(), pos.x, pos.y);
                    cq.Add(pos);
                }
            }
        }

        QueueAct(cq, (tcq, stBubble, new_pos) => {
            cBubble newbb = GetBubbleByIDX(new_pos);
            if (newbb != null)
            {
                tcq.Add(new_pos);
            }
        } , out_drop);
    }

    private void QueueAct(cCalcQueue cq, Action<cCalcQueue, cBubble, cPoint<int>> act , List<cBubble> out_list = null )
    {
        while (!cq.IsComplete())
        {
            cCalcTarget calcTarget = cq.Pop();

            cColsSlot<cBubble> colsSlot = GetColsSlotByIDX(calcTarget);
            cBubble stBubble = GetBubbleByIDX(calcTarget);

            // stBubble == null;
            // 부모자식 개념 탑재
            E_COLS_TYPE colsType = colsSlot.GetColsType();

            for (int i = 0; i < (int)E_SLOT_CHK_DIR.MAX; i++)
            {
                cPoint<int> new_pos = new cPoint<int>(calcTarget.x + ChkOffSet[colsType][(E_SLOT_CHK_DIR)i].x,
                                                        calcTarget.y + ChkOffSet[colsType][(E_SLOT_CHK_DIR)i].y);

                //act.Invoke(cq, stBubble, new_pos);
                act.Invoke(cq, stBubble, new_pos);
            }

            //Console.WriteLine("RESULT POS = {0} , {1}", calcTarget.x, calcTarget.y);

            if( out_list != null )
            {
                cBubble newbb = new cBubble(GetBubbleByIDX(calcTarget));
                out_list.Add(newbb);
                SetItemByID(calcTarget);

            }
            calcTarget.SetComplete();


        }
    }

    // 
    public bool FindStaySlot(cPoint<int> pos_id, cPoint<int> out_stay_pos_idx , List< cPoint<int> > out_stay_pos_idx_list)
    {
        out_stay_pos_idx_list.Clear();        

        cPoint<int> pos_idx = new cPoint<int>(ID2IDX(pos_id));

        cBubble current_bb = GetBubbleByIDX(pos_idx);
        if (current_bb == null) 
        {
            // bubble 이 있다..
            if (pos_idx.y == 0)
            {
                out_stay_pos_idx.DeepCopy(pos_idx);
                return true;
            }
        }
        else               
        {
            cColsSlot<cBubble> colsSlot = GetColsSlotByID(pos_id);
            E_COLS_TYPE colsType = colsSlot.GetColsType();

            //List<cPoint<int>> stayPosIdxList = new List<cPoint<int>>();

            foreach (E_SLOT_CHK_DIR dir in ConstData.GetChkStaySlotOffset())
            {
                cPoint<int> new_pos_idx = new cPoint<int>(pos_idx.x + ChkOffSet[colsType][dir].x,
                                                    pos_idx.y + ChkOffSet[colsType][dir].y);
                if (new_pos_idx.y < 0)
                    continue;

                if (new_pos_idx.x < 0)
                    continue;

                cBubble new_pos_bb = GetBubbleByIDX(new_pos_idx);
                if (new_pos_bb != null)
                    continue;

                out_stay_pos_idx_list.Add(new_pos_idx);
            }

            if (out_stay_pos_idx_list.Count > 0)
                return true;
        }


        return false;
    }

    public bool CheckStay(cPoint<int> pos)
    {
        cColsSlot<cBubble> colsSlot = GetColsSlotByID(pos);

        E_COLS_TYPE colsType = colsSlot.GetColsType();

        for (E_SLOT_CHK_DIR i = E_SLOT_CHK_DIR.UP_LEFT; i <= E_SLOT_CHK_DIR.UP_RIGHT; i++)
        {
            cPoint<int> new_pos = new cPoint<int>(pos.x + ChkOffSet[colsType][i].x,
                                                    pos.y + ChkOffSet[colsType][i].y);

            if (new_pos.y < 0)
                return true;

            cBubble newbb = GetBubbleByIDX(new_pos);
            if (newbb != null)
            {
                return true;
            }

        }
        return false;
    }

    //public void Pang(cPoint<int> pos)
    //{
    //    if (!ExistBubble(pos))
    //        return;

    //    cCalcQueue cq = new cCalcQueue();

    //    cq.Add(pos);

    //    QueueAct(cq,
    //        (cq, stBubble, new_pos) => {

    //            cBubble newbb = GetBubble(new_pos);
    //            if (newbb != null)
    //            {
    //                if (stBubble.IsSameType(newbb))
    //                {
    //                    cq.Add(new_pos);
    //                }
    //            }
    //        });

    //    // null Setting
    //    foreach (int key in cq.GetQueue().Keys)
    //    {
    //        if (cq.GetQueue()[key].calcState == E_CALC_STATE.COMPLETE)
    //            SetItem(cq.GetQueue()[key]);
    //    }

    //    // 연결이 끊어진 구슬 떨구기 
    //    GetFindDropBubble();
    //}


    void DropBubble(List<cBubble> out_drop)
    {
        cCalcQueue cq = new cCalcQueue();

        cColsSlot<cBubble> colsSlot = GetColsSlotByIDX(0);

        for (int i = 0; i < colsSlot.GetCount(); i++)
        {
            if (GetBubbleByIDX(new cPoint<int>(i, 0)) == null)
                continue;

            cq.Add(new cPoint<int>(i, 0));
            
            QueueAct(cq,
            (tcq, stBubble, new_pos) => {

                cBubble newbb = GetBubbleByIDX(new_pos);
                if (newbb != null)
                {
                    tcq.Add(new_pos);
                }
            });
        }

        for( int colsSlotIdx = 1 ; colsSlotIdx < GetColsSlotCount(); colsSlotIdx++ )
        {
            colsSlot = GetColsSlotByIDX(colsSlotIdx);

            for( int slotIdx = 0; slotIdx < colsSlot.GetCount(); slotIdx++)
            {
                cBubble bb = colsSlot.GetItemByIDX(slotIdx);

                if (bb == null)
                    continue;

                cPoint<int> pt = new cPoint<int>(slotIdx, colsSlotIdx);

                if ( cq.IsContain( pt ) )
                {
                    continue;
                }

                out_drop.Add(bb);
                SetItemByIDX(pt);
            }
        }       

    }

    public void PangByID(cPoint<int> pos , List<cBubble > out_pang , List<cBubble> out_drop)
    {
        //HACK
        if (!ExistBubbleByID(pos))
            return;

        out_pang.Clear();
        out_drop.Clear();

        // id  -> idx
        cPoint<int> npos = ID2IDX(pos);

        cCalcQueue cq = new cCalcQueue();

        cq.Add(npos);

        QueueAct(cq,
            (cq, stBubble, new_pos) => {

                cBubble newbb = GetBubbleByIDX(new_pos);
                if (newbb != null)
                {
                    if (stBubble.IsSameType(newbb))
                    {
                        cq.Add(new_pos);
                    }
                }
            });

        if (cq.GetCalcStateCount(E_CALC_STATE.COMPLETE) >= Defines.G_BUBBLE_PANG_COUNT)
        {
            // null Setting
            foreach (int key in cq.GetQueue().Keys)
            {
                if (cq.GetQueue()[key].calcState == E_CALC_STATE.COMPLETE)
                {
                    cBubble newbb = new cBubble(GetBubbleByIDX(cq.GetQueue()[key]));
                    out_pang.Add(newbb);

                    SetItemByIDX(cq.GetQueue()[key]);
                }
            }
        }
         
        // 연결이 끊어진 구슬 떨구기 

        DropBubble(out_drop);
        //GetFindDropBubble(out_drop);

        //int k = 0;
        //k = 100;
    }



}