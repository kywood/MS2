using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : State
{
    // Start is called before the first frame update
    public override void OnEnter()
    {
        //Debug.Log("Run OnEnter");
        
    }

    public override void OnLeave()
    {
        GameManager.Instance.BubbleManager.GetComponent<BubbleManager>().SetVisible(false);
        //Debug.Log("Run OnLeave");

    }

    //public override void OnUpdate()
    //{

    //}
}
