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

        public void GetInfo(Player myPlayer)
        {
            if (myPlayer == null)
                return;

            lock (_lock)
            {
                S_RoomInfo roomInfo = new S_RoomInfo();
                roomInfo.RoomInfo = new RoomInfo();
                roomInfo.RoomInfo.RoomId = RoomId;

                foreach( Player p in _players)
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
                
                _players.Add(player);
                player.Room = this;

                {
                    // me -> me
                    S_JoinGameRoom joinGameRoomPacket = new S_JoinGameRoom();
                    joinGameRoomPacket.Player = player.Info;
                    player.Session.Send(joinGameRoomPacket);

                    // !me -> me
                    S_Spawn spawnPacket = new S_Spawn();
                    foreach (Player p in _players)
                    {
                        if (player != p)
                            spawnPacket.Players.Add(p.Info);
                    }
                    player.Session.Send(spawnPacket);
                }

                {
                    S_Spawn spawnPacket = new S_Spawn();
                    spawnPacket.Players.Add(player.Info);
                    foreach (Player p in _players)
                    {
                        if (player != p)
                            p.Session.Send(spawnPacket);
                    }
                }
            }
        }

        public void EnterGame(Player newPlayer)
        {
            if (newPlayer == null)
                return;

            lock(_lock)
            {
                _players.Add(newPlayer); 
                newPlayer.Room = this;

                //{
                //    // me -> me
                //    S_EnterGame enterPacket = new S_EnterGame();
                //    enterPacket.Player = newPlayer.Info;
                //    newPlayer.Session.Send(enterPacket);

                //    // !me -> me
                //    S_Spawn spawnPacket = new S_Spawn();
                //    foreach (Player p in _players)
                //    {
                //        if (newPlayer != p)
                //            spawnPacket.Players.Add(p.Info);
                //    }
                //    newPlayer.Session.Send(spawnPacket);
                //}

                //{
                //    S_Spawn spawnPacket = new S_Spawn();
                //    spawnPacket.Players.Add(newPlayer.Info);
                //    foreach(Player p in _players)
                //    {
                //        if (newPlayer != p)
                //            p.Session.Send(spawnPacket);
                //    }
                //}
            }
        }

        public void LeaveRoom(int playerId)
        {
            Player player = _players.Find(p => p.Info.PlayerId == playerId);
            if (player == null)
                return;

            _players.Remove(player);
            player.Room = null;


            //// me
            //{
            //    S_LeaveGame leaveGamePacket = new S_LeaveGame();
            //    player.Session.Send(leaveGamePacket);
            //}

            ////
            //{
            //    S_Despawn despawnPacket = new S_Despawn();
            //    despawnPacket.PlayerIds.Add(player.Info.PlayerId);
            //    foreach(Player p in _players)
            //    {
            //        if(player != p)
            //        {
            //            p.Session.Send(despawnPacket);
            //        }
            //    }

            //}

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
