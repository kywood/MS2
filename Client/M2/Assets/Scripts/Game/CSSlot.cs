using RotSlot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSSlot : MonoBehaviour
{
    cBubbleSlot mRotSlot;
    cColsSlot<cBubble> mColsSlot;
    cSlot<cBubble> mSlot;

    public void Init(cBubbleSlot rotSlot, cColsSlot<cBubble> colsSlot  , cSlot<cBubble> slot )
    {
        mRotSlot = rotSlot;
        mColsSlot = colsSlot;
        mSlot = slot;
    }


    public int GetID()
    {
        return mSlot.GetID();
    }

    public int GetColsSlotID()
    {
        return mColsSlot.GetID();
    }

    public void Pang(List<cBubble> out_pang, List<cBubble> out_drop)
    {
        mRotSlot.PangByID(new cPoint<int>(mSlot.GetID(), mColsSlot.GetID()), out_pang, out_drop);
    }

    public bool EqCSlot(cSlot<cBubble> cslot )
    {
        if ( mSlot.GetParentID() == cslot.GetParentID() &&
            mSlot.GetID() == cslot.GetID() )
        {
            return true;
        }

        return false;
    }


    public cSlot<cBubble> GetcSlot()
    {
        return mSlot;
    }

    public cBubbleSlot GetcBubbleSlot()
    {
        return mRotSlot;
    }

}
