using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    // Start is called before the first frame update

    public enum E_PLAYER_TYPE
    {
        COMMON,
        MY_PLAYER,
        PEER
    }


    public List<OnlinePlayer> Players = new List<OnlinePlayer>();


    GameRoom _gameRoom;

    public GameRoom GameRoom { get { return _gameRoom; } }

    protected override void OnStart()
    {
        _gameRoom = AppManager.Instance.NetworkGameRoomManager.GetRoomInfo(1);
        NetworkPlayer myPlayer = AppManager.Instance.NetworkPlayerManager.GetMyPlayerInfo();


        foreach ( OnlinePlayer p in Players )
        {
            if( p.PlayerType == E_PLAYER_TYPE.MY_PLAYER )
            {
                p.NetworkPlayer = _gameRoom.GetPlayer(myPlayer.PlayerId);
            }
            else
            {
                p.NetworkPlayer = _gameRoom.GetIgnorePlayer(myPlayer.PlayerId);
            }
        }
    }


    public void Shoot(S_Shoot packet)
    {
        Player p = GetPlayer(packet.PlayerId);
        ((OnlinePlayer)p).PacketQueue.Add(new NetPacket(MsgId.SShoot, packet));
    }

    public void NextBubble(S_NextBubble packet)
    {
        Player p = GetPlayer(E_PLAYER_TYPE.MY_PLAYER);
        ((OnlinePlayer)p).PacketQueue.Add(new NetPacket(MsgId.SNextBubble, packet));
    }

    public void NextColsBubble(S_NextColsBubble packet)
    {
        Player p = GetPlayer(E_PLAYER_TYPE.MY_PLAYER);
        ((OnlinePlayer)p).PacketQueue.Add(new NetPacket(MsgId.SNextColsBubble, packet));
    }


    public List<OnlinePlayer> GetPlayers()
    {
        return Players;
    }

    Player GetPlayer(int playerId)
    {
        return Players.Find((p) => p.NetworkPlayer.PlayerId == playerId);
    }

    public Player GetPlayer(E_PLAYER_TYPE player_type)
    {
        foreach(Player p in Players)
        {
            if (p.PlayerType == player_type)
                return p;
        }

        return null;
    }

    public void DualAct( Action<Player> act )
    {
        foreach( Player p in Players)
        {
            if( p.gameObject.activeSelf)
                act.Invoke(p);
        }
    }

}
