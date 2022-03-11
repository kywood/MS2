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


            //TODO
            // 여기서 패킷을 보낸다.
            // 바로 해당 유저의 게임 오버 패킷을
            // 양쪽에 팝을을 올리고 게임 리절트
            //그리고 로비로 이동..............
            // 그리고 로비로 나간다.

           // Debug.Log("GameOver!!");
            GamePopup.Instance.Active((int)GamePopup.eWindows.GameOver, true);

        }
    }
}
