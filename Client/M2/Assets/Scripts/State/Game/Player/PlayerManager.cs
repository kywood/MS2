using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    // Start is called before the first frame update

    public enum E_PLAYER_TYPE
    {
        COMMON,
        MY_PLAYER,
        PEER
    }


    public List<Player> Players = new List<Player>();


    public List<Player> GetPlayers()
    {
        return Players;
    }

    public Player GetPlayer(E_PLAYER_TYPE player_type)
    {
        foreach(Player p in Players)
        {
            if (p.PlayerType == player_type)
                return p;
        }

        return null;
    }

    public void DualAct( Action<Player> act )
    {
        foreach( Player p in Players)
        {
            if( p.gameObject.activeSelf)
                act.Invoke(p);
        }
    }

}
