using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRun : State<StateManager>
{
    public GameRun(StateManager state_manager) : base(state_manager)
    {
    }

    // Start is called before the first frame update
    public override void OnEnter()
    {
        //Debug.Log("Run OnEnter");
        
    }

    public override void OnLeave()
    {

        //PlayerManager.Instance.DualAct((p) => p.BubbleManager.GetComponent<BubbleManager>().SetVisible(false));
        //GameManager.Instance.GetMyPlayer().BubbleManager.GetComponent<BubbleManager>().SetVisible(false);
        //Debug.Log("Run OnLeave");

    }

    //public override void OnUpdate()
    //{

    //}
}