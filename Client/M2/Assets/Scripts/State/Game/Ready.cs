using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ready : State
{
    
    public override void OnEnter()
    {
        //AppManager.Instance.GetComponent<CSRotSlot>().InitRotSlot();

        (GameManager.Instance.RotSlot.GetComponent<CSRotSlot>()).InitRotSlot();

        //MakeCollitionBubble();
        //GameManager.Instance.Pick.SetActive(true);

        //GameManager.Instance.RotSlot.GetComponent<CSRotSlot>().ActRotate();
        //GameManager.Instance.RotSlot.GetComponent<CSRotSlot>().ActRotate();
        //GameManager.Instance.RotSlot.GetComponent<CSRotSlot>().ActRotate();

        GameManager.Instance.StartCoroutine(EffectStartRow());        

    }

    IEnumerator EffectStartRow()
    {
        for( int i = 0; i < Defines.G_BUBBLE_SRART_ROW_COUNT; i++  )
        {
            GameManager.Instance.RotSlot.GetComponent<CSRotSlot>().ActRotate();
            yield return new WaitForSeconds(0.2f);
        }

        GameManager.Instance.GetGameStateManager().SetGameState(GameStateManager.E_GAME_STATE.SHOOT_READY);
        yield return null;
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
