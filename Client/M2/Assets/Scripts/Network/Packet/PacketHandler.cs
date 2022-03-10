using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class PacketHandler
{
	public static void S_ConnectHandler(PacketSession session, IMessage packet)
    {
		Debug.Log("S_ConnectHandler");

		S_Connect Packet = packet as S_Connect;

		ServerSession serverSession = session as ServerSession;

		AppManager.Instance.NetworkPlayerManager.Add(Packet.Player , true);

		Debug.Log("S_ConnectHandler");

		C_RoomInfo roomInfo = new C_RoomInfo()
		{
			RoomId = 1
		};

		serverSession.Send(roomInfo);
	}

	public static void S_RoomInfoHandler(PacketSession session, IMessage packet)
	{
		S_RoomInfo Packet = packet as S_RoomInfo;
		ServerSession serverSession = session as ServerSession;

		Debug.Log("S_RoomInfoHandler");

		AppManager.Instance.NetworkGameRoomManager.Upsert(Packet.RoomInfo);

	}
	public static void S_JoinGameRoomHandler(PacketSession session, IMessage packet)
	{
		S_JoinGameRoom Packet = packet as S_JoinGameRoom;
		ServerSession serverSession = session as ServerSession;

		Debug.Log("S_JoinGameRoomHandler");

		AppManager.Instance.NetworkGameRoomManager.Upsert(Packet.RoomInfo);

	}
	public static void S_LeaveGameRoomHandler(PacketSession session, IMessage packet)
	{
		S_LeaveGameRoom Packet = packet as S_LeaveGameRoom;
		ServerSession serverSession = session as ServerSession;
	}
	


	public static void S_SpawnHandler(PacketSession session, IMessage packet)
	{
		S_Spawn Packet = packet as S_Spawn;
		ServerSession serverSession = session as ServerSession;

		Debug.Log("S_SpawnHandler");
		AppManager.Instance.NetworkGameRoomManager.Spawn(Packet.RoomId , Packet.Players );
	}

	public static void S_DespawnHandler(PacketSession session, IMessage packet)
	{
		S_Despawn despawnPacket = packet as S_Despawn;
		ServerSession serverSession = session as ServerSession;
	}

	public static void S_ShootHandler(PacketSession session, IMessage packet)
	{
		S_Shoot Packet = packet as S_Shoot;
		ServerSession serverSession = session as ServerSession;

		PlayerManager.Instance.Shoot(Packet);

		//shoot packet 
	}

	public static void S_NextColsBubbleHandler(PacketSession session, IMessage packet)
	{
		S_NextColsBubble Packet = packet as S_NextColsBubble;
		ServerSession serverSession = session as ServerSession;

		Debug.Log("S_NextBubbleHandler");

		PlayerManager.Instance.NextColsBubble(Packet);
	}

	public static void S_NextColsBubblePeerHandler(PacketSession session, IMessage packet)
	{
		S_NextColsBubblePeer Packet = packet as S_NextColsBubblePeer;
		ServerSession serverSession = session as ServerSession;

		Debug.Log("S_NextColsBubblePeerHandler");


		PlayerManager.Instance.NextColsBubblePeer(Packet);

	}


	public static void S_NextColsBubbleListHandler(PacketSession session, IMessage packet)
	{
		S_NextColsBubbleList Packet = packet as S_NextColsBubbleList;
		ServerSession serverSession = session as ServerSession;

		Debug.Log("S_NextColsBubbleListHandler");

		PlayerManager.Instance.NextColsBubbleList(Packet);
	}


	public static void S_MoveHandler(PacketSession session, IMessage packet)
	{
		S_Move movePacket = packet as S_Move;
		ServerSession serverSession = session as ServerSession;
	}

	public static void S_StartGameHandler(PacketSession session, IMessage packet)
	{
		S_StartGame Packet = packet as S_StartGame;
		ServerSession serverSession = session as ServerSession;


		AppManager.Instance.NetworkGameRoomManager.StartGame();
	}

	public static void S_NextBubblesHandler(PacketSession session, IMessage packet)
	{
		S_NextBubbles Packet = packet as S_NextBubbles;
		ServerSession serverSession = session as ServerSession;

		Debug.Log("S_NextBubblesHandler");
		//AppManager.Instance.NetworkGameRoomManager.StartGame();

		PlayerManager.Instance.SNextBubbles(Packet);


	}
	public static void S_NextBubblesPeerHandler(PacketSession session, IMessage packet)
	{
		S_NextBubblesPeer Packet = packet as S_NextBubblesPeer;
		ServerSession serverSession = session as ServerSession;

		Debug.Log("S_NextBubblesPeerHandler");

		PlayerManager.Instance.SNextBubblesPeer(Packet);
		//AppManager.Instance.NetworkGameRoomManager.StartGame();
	}
}
