using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GamePopup;
using static PlayerPopup;

public class PlayerEnd : PlayerState<PlayerStateManager>
{

    protected Player Player;

    public PlayerEnd(PlayerStateManager state_manager) : base(state_manager)
    {
    }

    public override void OnEnter()
    {
        Player = GetPlayer();
        Player.GetPlayerPopup().Active((int)ePlayerWindows.GameEnd, true);

        Player.SetVisiblePick(false);


        packetDequeState.Init();

        if (PlayerManager.Instance.IsMyPlayer(Player))
            packetDequeState.AddWillRecvPacketId(MsgId.SGameResult);
    }

    public override void OnLeave()
    {

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (PlayerManager.Instance.IsMyPlayer(Player))
        {

            if (packetDequeState.PacketState == PacketDequeStates.ePkState.Processing)
            {
                {
                    NetPacket pk = ((OnlinePlayer)Player).PacketDeQueue(MsgId.SGameResult);
                    if (pk != null)
                    {
                        Player.GetPlayerPopup().Active((int)ePlayerWindows.GameEnd, false);

                        GamePopup.Instance.Active((int)eWindows.GameResult, true);

                        GameObject gameResultGameObject = GamePopup.Instance.GetWindow((int)eWindows.GameResult);
                        GameResultWin gameResultWindow = gameResultGameObject.GetComponent<GameResultWin>();

                        gameResultWindow.UpdateRankText();
                    }
                }
            }

        }

    }
}
