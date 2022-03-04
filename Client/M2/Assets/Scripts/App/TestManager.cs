using RotSlot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : DontDestroy<TestManager>
{
    public GameObject txtObj;

    NetworkManager _networkManager = new NetworkManager();
    override protected void OnAwake()
    {
        base.OnAwake();
    }

    
    override protected void OnStart()
    {
        base.OnStart();

        _networkManager.OnStart();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        _networkManager.OnUpdate();
    }

}
