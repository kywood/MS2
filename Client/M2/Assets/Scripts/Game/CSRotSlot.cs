using Google.Protobuf.Collections;
using Google.Protobuf.Protocol;
using RotSlot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ConstData;
using static Player;
using static PlayerManager;

public class CSRotSlot : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject topObject;
    public GameObject bottomObject;

    public GameObject PreFabSlot;
    public GameObject PreFabColsSlot;

    public Player Player;

    private cBubbleSlot mBubbleSlot;

    private List<Vector3> mVPosColsSlots = new List<Vector3>();
    private ListEx<CSColsSlot> mCSColsSlots = new ListEx<CSColsSlot>();

    private float _bubbleDiameter;

    public cBubbleSlot GetBubbleSlot()
    {
        return mBubbleSlot;
    }

    private void ActRotateColsSlot()
    {
       
        Vector3 pos = mCSColsSlots[0].transform.position;

        for (int i = 0; i < mCSColsSlots.Count - 1; i++)
        {
            mCSColsSlots[i].transform.position = mCSColsSlots[i + 1].transform.position;
        }
        mCSColsSlots[mCSColsSlots.Count - 1].transform.position = pos;

        
        //for (int i = 0; i < mCSColsSlots.Count; i++)
        //{
        //    mCSColsSlots[i].transform.position = new Vector3(mCSColsSlots[i].transform.position.x,
        //                                                    mVPosColsSlots[i].y,
        //                                                   mCSColsSlots[i].transform.position.z);
        //}
        
    }

    public void ActRotate()
    {
        mBubbleSlot.ForWard();
        mCSColsSlots.Rotate();    
        
        ActRotateColsSlot();


         CSSlot[] csSlots = mCSColsSlots[0].GetComponentsInChildren<CSSlot>(); 

        foreach(CSSlot finalCsSlot in csSlots)
        {
            SetCsBubbleInCsSlot(Player , finalCsSlot);
        }
    }
    public void ActRotate(RepeatedField<int> bubbleTypes)
    {
        mBubbleSlot.ForWard();
        mCSColsSlots.Rotate();

        ActRotateColsSlot();

        
        CSSlot[] csSlots = mCSColsSlots[0].GetComponentsInChildren<CSSlot>();

        int loopCnt = 0;
        foreach (CSSlot finalCsSlot in csSlots)
        {
            SetCsBubbleInCsSlot(Player, finalCsSlot, (E_BUBBLE_TYPE)bubbleTypes[loopCnt++]);
        }
    }
    //public void ActRotate(NetPacket packet)
    //{
    //    mBubbleSlot.ForWard();
    //    mCSColsSlots.Rotate();

    //    ActRotateColsSlot();

    //    S_NextBubble nextBubble = packet.Packet as S_NextBubble;
    //    CSSlot[] csSlots = mCSColsSlots[0].GetComponentsInChildren<CSSlot>();

    //    int loopCnt = 0;
    //    foreach (CSSlot finalCsSlot in csSlots)
    //    {
    //        SetCsBubbleInCsSlot(Player, finalCsSlot , (E_BUBBLE_TYPE)nextBubble.BubbleTypes[loopCnt++]);
    //    }
    //}

    public static void SetCsBubbleInCsSlot( Player player , CSSlot cs_slot , E_BUBBLE_TYPE bubble_type = E_BUBBLE_TYPE.NONE )
    {
        cSlot<cBubble> cSlot = cs_slot.GetcSlot();

        cBubble bb = cBubbleHelper.Factory(new cPoint<int>(cSlot.GetID(), cSlot.GetParentID() ),
            bubble_type );

        cSlot.Set(bb);

        Pool pool = ResPools.Instance.GetPool(player.PlayerType, MDefine.eResType.Bubble);

        GameObject BubbleGO = pool.GetAbleObject();
        CSBubble cb = BubbleGO.GetComponent<CSBubble>();
        //PlayerManager.Instance.GetPlayer(player_type);

        cb.SetBubbleWithPos(player,
            bb,
            cs_slot);


    }


    public CSSlot GetCsSlot( int colsSlotId , int slotId )
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            CSColsSlot csColsSlot = transform.GetChild(i).GetComponent<CSColsSlot>();

            if(csColsSlot.GetID() == colsSlotId)
            {
                for (int j = 0; j < csColsSlot.transform.childCount; j++)
                {
                    CSSlot csSlot = csColsSlot.transform.GetChild(j).GetComponent<CSSlot>();
                    if(csSlot.GetID() == slotId)
                    {
                        return csSlot;
                    }
                }
            }
        }

        return null;
    }


    public CSSlot GetCsSlot(cSlot<cBubble> cslot)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            CSColsSlot csColsSlot = transform.GetChild(i).GetComponent< CSColsSlot>() ;

            for (int j = 0; j < csColsSlot.transform.childCount; j++)
            {
                CSSlot csSlot = csColsSlot.transform.GetChild(j).GetComponent< CSSlot>();

                if( csSlot.EqCSlot(cslot) )
                {
                    return csSlot;
                }
            }  
        }

        return null;
    }

    public void InitRotSlot()
    {
        int BubbleRowNums = Defines.G_BUBBLE_ROW_COUNT;
        int BubbleColsNums = Defines.G_BUBBLE_COL_COUNT;

        //MyPlayer player = GameManager.Instance.MyPlayer.GetComponent<MyPlayer>();

        //BubbleManager bb = GameManager.Instance.GetBubbleManager();
        _bubbleDiameter = Player.Diameter;

        Player.Pick.GetComponent<Pick>().SetHitMarkSize(_bubbleDiameter);

        mVPosColsSlots.Clear();
        mCSColsSlots.Clear();

        cBubbleSlot bs = new cBubbleSlot(BubbleRowNums , BubbleColsNums);
        mBubbleSlot = bs;

        float startX = 0.0f;
        float startY = 0.0f;

        //Walls walls = GameManager.Instance.GetMyPlayer().Walls.GetComponent<Walls>();
        Walls walls = Player.Walls.GetComponent<Walls>();

        float firstX = 0.0f;
        float firstEndX = 0.0f;

        float bubbleRadius = _bubbleDiameter / 2.0f;
        float slotRadius = bubbleRadius - Player.SLOT_RADIUS_GAP;

        //startY = topObject.transform.localPosition.y - (topObject.GetComponent<BoxCollider2D>().size.y / 2) - bubbleRadius;


        startY = bottomObject.transform.localPosition.y + (bottomObject.GetComponent<BoxCollider2D>().size.y / 2) +
            ( _bubbleDiameter * 1.2f) + (_bubbleDiameter * (bs.GetColsSlotCount() - 1));

        float topObjectY = startY + (topObject.GetComponent<BoxCollider2D>().size.y / 2) + bubbleRadius;

        topObject.transform.localPosition = new Vector3(topObject.transform.localPosition.x,
            topObjectY,
            topObject.transform.localPosition.z);

        for (int colsSlotIdx = 0; colsSlotIdx < bs.GetColsSlotCount() ; colsSlotIdx++)
        {
            cColsSlot<cBubble> colsSlot = bs.GetColsSlotByIDX(colsSlotIdx);

            int slotCount = colsSlot.GetCount();

            CSColsSlot myColsSlot = Instantiate(PreFabColsSlot).GetComponent<CSColsSlot>();
            myColsSlot.Init(bs , colsSlot);
            myColsSlot.gameObject.name = ConstData.GetPreFabProperty(E_PREFAB_TYPE.COLS_SLOT).mNM;


            mCSColsSlots.Add(myColsSlot);

            //myColsSlot.transform.parent = GameManager.Instance.RotSlot.transform;
            //Util.AddChild(GameManager.Instance.RotSlot, myColsSlot.gameObject);

            Util.AddChild( gameObject , myColsSlot.gameObject);

            //startY = topObject.transform.localPosition.y - (topObject.GetComponent<BoxCollider2D>().size.y / 2) - bubbleRadius;

            float yY = startY - (Mathf.Sqrt(Mathf.Pow(bubbleRadius * 2, 2) - Mathf.Pow(bubbleRadius, 2)) * colsSlotIdx);

            myColsSlot.transform.localPosition = new Vector3(0.0f, yY, 0.0f);

            mVPosColsSlots.Add(myColsSlot.transform.localPosition);

            if(colsSlotIdx == bs.GetColsSlotCount() - 1)
            {
                Player.Walls.GetComponent<Walls>().SetDeadLinePos(myColsSlot.transform.position);
            }


            for (int slotIdx = 0; slotIdx < slotCount; slotIdx++)
            {
                CSSlot mySlot = Instantiate(PreFabSlot).GetComponent<CSSlot>();
                mySlot.name = ConstData.GetPreFabProperty(E_PREFAB_TYPE.SLOT).mNM;
                mySlot.GetComponent<CircleCollider2D>().radius = slotRadius;

                mySlot.Init(bs, colsSlot, colsSlot.GetSlotByIDX(slotIdx));

                float r = bubbleRadius;// mySlot.GetComponent<CircleCollider2D>().radius;

                startX = (-(r * 2) * (slotCount / 2)) + (slotCount % 2 == 0 ? r : 0);

                if (colsSlotIdx == 0 && slotIdx == 0)
                {
                    firstX = startX - r;
                }
                if (colsSlotIdx == 0 && slotIdx == (slotCount - 1))
                {
                    firstEndX = (startX + ((r * 2) * slotIdx)) + r;
                }

                Util.AddChild( myColsSlot.gameObject, mySlot.gameObject);

                mySlot.transform.localPosition = new Vector3(startX + ((r * 2) * slotIdx), 0.0f, 0.0f);

            }
        }

        walls.WL.transform.localPosition = new Vector3(firstX - (walls.WL.GetComponent<BoxCollider2D>().size.x / 2), walls.WL.transform.localPosition.y, 0);
        walls.WR.transform.localPosition = new Vector3(firstEndX + (walls.WR.GetComponent<BoxCollider2D>().size.x / 2), walls.WR.transform.localPosition.y, 0);


        Player.BG.transform.position = Player.WallMaskArea.GetComponent<MaskArea>().AdJustMaskArea();

    }

}
