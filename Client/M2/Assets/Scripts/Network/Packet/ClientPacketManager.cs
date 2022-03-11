using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System;
using System.Collections.Generic;

class PacketManager
{
	#region Singleton
	static PacketManager _instance = new PacketManager();
	public static PacketManager Instance { get { return _instance; } }
	#endregion

	PacketManager()
	{
		Register();
	}

	Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>> _onRecv = new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>>();
	Dictionary<ushort, Action<PacketSession, IMessage>> _handler = new Dictionary<ushort, Action<PacketSession, IMessage>>();

	public Action<PacketSession, IMessage, ushort> CustomHandler { get; set; }
		
	public void Register()
	{		
		_onRecv.Add((ushort)MsgId.SConnect, MakePacket<S_Connect>);
		_handler.Add((ushort)MsgId.SConnect, PacketHandler.S_ConnectHandler);		
		_onRecv.Add((ushort)MsgId.SRoomInfo, MakePacket<S_RoomInfo>);
		_handler.Add((ushort)MsgId.SRoomInfo, PacketHandler.S_RoomInfoHandler);		
		_onRecv.Add((ushort)MsgId.SJoinGameRoom, MakePacket<S_JoinGameRoom>);
		_handler.Add((ushort)MsgId.SJoinGameRoom, PacketHandler.S_JoinGameRoomHandler);		
		_onRecv.Add((ushort)MsgId.SLeaveGameRoom, MakePacket<S_LeaveGameRoom>);
		_handler.Add((ushort)MsgId.SLeaveGameRoom, PacketHandler.S_LeaveGameRoomHandler);		
		_onRecv.Add((ushort)MsgId.SSpawn, MakePacket<S_Spawn>);
		_handler.Add((ushort)MsgId.SSpawn, PacketHandler.S_SpawnHandler);		
		_onRecv.Add((ushort)MsgId.SDespawn, MakePacket<S_Despawn>);
		_handler.Add((ushort)MsgId.SDespawn, PacketHandler.S_DespawnHandler);		
		_onRecv.Add((ushort)MsgId.SStartGame, MakePacket<S_StartGame>);
		_handler.Add((ushort)MsgId.SStartGame, PacketHandler.S_StartGameHandler);		
		_onRecv.Add((ushort)MsgId.SShoot, MakePacket<S_Shoot>);
		_handler.Add((ushort)MsgId.SShoot, PacketHandler.S_ShootHandler);		
		_onRecv.Add((ushort)MsgId.SMove, MakePacket<S_Move>);
		_handler.Add((ushort)MsgId.SMove, PacketHandler.S_MoveHandler);		
		_onRecv.Add((ushort)MsgId.SNextColsBubble, MakePacket<S_NextColsBubble>);
		_handler.Add((ushort)MsgId.SNextColsBubble, PacketHandler.S_NextColsBubbleHandler);		
		_onRecv.Add((ushort)MsgId.SNextColsBubblePeer, MakePacket<S_NextColsBubblePeer>);
		_handler.Add((ushort)MsgId.SNextColsBubblePeer, PacketHandler.S_NextColsBubblePeerHandler);		
		_onRecv.Add((ushort)MsgId.SNextColsBubbleList, MakePacket<S_NextColsBubbleList>);
		_handler.Add((ushort)MsgId.SNextColsBubbleList, PacketHandler.S_NextColsBubbleListHandler);		
		_onRecv.Add((ushort)MsgId.SNextBubbles, MakePacket<S_NextBubbles>);
		_handler.Add((ushort)MsgId.SNextBubbles, PacketHandler.S_NextBubblesHandler);		
		_onRecv.Add((ushort)MsgId.SNextBubblesPeer, MakePacket<S_NextBubblesPeer>);
		_handler.Add((ushort)MsgId.SNextBubblesPeer, PacketHandler.S_NextBubblesPeerHandler);		
		_onRecv.Add((ushort)MsgId.SFixedBubbleSlotPeer, MakePacket<S_FixedBubbleSlotPeer>);
		_handler.Add((ushort)MsgId.SFixedBubbleSlotPeer, PacketHandler.S_FixedBubbleSlotPeerHandler);		
		_onRecv.Add((ushort)MsgId.SPlayerGameOverBroadCast, MakePacket<S_PlayerGameOverBroadCast>);
		_handler.Add((ushort)MsgId.SPlayerGameOverBroadCast, PacketHandler.S_PlayerGameOverBroadCastHandler);		
		_onRecv.Add((ushort)MsgId.SGameResult, MakePacket<S_GameResult>);
		_handler.Add((ushort)MsgId.SGameResult, PacketHandler.S_GameResultHandler);
	}

	public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer)
	{
		ushort count = 0;

		ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
		count += 2;
		ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
		count += 2;

		Action<PacketSession, ArraySegment<byte>, ushort> action = null;
		if (_onRecv.TryGetValue(id, out action))
			action.Invoke(session, buffer, id);
	}

	void MakePacket<T>(PacketSession session, ArraySegment<byte> buffer, ushort id) where T : IMessage, new()
	{
		T pkt = new T();
		pkt.MergeFrom(buffer.Array, buffer.Offset + 4, buffer.Count - 4);

		if(CustomHandler != null )
		{
			CustomHandler.Invoke(session,pkt,id);
		}
		else
		{
			Action<PacketSession, IMessage> action = null;
			if (_handler.TryGetValue(id, out action))
				action.Invoke(session, pkt);
		}
	}

	public Action<PacketSession, IMessage> GetPacketHandler(ushort id)
	{
		Action<PacketSession, IMessage> action = null;
		if (_handler.TryGetValue(id, out action))
			return action;
		return null;
	}
}