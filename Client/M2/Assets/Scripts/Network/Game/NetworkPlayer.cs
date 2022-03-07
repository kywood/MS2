using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer
{
    int _playerId;
    PlayerInfo _playerInfo;

    GameRoom _room;


    public GameRoom Room 
    { 
        get { return _room; }
        set { _room = value; }
    }


    public int PlayerId { get { return _playerId; } }

    public PlayerInfo PlayerInfo { get { return _playerInfo; } }

    public NetworkPlayer(PlayerInfo playerInfo)
    {
        _playerId = playerInfo.PlayerId;
        this._playerInfo = playerInfo;
    }

}
