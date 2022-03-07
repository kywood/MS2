using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ISubJect;

public class NetworkPlayerManager : AbSubJect
{

    Dictionary<int, PlayerInfo> _players = new Dictionary<int, PlayerInfo>();

    int _myId = -100;


    public int MyId { get { return _myId; } }

    public void Clear()
    {
        _players.Clear();
        _myId = -100;
    }


    public void Add(PlayerInfo playerInfo, bool myPlayer = false)
    {
        _players.Add(playerInfo.PlayerId, playerInfo);

        if (myPlayer)
        {
            _myId = playerInfo.PlayerId;
            //Lobby.UpdateOnlineState(playerInfo);

            NotifyObservers(E_UPDAET_TYPE.PLAYER_UPDATE);
        }

    
    }

    public PlayerInfo GetMyPlayerInfo()
    {
        if (_players.ContainsKey(_myId))
            return _players[_myId];

        return null;
    }

    public void Remove( int playerId )
    {
        _players.Remove(playerId);
    }


}
