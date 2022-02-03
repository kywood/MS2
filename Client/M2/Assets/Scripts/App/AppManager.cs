using RotSlot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : DontDestroy<AppManager>
{
    



    


    override protected void OnAwake()
    {
        base.OnAwake();
        Application.targetFrameRate = 60;
    }

    
    override protected void OnStart()
    {
        base.OnStart();

        //HACK 20200812
        Debug.Log(Application.persistentDataPath);

        

        //cBubbleSlot bs = new cBubbleSlot();

        //bs.SetItem(4, 0, cBubbleHelper.Factory(E_BUBBLE_TYPE.RED));
        //bs.SetItem(4, 1, cBubbleHelper.Factory(E_BUBBLE_TYPE.RED));

        //Debug.Log(bs.ToString());

        //bs.ForWard();
        //Debug.Log(bs.ToString());

        //bs.Print();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        
    }
}
