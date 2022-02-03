using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class State
{

    public virtual void OnEnter()
    {

    }

    public virtual void OnEnter( Action<State> act )
    {
        if( act != null )
        {
            //act(this);
            act.Invoke(this);
        }

        OnEnter();
    }

    public virtual void OnLeave()
    {

    }

    public virtual void OnUpdate()
    {
        //HACK
        Pool pool = ResPools.Instance.GetPool(MDefine.eResType.Bubble);
        foreach (int k in pool.ResList.Keys)
        {
            if (pool.ResList[k].activeSelf == false)
                continue;

        //    (pool.ResList[k].GetComponent<CSBubble>()).OnUpdate();
        }

    }

    //public virtual void SetEnterParam( List<Object> param )
    //{

    //}

}
