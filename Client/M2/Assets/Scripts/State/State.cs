using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class State<T> where T : StateManager
{
    protected T stateManager;

    public State (T state_manager)
    {
        this.stateManager = state_manager;
    }

    public virtual void OnEnter()
    {

    }

    public virtual void OnEnter( Action<State<T>> act )
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
        

    }


}
