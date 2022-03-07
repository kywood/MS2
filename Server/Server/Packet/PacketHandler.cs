using Google.Protobuf;
using Google.Protobuf.Protocol;
using Server;
using Server.Game;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;

class PacketHandler
{

	public static void C_JoinGameRoomHandler(PacketSession session, IMessage packet)
	{
		C_JoinGameRoom Packet = packet as C_JoinGameRoom;
		ClientSession serverSession = session as ClientSession;

		RoomManager.Instance.Find(1).JoinGameRoom(serverSession.MyPlayer);

		//serverSession.MyPlayer 

	}
	public static void C_RoomInfoHandler(PacketSession session, IMessage packet)
	{
		C_RoomInfo roomInfoPacket = packet as C_RoomInfo;
		ClientSession serverSession = session as ClientSession;


		RoomManager.Instance.Find(1).GetInfo(serverSession.MyPlayer);
	}

	public static void C_MoveHandler(PacketSession session, IMessage packet)
	{
		C_Move movePacket = packet as C_Move;
		ClientSession serverSession = session as ClientSession;

	}
}
