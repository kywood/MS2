using Google.Protobuf;
using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Game
{
    public class GameRoom
    {
        object _lock = new object();
        public int RoomId { get; set; }

        List<Player> _players = new List<Player>();

        BubbleMap _bubbleMap;

        RankMap _rankMap = new RankMap();

        GameRoomState _gameRoomState = GameRoomState.Lobby;


        public void Init()
        {
            _bubbleMap = new BubbleMap();
            _bubbleMap.Init();

            _gameRoomState = GameRoomState.Lobby;
        }

        public void GetInfo(Player myPlayer)
        {
            if (myPlayer == null)
                return;

            lock (_lock)
            {
                S_RoomInfo roomInfo = new S_RoomInfo();
                roomInfo.RoomInfo = new RoomInfo();
                roomInfo.RoomInfo.RoomId = RoomId;

                foreach (Player p in _players)
                {
                    roomInfo.RoomInfo.Players.Add(p.Info);
                }

                myPlayer.Session.Send(roomInfo);
            }
        }

        private bool IsContainPlayer(Player myPlayer)
        {
            if (myPlayer == null)
                return false;

            lock (_lock)
            {
                foreach (Player p in _players)
                {
                    if (p.Info.PlayerId == myPlayer.Info.PlayerId)
                        return true;
                }
            }
            return false;
        }

        public void JoinGameRoom(Player player)
        {
            if (player == null)
                return;

            lock (_lock)
            {
                if (_players.Count >= 2)
                {
                    S_JoinGameRoom joinGameRoomPacket = new S_JoinGameRoom();
                    joinGameRoomPacket.RoomInfo = new RoomInfo();

                    joinGameRoomPacket.RoomInfo.RoomId = RoomId;

                    foreach (Player p in _players)
                    {
                        joinGameRoomPacket.RoomInfo.Players.Add(p.Info);
                    }

                    player.Session.Send(joinGameRoomPacket);
                    return;
                }

                _bubbleMap.AddIndexer(player.Info.PlayerId);
                _players.Add(player);
                player.Room = this;


                {
                    // me -> me
                    S_JoinGameRoom joinGameRoomPacket = new S_JoinGameRoom();
                    joinGameRoomPacket.RoomInfo = new RoomInfo();

                    joinGameRoomPacket.RoomInfo.RoomId = RoomId;

                    foreach (Player p in _players)
                    {
                        joinGameRoomPacket.RoomInfo.Players.Add(p.Info);
                    }

                    player.Session.Send(joinGameRoomPacket);

                    // me -> me ( 방에 있는 사람 목록 )
                    S_Spawn spawnPacket = new S_Spawn();
                    spawnPacket.RoomId = RoomId;
                    foreach (Player p in _players)
                    {
                        if (player != p)
                            spawnPacket.Players.Add(p.Info);
                    }
                    player.Session.Send(spawnPacket);
                }

                {
                    //같은 방사람에게 전송
                    S_Spawn spawnPacket = new S_Spawn();
                    spawnPacket.RoomId = RoomId;
                    spawnPacket.Players.Add(player.Info);
                    foreach (Player p in _players)
                    {
                        if (player != p)
                            p.Session.Send(spawnPacket);
                    }
                }

                {
                    //로비유저들에게 전송
                    S_Spawn spawnPacket = new S_Spawn();
                    spawnPacket.RoomId = RoomId;
                    foreach (Player p in _players)
                    {
                        spawnPacket.Players.Add(p.Info);
                    }

                    Dictionary<int, Player> players = PlayerManager.Instance.GetPlayers();

                    foreach (Player p in players.Values)
                    {
                        if (p.Room == null)
                        {
                            p.Session.Send(spawnPacket);
                        }
                    }

                }
            }
        }

        public void NextColsBubble(ClientSession clientSession)
        {
            lock (_lock)
            {
                S_NextColsBubble nextPacket = new S_NextColsBubble();

                BubbleCols bubbleCols = _bubbleMap.Next(clientSession.MyPlayer.Info.PlayerId);

                foreach (Bubble bb in bubbleCols.Cols)
                {
                    nextPacket.BubbleTypes.Add((int)bb.BubbleType);
                }
                clientSession.Send(nextPacket);


                foreach (Player p in _players)
                {
                    if (p.Info.PlayerId == clientSession.MyPlayer.Info.PlayerId)
                        continue;

                    S_NextColsBubblePeer nextPeerPacket = new S_NextColsBubblePeer();
                    nextPeerPacket.PlayerId = clientSession.MyPlayer.Info.PlayerId;

                    foreach (Bubble bb in bubbleCols.Cols)
                    {
                        nextPeerPacket.BubbleTypes.Add((int)bb.BubbleType);
                    }

                    p.Session.Send(nextPeerPacket);
                }
            }
        }


        public void FixedBubbleSlot(ClientSession clientSession, C_FixedBubbleSlot packet)
        {
            S_FixedBubbleSlotPeer res = new S_FixedBubbleSlotPeer()
            {
                PlayerId = clientSession.MyPlayer.Info.PlayerId,
                ColsSlotId = packet.ColsSlotId,                     
                SlotId = packet.SlotId            
            };

            lock (_lock )
            {
                foreach (Player p in _players)
                {
                    if (p.Info.PlayerId == clientSession.MyPlayer.Info.PlayerId)
                        continue;

                    p.Session.Send(res);
                }

            }
        }

        public void NextBubbles(ClientSession clientSession, C_NextBubbles packet)
        {
            lock (_lock)
            {
                S_NextBubbles nextBubbles = new S_NextBubbles();

                for(int i = 0; i < packet.ReqCount; i++ )
                {
                    nextBubbles.BubbleTypes.Add((int)Bubble.GetBubbleType());
                }
                clientSession.Send(nextBubbles);

                foreach (Player p in _players)
                {
                    if (p.Info.PlayerId == clientSession.MyPlayer.Info.PlayerId)
                        continue;

                    S_NextBubblesPeer nextPeerPacket = new S_NextBubblesPeer();
                    nextPeerPacket.PlayerId = clientSession.MyPlayer.Info.PlayerId;

                    foreach (int bt in nextBubbles.BubbleTypes)
                    {
                        nextPeerPacket.BubbleTypes.Add(bt);
                    }

                    p.Session.Send(nextPeerPacket);
                }

            }
        }

        public void NextColsBubbleList(ClientSession clientSession , C_NextColsBubbleList packet)
        {
            lock (_lock)
            {
                S_NextColsBubbleList nextColsPacket = new S_NextColsBubbleList();

                for ( int i = 0; i <  packet.ColsCount; i++ )
                {
                    BubbleCols bubbleCols = _bubbleMap.Next(clientSession.MyPlayer.Info.PlayerId);
                    ColsBubbles colsBubbles = new ColsBubbles();

                    foreach (Bubble bb in bubbleCols.Cols)
                    {
                        colsBubbles.BubbleTypes.Add((int)bb.BubbleType);
                    }

                    nextColsPacket.ColsBubbles.Add(colsBubbles);
                }

                clientSession.Send(nextColsPacket);
            }
        }
        public void CMove(ClientSession clientSession, C_Move packet)
        {
            S_Move res = new S_Move()
            {
                PlayerId = clientSession.MyPlayer.Info.PlayerId,
                PosX = packet.PosX,
                PosY = packet.PosY
            };

            lock (_lock)
            {
                foreach (Player p in _players)
                {
                    if (p.Info.PlayerId == clientSession.MyPlayer.Info.PlayerId)
                        continue;

                    p.Session.Send(res);
                }

            }
        }

        public void Shoot(ClientSession clientSession, C_Shoot shootPacket )
        {
            lock (_lock)
            {
                S_Shoot packet = new S_Shoot()
                {
                    PlayerId = clientSession.MyPlayer.Info.PlayerId,
                    RadianAngle = shootPacket.RadianAngle
                };

                foreach (Player p in _players)
                {
                    if( p.Info.PlayerId != clientSession.MyPlayer.Info.PlayerId)
                        p.Session.Send(packet);
                }
            }
        }

        public void PlayerGameOver(ClientSession clientSession, C_PlayerGameOver packet)
        {
            S_PlayerGameOverBroadCast res = new S_PlayerGameOverBroadCast();
            res.PlayerRank = new PlayerRank();

            res.PlayerRank.PlayerId = clientSession.MyPlayer.Info.PlayerId;
            res.PlayerRank.Rank = 0;

            lock (_lock)
            {
                _rankMap.EndGame(clientSession.MyPlayer);
                BroadCast(res);

                //게임 종료 전부에게 랭크 1등이 나왔음
                if( _rankMap.GetRemainCount() == 0 )
                {
                    S_GameResult gameResult = new S_GameResult();

                    int loopCount = 1;
                    foreach(GamePlayer gp in _rankMap.Ranker )
                    {
                        PlayerRank playerRanker = new PlayerRank();
                        playerRanker.PlayerId = gp.Player.Info.PlayerId;
                        playerRanker.Rank = gp.Rank;
                        playerRanker.DisConnected = gp.PlayerState == eGamePlayerState.Disconnected ? true : false;
                        gameResult.PlayerRanks.Add(playerRanker);
                    }
                    BroadCast(gameResult);
                }

            }
        }



        public void StartGame()
        {
            S_StartGame packet = new S_StartGame();
            packet.RoomId = RoomId;

            lock (_lock)
            {
                _rankMap.Init(_players);

                foreach (Player p in _players)
                {
                    p.Session.Send(packet);
                }

                _gameRoomState = GameRoomState.Game;
            }
        }

        //public void EnterGame(Player newPlayer)
        //{
        //    if (newPlayer == null)
        //        return;

        //    lock(_lock)
        //    {
        //        _players.Add(newPlayer); 
        //        newPlayer.Room = this;

        //        //{
        //        //    // me -> me
        //        //    S_EnterGame enterPacket = new S_EnterGame();
        //        //    enterPacket.Player = newPlayer.Info;
        //        //    newPlayer.Session.Send(enterPacket);

        //        //    // !me -> me
        //        //    S_Spawn spawnPacket = new S_Spawn();
        //        //    foreach (Player p in _players)
        //        //    {
        //        //        if (newPlayer != p)
        //        //            spawnPacket.Players.Add(p.Info);
        //        //    }
        //        //    newPlayer.Session.Send(spawnPacket);
        //        //}

        //        //{
        //        //    S_Spawn spawnPacket = new S_Spawn();
        //        //    spawnPacket.Players.Add(newPlayer.Info);
        //        //    foreach(Player p in _players)
        //        //    {
        //        //        if (newPlayer != p)
        //        //            p.Session.Send(spawnPacket);
        //        //    }
        //        //}
        //    }
        //}

        public void LeaveRoom(int playerId)
        {
            Player player = _players.Find(p => p.Info.PlayerId == playerId);
            if (player == null)
                return;

            lock (_lock)
            {
                _players.Remove(player);
                player.Room = null;

                _rankMap.DisConnectPlayer(playerId);

                S_LeaveGameRoom res = new S_LeaveGameRoom();

                foreach (Player p in _players)
                {
                    p.Session.Send(res);
                }
            }
        }

        public void BroadCast(IMessage packet)
        {
            lock(_lock)
            {
                foreach(Player p in _players)
                {
                    p.Session.Send(packet);
                }
            }
        }
    }
}
