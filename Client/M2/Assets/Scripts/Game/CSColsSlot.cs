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



    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
