using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PeerPlayerShootReady : PlayerShootReady
{

    List<Vector3> _targetLocalPos = new List<Vector3>();

    Vector3 _curPos;
    Vector3 _targetPos;

    bool _posSet = false;

    Vector3 _lastPos;

    float _fllowTime;

    public PeerPlayerShootReady(PlayerStateManager state_manager) : base(state_manager)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        ((OnlinePlayer)Player).PacketClearQueue(MsgId.SMove);

        _curPos = Target.transform.localPosition;
        //        Init();
    }


    protected override void Init()
    {

    }



    void Shootlocal(NetPacket packet )
    {
        float radianAngle = ((S_Shoot)packet.Packet).RadianAngle;
        Vector2 vel = (new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle))).normalized * (Defines.G_SHOOT_FORCE * GetPlayer().Scale);
        RbBubble.velocity = vel;
    }


    public override void OnUpdate()
    {
        base.OnUpdate();

        {
            NetPacket pk = ((OnlinePlayer)Player).PacketDeQueue(MsgId.SMove);
            if (pk != null)
            {
                S_Move move = pk.Packet as S_Move;

                //_targetPos= new Vector3(move.PosX, move.PosY, 0.0f);

                //Target.transform.localPosition = new Vector3(move.PosX, move.PosY, 0.0f);

                _targetLocalPos.Add(new Vector3(move.PosX, move.PosY, 0.0f));

            }
        }

        {
            NetPacket pk = ((OnlinePlayer)Player).PacketDeQueue(MsgId.SShoot);
            if (pk != null)
            {
                GetPlayer().SetPlayerState(PlayerStateManager.E_PLAYER_STATE.RUN);

                Shootlocal(pk);
            }
        }

        {
            
            if(_posSet == false && _targetLocalPos.Count> 0)
            {
                //_lastPos = _targetLocalPos[_targetLocalPos.Count - 1];
                _lastPos = _targetLocalPos[0];
                _posSet = true;
                _fllowTime = 0.0f;

                _targetLocalPos.RemoveAt(0);
            }

            if( _posSet)
            {
                // _fllowTime
                _fllowTime += Time.deltaTime;
                float fTime = (_fllowTime) / (1.0f / 4.0f);

                if (fTime >= 1.0f)
                {
                    fTime = 1.0f;
                    _posSet = false;
                }

                Target.transform.localPosition = Vector3.Lerp(Target.transform.localPosition, _lastPos , fTime);

                //if (fTime > 1.0f)
                //{
                //    fTime = 1.0f;
                //    _posSet = false;
                //}
            }
            
        }
        

    }
}
