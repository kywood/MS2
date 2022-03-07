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
        AppManager.Instance.NetStart();

        AppManager.Instance.NetworkPlayerManager.RegisterObserver(this);
        AppManager.Instance.NetworkGameRoomManager.RegisterObserver(this);
    }   


    public void ObserverUpdate(E_UPDAET_TYPE updateType)
    {
        if( updateType == E_UPDAET_TYPE.PLAYER_UPDATE )
            NetworkState.text = "OnLine";
        else if (updateType == E_UPDAET_TYPE.ROOM_INFO_UPSERT)
        {
            RoomInfo roomInfo = AppManager.Instance.NetworkGameRoomManager.GetRoomInfo(1);

            PlayerInfo myPlayer = AppManager.Instance.NetworkPlayerManager.GetMyPlayerInfo();

            for ( int i = 0; i < roomInfo.Players.Count; i++ )
            {
                PlayerInfo playerInfo = roomInfo.Players[i];

                if (myPlayer == null)
                {
                    NetPlayerState.text = $"{playerInfo.PlayerId.ToString()} Joined";
                }
                else
                {
                    if(myPlayer.PlayerId == playerInfo.PlayerId)
                    {
                        MyState.text = $"{playerInfo.PlayerId.ToString()} Joined";
                    }
                    else
                    {
                        NetPlayerState.text = $"{playerInfo.PlayerId.ToString()} Joined";
                    }
                }
            }
        }
    }

    public void OnBtnClick_GameRoomEnter()
    {
        //내가 이미 입장중이라면 안되고..

        Debug.Log("OnBtnClick_GameRoomEnter");

        PlayerInfo myPlayer = AppManager.Instance.NetworkPlayerManager.GetMyPlayerInfo();

        if (AppManager.Instance.NetworkGameRoomManager.IsContain(1, myPlayer))
            return;

        C_JoinGameRoom joinRoomPacket = new C_JoinGameRoom();
        joinRoomPacket.Player = AppManager.Instance.NetworkPlayerManager.GetMyPlayerInfo();
        AppManager.Instance.NetworkManager.Send(joinRoomPacket);
    }
}
