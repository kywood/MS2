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

        
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        
    }
}
