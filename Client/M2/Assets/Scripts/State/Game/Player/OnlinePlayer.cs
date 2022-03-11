using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerManager;

public class OnlinePlayer : Player
{

    public Text PlayerID;

    NetworkPlayer _networkPlayer;

    List<NetPacket> _packetQueue = new List<NetPacket>();
    public List<NetPacket> PacketQueue { get { return _packetQueue; } }


    public NetworkPlayer NetworkPlayer { 
        get { return _networkPlayer; } 
        set 
        {
            PlayerID.text = value.PlayerId.ToString();
            _networkPlayer = value; 
        }
    }

    protected override void OnStart()
    {
        base.OnStart();
    }


    public void PacketClearQueue(MsgId msgId)
    {
        _packetQueue.RemoveAll(p => p.MsgId == msgId);
    }

    public NetPacket PacketDeQueue( MsgId msgId )
    {
        NetPacket ret;
        foreach (NetPacket pk in _packetQueue)
        {
            if( pk.MsgId == msgId )
            {
                ret = pk;
                _packetQueue.Remove(pk);
                return ret;
            }
        }

        return null;
    }
}
