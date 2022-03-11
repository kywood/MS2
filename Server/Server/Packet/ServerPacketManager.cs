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
		_onRecv.Add((ushort)MsgId.CRoomInfo, MakePacket<C_RoomInfo>);
		_handler.Add((ushort)MsgId.CRoomInfo, PacketHandler.C_RoomInfoHandler);		
		_onRecv.Add((ushort)MsgId.CJoinGameRoom, MakePacket<C_JoinGameRoom>);
		_handler.Add((ushort)MsgId.CJoinGameRoom, PacketHandler.C_JoinGameRoomHandler);		
		_onRecv.Add((ushort)MsgId.CStartGame, MakePacket<C_StartGame>);
		_handler.Add((ushort)MsgId.CStartGame, PacketHandler.C_StartGameHandler);		
		_onRecv.Add((ushort)MsgId.CShoot, MakePacket<C_Shoot>);
		_handler.Add((ushort)MsgId.CShoot, PacketHandler.C_ShootHandler);		
		_onRecv.Add((ushort)MsgId.CMove, MakePacket<C_Move>);
		_handler.Add((ushort)MsgId.CMove, PacketHandler.C_MoveHandler);		
		_onRecv.Add((ushort)MsgId.CNextColsBubble, MakePacket<C_NextColsBubble>);
		_handler.Add((ushort)MsgId.CNextColsBubble, PacketHandler.C_NextColsBubbleHandler);		
		_onRecv.Add((ushort)MsgId.CNextColsBubbleList, MakePacket<C_NextColsBubbleList>);
		_handler.Add((ushort)MsgId.CNextColsBubbleList, PacketHandler.C_NextColsBubbleListHandler);		
		_onRecv.Add((ushort)MsgId.CNextBubbles, MakePacket<C_NextBubbles>);
		_handler.Add((ushort)MsgId.CNextBubbles, PacketHandler.C_NextBubblesHandler);		
		_onRecv.Add((ushort)MsgId.CFixedBubbleSlot, MakePacket<C_FixedBubbleSlot>);
		_handler.Add((ushort)MsgId.CFixedBubbleSlot, PacketHandler.C_FixedBubbleSlotHandler);		
		_onRecv.Add((ushort)MsgId.CPlayerGameOver, MakePacket<C_PlayerGameOver>);
		_handler.Add((ushort)MsgId.CPlayerGameOver, PacketHandler.C_PlayerGameOverHandler);
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