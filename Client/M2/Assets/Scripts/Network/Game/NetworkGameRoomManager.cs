using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ISubJect;

public class NetworkGameRoomManager : AbSubJect
{
    Dictionary<int, RoomInfo> _rooms = new Dictionary<int, RoomInfo>();

    RoomInfo roomInfo = new RoomInfo();

    public void Clear()
    {
        _rooms.Clear();
    }

    public RoomInfo GetRoomInfo(int roomId)
    {
        return _rooms[roomId];
    }

    public bool IsContain(int roomId , PlayerInfo player )
    {
        RoomInfo roomInfo = GetRoomInfo(roomId);

        foreach( PlayerInfo p in roomInfo.Players)
        {
            if (p.PlayerId == player.PlayerId)
                return true;
        }
        return false;
    }

    public void Upsert(RoomInfo roomInfo)
    {
        if( _rooms.ContainsKey(roomInfo.RoomId) )
        {
            _rooms[roomInfo.RoomId] = roomInfo;
        }
        else
        {
            _rooms.Add(roomInfo.RoomId, roomInfo);
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
