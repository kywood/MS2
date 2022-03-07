﻿using Google.Protobuf;
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
		ClientSession clientSession = session as ClientSession;

		RoomManager.Instance.Find(1).JoinGameRoom(clientSession.MyPlayer);

		//serverSession.MyPlayer 

	}
	public static void C_RoomInfoHandler(PacketSession session, IMessage packet)
	{
		C_RoomInfo roomInfoPacket = packet as C_RoomInfo;
		ClientSession clientSession = session as ClientSession;


		RoomManager.Instance.Find(1).GetInfo(clientSession.MyPlayer);
	}

	public static void C_MoveHandler(PacketSession session, IMessage packet)
	{
		C_Move movePacket = packet as C_Move;
		ClientSession clientSession = session as ClientSession;

	}

	public static void C_StartGameHandler(PacketSession session, IMessage packet)
	{
		C_StartGame Packet = packet as C_StartGame;
		ClientSession clientSession = session as ClientSession;
		RoomManager.Instance.Find(Packet.RoomId).StartGame();
	}

	public static void C_ShootHandler(PacketSession session, IMessage packet)
	{
		C_Shoot Packet = packet as C_Shoot;
		ClientSession clientSession = session as ClientSession;

		RoomManager.Instance.Find(clientSession.MyPlayer.Room.RoomId).Shoot(clientSession,Packet);
	}
}
