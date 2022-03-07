using Google.Protobuf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf.Protocol;

public class NetPacket
{
    public MsgId MsgId { get; set; }

    public IMessage Packet { get; set; }

    public NetPacket(MsgId msgid , IMessage packet )
    {
        MsgId = msgid;
        Packet = packet;
    }

}
