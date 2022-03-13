using RotSlot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : DontDestroy<AppManager>
{
    NetworkManager _networkManager;

    NetworkPlayerManager _networkPlayerManager;
    NetworkGameRoomManager _networkGameRoomManager;

    public NetworkManager NetworkManager { get { return _networkManager; } }
    public NetworkPlayerManager NetworkPlayerManager { get { return _networkPlayerManager; } }
    public NetworkGameRoomManager NetworkGameRoomManager { get { return _networkGameRoomManager; } }

    override protected void OnAwake()
    {
        base.OnAwake();
        Application.targetFrameRate = 60;
    }

    
    override protected void OnStart()
    {
        base.OnStart();

    }

    public bool IsOnline()
    {
        if (_networkManager == null)
            return false;

        return _networkManager.Online();
    }

    public bool NetStart()
    {
        if (IsOnline())
            return true;

        if (_networkManager == null)
        {
            _networkPlayerManager = new NetworkPlayerManager();
            _networkGameRoomManager = new NetworkGameRoomManager();

            _networkManager = new NetworkManager();
            _networkManager.OnStart();

            return true;
        }

        _networkPlayerManager.Clear();
        return true;
    }


    public void NetStop()
    {
        if (_networkManager != null)
            _networkManager.OnStop();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if(_networkManager!=null)
            _networkManager.OnUpdate();

    }
}
