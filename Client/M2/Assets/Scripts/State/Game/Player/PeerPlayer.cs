using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerManager;

public class PeerPlayer : OnlinePlayer
{
    PeerPlayer()
    {
        PlayerType = E_PLAYER_TYPE.PEER;
    }
    protected override void OnStart()
    {
        base.OnStart();        
    }

}
