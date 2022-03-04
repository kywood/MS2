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

           // Debug.Log("GameOver!!");
            GamePopup.Instance.Active((int)GamePopup.eWindows.GameOver, true);

        }
    }
}
