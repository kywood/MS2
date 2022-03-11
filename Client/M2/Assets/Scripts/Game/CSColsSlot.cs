using RotSlot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSColsSlot : MonoBehaviour
{
    cBubbleSlot mRotSlot;
    cColsSlot<cBubble> mColsSlot;

    public void Init(cBubbleSlot rotSlot, cColsSlot<cBubble> colsSlot)
    {
        mRotSlot = rotSlot;
        mColsSlot = colsSlot;
    }

    public int GetID()
    {
        return mColsSlot.GetID();
    }

    public int GetIDX()
    {
        return mRotSlot.ID2IDX(GetID());
    }

}
