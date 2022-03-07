using Google.Protobuf.Collections;
using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ISubJect;

public class NetworkGameRoomManager : AbSubJect
{
    Dictionary<int, GameRoom> _rooms = new Dictionary<int, GameRoom>();

    //RoomInfo roomInfo = new RoomInfo();

    public void Clear()
    {
        _rooms.Clear();
    }

    public GameRoom GetRoomInfo(int roomId)
    {
        if(_rooms.ContainsKey(roomId) )
            return _rooms[roomId];

        return null;
    }

    public void StartGame()
    {
        NotifyObservers(E_UPDAET_TYPE.GAME_START);
    }

    public bool IsContain(int roomId , NetworkPlayer player )
    {
        GameRoom roomInfo = GetRoomInfo(roomId);

        foreach(NetworkPlayer p in roomInfo.Players)
        {
            if (p.PlayerId == player.PlayerId)
                return true;
        }
        return false;
    }

    public void Spawn(int roomId , RepeatedField<PlayerInfo> players )
    {
        GameRoom gameRoom = GetRoomInfo(roomId);

        foreach(PlayerInfo p in players)
        {
            if(!gameRoom.ContainPlayer( p.PlayerId ))
                gameRoom.Players.Add(new NetworkPlayer(p));
        }

        NotifyObservers(E_UPDAET_TYPE.ROOM_INFO_UPSERT);
    }

    public void Upsert(RoomInfo roomInfo)
    {
        if( _rooms.ContainsKey(roomInfo.RoomId) )
        {
            _rooms[roomInfo.RoomId].Upsert( roomInfo);
        }
        else
        {
            _rooms.Add(roomInfo.RoomId, new GameRoom( roomInfo ));
        }
        /// 업데이트 항목 전송  룸info ...
        /// // 아니면 콜백???
        /// 
        NotifyObservers(E_UPDAET_TYPE.ROOM_INFO_UPSERT);
    }

    public void Remove(int roomId)
    {
        _rooms.Remove(roomId);
    }


}
