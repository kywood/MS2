using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ISubJect;

public class NetworkPlayerManager : AbSubJect
{

    Dictionary<int, NetworkPlayer> _players = new Dictionary<int, NetworkPlayer>();

    int _myId = -100;


    public NetworkPlayer MyPlayer;

    public void Clear()
    {
        _players.Clear();
        _myId = -100;
    }

    public void Add(PlayerInfo player, bool myPlayer = false)
    {
        Add(new NetworkPlayer(player), myPlayer);
    }
    public void Add(NetworkPlayer networkPlayer, bool myPlayer = false)
    {
        _players.Add(networkPlayer.PlayerId, networkPlayer);

        if (myPlayer)
        {
            MyPlayer = networkPlayer;
            //Lobby.UpdateOnlineState(playerInfo);

            NotifyObservers(E_UPDAET_TYPE.PLAYER_UPDATE);
        }

    
    }

    public NetworkPlayer GetMyPlayerInfo()
    {
        return MyPlayer;
    }

    public void Remove( int playerId )
    {
        _players.Remove(playerId);
    }


}
