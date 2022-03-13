using Google.Protobuf.Protocol;
using MDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{

    public Player Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Player.PlayerType != Player.DeadLine.GetComponent<DeadLine>().Player.PlayerType)
            return;

        if ( collision.name.Contains(GConst.ResPrefabs[ (int)eResType.Bubble ].PrefabsName) )
        {
            if (!collision.GetComponent<CSBubble>().IsStayState())
                return;


            if(Player.PlayerType == PlayerManager.E_PLAYER_TYPE.MY_PLAYER)
            {
                C_PlayerGameOver playerGameOverPacket = new C_PlayerGameOver()
                {

                };

                AppManager.Instance.NetworkManager.Send(playerGameOverPacket);
            }

            Player.SetPlayerState(PlayerStateManager.E_PLAYER_STATE.END);
        }
    }
}
