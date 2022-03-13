using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Game
{
    enum eGamePlayerState
    {
        None,
        Ranker,
        Disconnected
    }

    class GamePlayer
    {
        eGamePlayerState _playerState = eGamePlayerState.None;
        Player _player;
        int _rank = 0;

        public int Rank { 
            get { return _rank; } 
            set { _rank = value; } 
        }

        public Player Player { get { return _player; } }
        public eGamePlayerState PlayerState {
            get { return _playerState; }
            set { _playerState = value; }
        }

        public GamePlayer( Player player )
        {
            _player = player;
        }
    }

    class RankMap
    {
        List<GamePlayer> _gamePlayers = new List<GamePlayer>();
        List<GamePlayer> _ranker = new List<GamePlayer>();

        public List<GamePlayer> Ranker 
        { 
            get 
            {
                List<GamePlayer> finalRanker = new List<GamePlayer>();
                finalRanker.AddRange( _ranker );
                finalRanker.Reverse();
                finalRanker.AddRange(_gamePlayers.FindAll((gp) => gp.PlayerState == eGamePlayerState.Disconnected));

                int rankValue = 1;
                finalRanker.ForEach((gp) =>
                {
                    gp.Rank = rankValue++;

                    if (gp.PlayerState == eGamePlayerState.Disconnected)
                        gp.Rank = 0;
                    
                });

                return finalRanker; 
            } 
        }

        public RankMap()
        {
            //_playerList.AddRange(playerList);
            //_playerCount = playerCount;
        }

        public void Init( List<Player> startUsers )
        {
            _gamePlayers.Clear();
            
            foreach( Player p in startUsers )
            {
                _gamePlayers.Add(new GamePlayer(p));
            }
        }

        public int GetRemainCount()
        {
            return _gamePlayers.FindAll(gamePlayer => gamePlayer.PlayerState == eGamePlayerState.None).Count;
        }

        void UpdateTopPlayer()
        {
            if( GetRemainCount() == 1 )
            {
                GamePlayer p = _gamePlayers.Find(gamePlayer => gamePlayer.PlayerState == eGamePlayerState.None);
                AddRanker(p);
            }
        }
        bool AddRanker(GamePlayer p)
        {
            if (p == null)
                return false;

            p.PlayerState = eGamePlayerState.Ranker;
            _ranker.Add(p);

            return true;
        }

        bool RemoveRanker(GamePlayer p )
        {
            if (p == null)
                return false;

            GamePlayer findPlayer = _ranker.Find(gp => gp.Player.Info.PlayerId == p.Player.Info.PlayerId);
            
            if( findPlayer != null)
            {
                _ranker.Remove(findPlayer);
            }

            return true;

        }

        public void EndGame( Player player )
        {
           GamePlayer p = _gamePlayers.Find(gamePlayer => gamePlayer.Player.Info.PlayerId == player.Info.PlayerId);

            if(AddRanker(p))
            {
                UpdateTopPlayer();
            }
        }

        public void DisConnectPlayer( int playerId )
        {
            GamePlayer p = _gamePlayers.Find(gamePlayer => gamePlayer.Player.Info.PlayerId == playerId);
            if (p != null)
            {
                RemoveRanker(p);

                p.PlayerState = eGamePlayerState.Disconnected;
                UpdateTopPlayer();
            }
        }


    }
}
