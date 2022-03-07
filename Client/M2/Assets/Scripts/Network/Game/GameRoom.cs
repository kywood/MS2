using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoom 
{
    public int RoomId { get; set; }


    List<NetworkPlayer> _players = new List<NetworkPlayer>();

    //public RoomInfo roomInfo;

    public List<NetworkPlayer> Players { get { return _players; } }

    public GameRoom(RoomInfo roomInfo)
    {
        Upsert(roomInfo);
    }

    public NetworkPlayer GetPlayer(int playerId)
    {
        foreach (NetworkPlayer p in _players)
        {
            if (playerId == p.PlayerId)
                return p;
        }

        return null;
    }

    public NetworkPlayer GetIgnorePlayer(int playerId)
    {
        foreach (NetworkPlayer p in _players)
        {
            if (playerId != p.PlayerId)
                return p;
        }

        return null;
    }

    public bool ContainPlayer(  int playerId  )
    {
        foreach (NetworkPlayer p in _players )
        {
            if (playerId == p.PlayerId)
                return true;
        }

        return false;
    }

    public void Upsert(RoomInfo roomInfo)
    {
        _players.Clear();

        foreach (PlayerInfo pi in roomInfo.Players)
        {
            NetworkPlayer nplayer = new NetworkPlayer(pi);
            nplayer.Room = this;
            _players.Add(new NetworkPlayer(pi));
        }
    }

    public bool IsAbleEnterGame()
    {

        Debug.Log($"IsAbleEnterGame : {_players.Count}");

        foreach(NetworkPlayer p in _players)
        {
            Debug.Log($"room player : {p.PlayerId}");
        }

        if (_players.Count == 2)
            return true;

        return false;
    }

}
