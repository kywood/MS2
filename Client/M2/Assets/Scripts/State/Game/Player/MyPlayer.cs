using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerManager;

public class MyPlayer : OnlinePlayer
{

    public GameObject Next;



    MyPlayer()
    {
        PlayerType = E_PLAYER_TYPE.MY_PLAYER;
    }

    protected override void OnStart()
    {
        base.OnStart();
    }
}
