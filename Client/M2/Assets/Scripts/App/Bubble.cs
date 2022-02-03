using RotSlot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ConstData;

public class Bubble : MonoBehaviour
{

    private E_BUBBLE_TYPE mBubbleType = E_BUBBLE_TYPE.NONE;
    public void Awake()
    {
        //transform.localScale = new Vector3(Defines.G_SLOT_RADIUS * 2,
        //    Defines.G_SLOT_RADIUS * 2, 0.0f);
    }

    public void Start()
    {
        
    }

    public E_BUBBLE_TYPE GetBubbleType()
    {
        return mBubbleType;
    }

    public void SetBubbleType( E_BUBBLE_TYPE bubble_type )
    {
        mBubbleType = bubble_type;

        SpriteRenderer sp = GetComponent<SpriteRenderer>();

        sp.sprite = GameManager.Instance.GetBubbleManager().GetSprite(bubble_type);

        //Color c = ConstData.GetBubbleProperty(bubble_type).mColor;
        //sp.color = ConstData.GetBubbleProperty(bubble_type).mColor;

        //GameObject NewObj = Util.AddChildWithOutScaleLayer(parent, Instantiate(Resources.Load(preFabsPath) as GameObject));

        //sp.sprite = Resources.Load< Sprite>(ConstData.GetBubbleProperty(bubble_type).mImgPath);

        //Instantiate(Resources.Load("Imgs/Bubble/1.png")) as 


        Debug.Log(bubble_type);
        //Debug.Log(c.ToString());


    }
        
    public void SetVisible(bool value)
    {
        gameObject.SetActive(value);
        if( value == true )
        {
            Pick pick = GameManager.Instance.Pick.GetComponent<Pick>();

            transform.position = pick.ShootBody.transform.position;


            SetBubbleType(ConstData.GetNextBubbleType());
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

            CSRotSlot csRotSlot = GameManager.Instance.GetRotSlot().GetComponent<CSRotSlot>();
            cBubbleSlot bubbleSlot = csRotSlot.GetBubbleSlot();

            CSSlot finalCsSlot;

            if (out_stay_pos_idx_list.Count <= 0)
            {
                stay_idx = out_top_stay_pos_idx;


                cSlot<cBubble> cSlotTmp = bubbleSlot.GetSlotByIDX(out_top_stay_pos_idx);

                //cSlot 으로 실제 GameObject slot 를 찾는다.
                finalCsSlot = csRotSlot.GetCSSclot(cSlotTmp);

            }
            else
            {
                // 나온 결과 로  sort 해서 1등 뽑기..
                // collision --> out_stay_pos_idx_list  == > distance 1 등인거
                //out_stay_pos_idx_list
                //Debug.Log("aAAAAAAAAAAAAAAAAAAAAAAAAA");

                List<CSSlot> csSlotLists = new List<CSSlot>();

                foreach ( cPoint<int> cpos in out_stay_pos_idx_list)
                {
                    

                    cSlot<cBubble> cSlotTmp = bubbleSlot.GetSlotByIDX(cpos);

                    if (cSlotTmp == null)
                        continue;

                    Debug.Log("cpos : " + cpos.ToString() );
                    Debug.Log("cSlotTmp: " + cSlotTmp.ToString());

                    //cSlot 으로 실제 GameObject slot 를 찾는다.
                    CSSlot CsSlotTmp = csRotSlot.GetCSSclot(cSlotTmp);

                    csSlotLists.Add(CsSlotTmp);
                }

                finalCsSlot = FindNearPos(csSlotLists);

            }

            //CSRotSlot csRotSlot = AppManager.Instance.GetRotSlot().GetComponent<CSRotSlot>();
            //cBubbleSlot bubbleSlot = csRotSlot.GetBubbleSlot();


            BubbleManager bubbleManager = GameManager.Instance.BubbleManager.GetComponent<BubbleManager>();
            bubbleManager.SetVisible(false);
            Bubble bubble = bubbleManager.GetBubble();

            cSlot<cBubble> cSlot = finalCsSlot.GetcSlot();

            //AppManager.Instance.BubbleManager.GetComponent<BubbleManager>().SetVisible(false);
            Debug.Log(collision.name + " " + cSlot.GetParentID() + " " + cSlot.GetID());

            cBubble bb = cBubbleHelper.Factory(bubble.GetBubbleType(), new cPoint<int>(cSlot.GetID(), cSlot.GetParentID() ));
            cSlot.Set(bb);

            Pool pool = ResPools.Instance.GetPool(MDefine.eResType.Bubble);

            GameObject BubbleGO = pool.GetAbleObject();

            CSBubble cb = BubbleGO.GetComponent<CSBubble>();
            cb.SetBubbleWithPos(bb , finalCsSlot.transform.position);

            //HACK
            //cb.transform.localScale = new Vector3(Defines.G_SLOT_RADIUS * 2, Defines.G_SLOT_RADIUS * 2, 0.0f);
            //            cb.transform.position = finalCsSlot.transform.position;


            //HACK 
            //무형 함수
            GameManager.Instance.GetGameStateManager().SetGameState(GameStateManager.E_GAME_STATE.RUN_RESULT,
                (State state) =>
                {
                    ((RunResult)state).SetCsSlot(finalCsSlot);
                });

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.name.CompareTo(Defines.E_WALL_NM.WB.ToString()) == 0)
        {
            //Debug.Log(collision.gameObject.name);
            GameManager.Instance.GetGameStateManager().SetGameState(GameStateManager.E_GAME_STATE.SHOOT_READY);
        }


    }


}
