using Google.Protobuf;
using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketDequeStates
{
    public enum ePkState
    {
        None,
        Processing,
        Complete
    }

    List<MsgId> _packetDequeStates = new List<MsgId>();

    ePkState _packetState = ePkState.None;

    public List<MsgId> PacketDequeStatesList 
    { 
        get { return _packetDequeStates; }
    }

    public ePkState PacketState { get { return _packetState;} }

    public void Init()
    {
        _packetDequeStates.Clear();
        _packetState = ePkState.None;
    }
    public void AddWillRecvPacketId(MsgId msgId)
    {
        _packetState = ePkState.Processing;

        _packetDequeStates.Add(msgId);
    }

    public void Complete(MsgId msgId)
    {
        foreach( MsgId _msgId in _packetDequeStates )
        {
            if(_msgId == msgId)
            {
                _packetDequeStates.Remove(_msgId);
                break;
            }
        }

        if (_packetDequeStates.Count <= 0)
            _packetState = ePkState.Complete;
    }



}