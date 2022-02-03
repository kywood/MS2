using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ready : State
{
    // Start is called before the first frame update
    //public int BubbleRowNums = 12;
    //public int BubbleRowFNums = 8;
    //public int BubbleRowSNums = 7;

    //private void MakeCollitionBubble()
    //{
    //    //// 공이 있을곳
    //    //float startX = 0.0f;
    //    //float startY = 0.0f;

    //    //Walls walls = AppManager.Instance.Walls.GetComponent<Walls>();

    //    ////Debug.Log(" Y : " + walls.WT.transform.position.y +
    //    ////          " S : " + walls.WT.GetComponent<BoxCollider2D>().size.y 
    //    ////          );

    //    //float firstX = 0.0f;
    //    //float firstEndX = 0.0f;

    //    //for (int lop = 0; lop < BubbleRowNums; lop++)
    //    //{
    //    //    int loopCnt = lop % 2 == 0 ? BubbleRowFNums : BubbleRowSNums;

    //    //    GameObject myColsSlot = Object.Instantiate(AppManager.Instance.PreFabColsSlot);
    //    //    myColsSlot.transform.parent = AppManager.Instance.BubbleParent.transform;

    //    //    for (int i = 0; i < loopCnt; i++)
    //    //    {
    //    //        GameObject myInstance = Object.Instantiate(AppManager.Instance.PreFabSlot);
    //    //        float r = myInstance.GetComponent<CircleCollider2D>().radius;
    //    //        startY = walls.WT.transform.position.y - (walls.WT.GetComponent<BoxCollider2D>().size.y / 2) - r;

    //    //        float f2 = startY - (Mathf.Sqrt(Mathf.Pow(r * 2, 2) - Mathf.Pow(r, 2)) * lop);

    //    //        startX = (-(r * 2) * (loopCnt / 2)) + (loopCnt % 2 == 0 ? r : 0);

    //    //        if(lop == 0 && i == 0 )
    //    //        {
    //    //            firstX = startX - r;
    //    //        }
    //    //        if (lop == 0 && i == (loopCnt - 1) )
    //    //        {
    //    //            firstEndX = (startX + ((r * 2) * i)) + r;
    //    //        }

    //    //        myInstance.transform.parent = myColsSlot.transform; //AppManager.Instance.BubbleParent.transform;


    //    //        myInstance.transform.position = new Vector3(startX + ((r * 2) * i), 
    //    //                                                    f2, 
    //    //                                                    0.0f);
    //    //    }
    //    //}
    //    //// 벽 좌표 이동
    //    //walls.WL.transform.position = new Vector3(firstX - (walls.WL.GetComponent<BoxCollider2D>().size.x / 2), walls.WL.transform.position.y, 0);
    //    //walls.WR.transform.position = new Vector3(firstEndX + (walls.WR.GetComponent<BoxCollider2D>().size.x / 2), walls.WR.transform.position.y, 0);

    //}
    
    public override void OnEnter()
    {
        //Debug.Log("Ready OnEnter");

        //AppManager.Instance.GetComponent<CSRotSlot>().InitRotSlot();

        //AppManager.Instance.GetComponent<CSRotSlot>().InitRotSlot();

        (GameManager.Instance.RotSlot.GetComponent<CSRotSlot>()).InitRotSlot();

        //MakeCollitionBubble();
        GameManager.Instance.Pick.SetActive(true);

        GameManager.Instance.GetGameStateManager().SetGameState(GameStateManager.E_GAME_STATE.SHOOT_READY);
    }

    public override void OnLeave()
    {
        //Debug.Log("Ready OnLeave");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        //AppManager.Instance.GetStateManager().SetGameState(StateManager.E_GAME_STATE.SHOOT_READY);
    }
}
