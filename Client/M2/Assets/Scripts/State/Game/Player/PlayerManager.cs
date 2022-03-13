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


    List<PlayerRank> _playerRank = new List<PlayerRank>(); 

    public List<PlayerRank> PlayerRank { get { return _playerRank; } }

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

    public void NextColsBubble(S_NextColsBubble packet)
    {
        Player p = GetPlayer(E_PLAYER_TYPE.MY_PLAYER);
        ((OnlinePlayer)p).PacketQueue.Add(new NetPacket(MsgId.SNextColsBubble, packet));
    }

    public void NextColsBubblePeer(S_NextColsBubblePeer packet)
    {
        Player p = GetPlayer(packet.PlayerId);
        ((OnlinePlayer)p).PacketQueue.Add(new NetPacket(MsgId.SNextColsBubblePeer, packet));
    }

    public void NextColsBubbleList(S_NextColsBubbleList packet)
    {
        Player p = GetPlayer(E_PLAYER_TYPE.MY_PLAYER);
        ((OnlinePlayer)p).PacketQueue.Add(new NetPacket(MsgId.SNextColsBubbleList, packet));
    }

    public void SMove(S_Move packet)
    {
        Player p = GetPlayer(packet.PlayerId);
        ((OnlinePlayer)p).PacketQueue.Add(new NetPacket(MsgId.SMove, packet));
    }

    public void SNextBubbles(S_NextBubbles packet)
    {
        Player p = GetPlayer(E_PLAYER_TYPE.MY_PLAYER);
        ((OnlinePlayer)p).PacketQueue.Add(new NetPacket(MsgId.SNextBubbles, packet));
    }

    public void SNextBubblesPeer(S_NextBubblesPeer packet)
    {
        //이패킷은 통합 처리를 한다.

        Player p = null;
        if( packet.BubbleTypes.Count > 1)
            p = GetPlayer(E_PLAYER_TYPE.MY_PLAYER);
        else
            p = GetPlayer(packet.PlayerId);

        //Player p = GetPlayer(E_PLAYER_TYPE.MY_PLAYER);
        ((OnlinePlayer)p).PacketQueue.Add(new NetPacket(MsgId.SNextBubblesPeer, packet));
    }

    public void PlayerGameOverBroadCast(S_PlayerGameOverBroadCast packet)
    {
        Player p = GetPlayer(packet.PlayerRank.PlayerId);

        if (p == null)
            return;

        p.SetGameOver();
    }

    public void GameResult(S_GameResult packet)
    {
        _playerRank.Clear();
        _playerRank.AddRange(packet.PlayerRanks);

        foreach(PlayerRank pr in packet.PlayerRanks )
        {
            Player p = GetPlayer(pr.PlayerId);
            if( p != null )
            {
                p.SetGameOver();

                ((OnlinePlayer)p).PacketQueue.Add(new NetPacket(MsgId.SGameResult, packet));

            }
        }
        
    }



    public void SFixedBubbleSlotPeer(S_FixedBubbleSlotPeer packet)
    {
        //이패킷은 통합 처리를 한다.
        Player p = GetPlayer(packet.PlayerId);
        //Player p = GetPlayer(E_PLAYER_TYPE.MY_PLAYER);
        ((OnlinePlayer)p).PacketQueue.Add(new NetPacket(MsgId.SFixedBubbleSlotPeer, packet));
    }



    public List<OnlinePlayer> GetPlayers()
    {
        return Players;
    }

    public Player GetPlayer(int playerId)
    {
        return Players.Find((p) => p.NetworkPlayer.PlayerId == playerId);
    }

    public bool IsMyPlayer ( Player player )
    {
        if( GetPlayer(E_PLAYER_TYPE.MY_PLAYER) == player )
        {
            return true;
        }

        return false;
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
