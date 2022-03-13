using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ISubJect;

public class Lobby : MonoBehaviour , IObserver
{
    // Start is called before the first frame update

    public Text NetworkState;
    public Text MyState;
    public Text NetPlayerState;


    private void Start()
    {

        // Online 일때 상태 다시 요청 해야 함.....

        AppManager.Instance.NetStart();

        
        AppManager.Instance.NetworkPlayerManager.RegisterObserver(this);
        AppManager.Instance.NetworkGameRoomManager.RegisterObserver(this);

        if (AppManager.Instance.IsOnline())
        {
            NetworkPlayer myPlayer = AppManager.Instance.NetworkPlayerManager.GetMyPlayerInfo();
            UpdatePlayerInfo(myPlayer);

            int roomId = 1;

            if (myPlayer.Room != null)
                roomId = myPlayer.Room.RoomId;

            C_RoomInfo roomInfo = new C_RoomInfo()
            {
                RoomId = roomId
            };

            AppManager.Instance.NetworkManager.Send(roomInfo);
        }

    }

    void UpdatePlayerInfo(NetworkPlayer myPlayer)
    {
        NetworkState.text = $"{myPlayer.PlayerId} OnLine";
    }

    public void ObserverUpdate(E_UPDAET_TYPE updateType)
    {
        NetworkPlayer myPlayer = AppManager.Instance.NetworkPlayerManager.GetMyPlayerInfo();

        if (updateType == E_UPDAET_TYPE.PLAYER_UPDATE)
            UpdatePlayerInfo(myPlayer);
        else if (updateType == E_UPDAET_TYPE.ROOM_INFO_UPSERT)
        {
            GameRoom gameRoom = AppManager.Instance.NetworkGameRoomManager.GetRoomInfo(1);

            bool isContain = AppManager.Instance.NetworkGameRoomManager.IsContain(1, myPlayer);

            for (int i = 0; i < gameRoom.Players.Count; i++)
            {
                NetworkPlayer playerInfo = gameRoom.Players[i];

                if (isContain)
                {
                    if (myPlayer.PlayerId == playerInfo.PlayerId)
                    {
                        MyState.text = $"{playerInfo.PlayerId.ToString()} Joined";
                    }
                    else
                    {
                        NetPlayerState.text = $"{playerInfo.PlayerId.ToString()} Joined";
                    }
                }
                else
                {
                    if (i == 1)
                        MyState.text = $"{playerInfo.PlayerId.ToString()} Joined";
                    else
                        NetPlayerState.text = $"{playerInfo.PlayerId.ToString()} Joined";
                }


            }
        }
        else if (updateType == E_UPDAET_TYPE.GAME_START)
        {
            Application.LoadLevel(Defines.GetScenesName(Defines.E_SCENES.GAME));
        }
    }

    public void OnBtnClick_GameRoomEnter()
    {
        //내가 이미 입장중이라면 안되고..

        //Debug.Log("OnBtnClick_GameRoomEnter");

        NetworkPlayer myPlayer = AppManager.Instance.NetworkPlayerManager.GetMyPlayerInfo();

        if (AppManager.Instance.NetworkGameRoomManager.IsContain(1, myPlayer))
            return;

        C_JoinGameRoom joinRoomPacket = new C_JoinGameRoom();
        joinRoomPacket.Player = myPlayer.PlayerInfo;
        AppManager.Instance.NetworkManager.Send(joinRoomPacket);
    }

    public void OnBtnClick_GameStart()
    {
        //Debug.Log("OnBtnClick_GameStart");

        if ( AppManager.Instance.NetworkGameRoomManager.GetRoomInfo(1).IsAbleEnterGame() )
        {
            C_StartGame Packet = new C_StartGame();
            Packet.RoomId = 1;
            AppManager.Instance.NetworkManager.Send(Packet);
        }
            //Application.LoadLevel(Defines.GetScenesName(Defines.E_SCENES.GAME));


    }
}
