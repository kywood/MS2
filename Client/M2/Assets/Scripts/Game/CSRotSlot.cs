using RotSlot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ConstData;

public class CSRotSlot : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject PreFabSlot;
    public GameObject PreFabColsSlot;

    private cBubbleSlot mBubbleSlot;

    private List<Vector3> mVPosColsSlots = new List<Vector3>();
    private List<CSColsSlot> mCSColsSlots = new List<CSColsSlot>();


    public cBubbleSlot GetBubbleSlot()
    {
        return mBubbleSlot;
    }

    public void ActRotate()
    {
        mBubbleSlot.ForWard();

        foreach( CSColsSlot csColsSlot in mCSColsSlots)
        {
            csColsSlot.transform.position = new Vector3(
                csColsSlot.transform.position.x,
                csColsSlot.transform.position.y + 0.5f,
                csColsSlot.transform.position.z
                );
        }

    }

    public CSSlot GetCSSclot(cSlot<cBubble> cslot)
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

        mVPosColsSlots.Clear();
        mCSColsSlots.Clear();

        cBubbleSlot bs = new cBubbleSlot(BubbleRowNums , BubbleColsNums);
        mBubbleSlot = bs;

        float startX = 0.0f;
        float startY = 0.0f;

        Walls walls = GameManager.Instance.Walls.GetComponent<Walls>();

        float firstX = 0.0f;
        float firstEndX = 0.0f;        

        for (int colsSlotIdx = 0; colsSlotIdx < bs.GetColsSlotCount() ; colsSlotIdx++)
        {
            cColsSlot<cBubble> colsSlot = bs.GetColsSlotByIDX(colsSlotIdx);

            int slotCount = colsSlot.GetCount();

            CSColsSlot myColsSlot = Instantiate(PreFabColsSlot).GetComponent<CSColsSlot>();
            myColsSlot.Init(bs , colsSlot);
            myColsSlot.gameObject.name = ConstData.GetPreFabProperty(E_PREFAB_TYPE.COLS_SLOT).mNM;


            mCSColsSlots.Add(myColsSlot);

            //myColsSlot.transform.parent = GameManager.Instance.RotSlot.transform;

            Util.AddChild(GameManager.Instance.RotSlot, myColsSlot.gameObject);

            startY = walls.WT.transform.position.y - (walls.WT.GetComponent<BoxCollider2D>().size.y / 2) - Defines.G_SLOT_RADIUS;

            float yY = startY - (Mathf.Sqrt(Mathf.Pow(Defines.G_SLOT_RADIUS * 2, 2) - Mathf.Pow(Defines.G_SLOT_RADIUS, 2)) * colsSlotIdx);

            myColsSlot.transform.localPosition = new Vector3(0.0f, yY, 0.0f);

            mVPosColsSlots.Add(myColsSlot.transform.localPosition);

            for (int slotIdx = 0; slotIdx < slotCount; slotIdx++)
            {
                CSSlot mySlot = Instantiate(PreFabSlot).GetComponent<CSSlot>();
                mySlot.name = ConstData.GetPreFabProperty(E_PREFAB_TYPE.SLOT).mNM;

                mySlot.GetComponent<CircleCollider2D>().radius = Defines.G_SLOT_RADIUS_WITHOUT_GAP;

                float r = Defines.G_SLOT_RADIUS;// mySlot.GetComponent<CircleCollider2D>().radius;

                mySlot.Init(bs, colsSlot, colsSlot.GetSlotByIDX(slotIdx));

                //startY = walls.WT.transform.position.y - (walls.WT.GetComponent<BoxCollider2D>().size.y / 2) - r;

                //float f2 = startY - (Mathf.Sqrt(Mathf.Pow(r * 2, 2) - Mathf.Pow(r, 2)) * colsSlotIdx);

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

                //mySlot.transform.parent = myColsSlot.transform; //AppManager.Instance.BubbleParent.transform;



            }
        }

        walls.WL.transform.position = new Vector3(firstX - (walls.WL.GetComponent<BoxCollider2D>().size.x / 2), walls.WL.transform.position.y, 0);
        walls.WR.transform.position = new Vector3(firstEndX + (walls.WR.GetComponent<BoxCollider2D>().size.x / 2), walls.WR.transform.position.y, 0);

        //bs.SetItem(4, 0, cBubbleHelper.Factory(E_BUBBLE_TYPE.RED));
        //bs.SetItem(4, 1, cBubbleHelper.Factory(E_BUBBLE_TYPE.RED));

        //Debug.Log(bs.ToString());

        //bs.ForWard();
        //Debug.Log(bs.ToString());
    }

}
