using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Game
{
    class RankMap
    {
        //Dictionary<int, Player> _rankerTable = new Dictionary<int, Player>();

        //int _playerCount;

        //public Dictionary<int, Player> RankerTable { get { return _rankerTable; } }

        List<Player> _rankList = new List<Player>();

        //List<Player> _playerList = new List<Player>();

        public RankMap()
        {
            //_playerList.AddRange(playerList);
            //_playerCount = playerCount;
        }

        public void AddRanker( Player player )
        {
            _rankList.Add(player);
        }


    }
}
