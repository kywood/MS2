using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShootReady : PlayerState<PlayerStateManager>
{
    // Start is called before the first frame update
    protected GameObject Target;
    protected GameObject ShootBody;
    protected GameObject Bubble;
    protected Rigidbody2D RbBubble;

    protected bool bMousePress;


    Player Player;

    public PlayerShootReady(PlayerStateManager state_manager) : base(state_manager)
    {
    }

    public override void OnEnter()
    {
        Player = GetPlayer();

        Target = Player.Pick.GetComponent<Pick>().Target;
        ShootBody = Player.Pick.GetComponent<Pick>().ShootBody;


        Bubble = ((ShootBubbleManager)Player.BubbleManager.GetComponent<BubbleManager>()).shootBubble;
        RbBubble = Bubble.GetComponent<Rigidbody2D>();

        Player.BubbleManager.GetComponent<BubbleManager>().SetVisible(true);

      
        Player.Pick.SetActive(true);

        Init();
    }

    public override void OnLeave()
    {
        //Debug.Log("ShootReady OnLeave");
    }

    protected virtual void Init()
    {
        bMousePress = false;

       // GameManager.Instance.StartCoroutine(EffectStartRow());


    }


    //IEnumerator EffectStartRow()
    //{
    //    yield return new WaitForSeconds(3.2f);

    //    GetPlayer().SetPlayerState(PlayerStateManager.E_PLAYER_STATE.RUN);
    //    //                GameManager.Instance.GetGameStateManager().SetGameState(GameStateManager.E_GAME_STATE.RUN);
    //    Shoot(
    //        GetPlayer().Scale
    //        );


    //    yield return null;
    //}
    


    protected virtual void Shoot(float scale = 1.0f)
    {
        Bubble.transform.position = ShootBody.transform.position;
        //float fAngle = CMath.GetAngle(ShootBody.transform.position, Target.transform.position);

        float radianAngle = Random.Range(40, 140) * Mathf.Deg2Rad;
        Vector2 vel = (new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle))).normalized * (Defines.G_SHOOT_FORCE * scale);
        RbBubble.velocity = vel;
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

        NetPacket pk = ((OnlinePlayer)Player).PacketDeQueue(MsgId.SShoot);
        if( pk != null)
        {
            Shootlocal(pk);
        }

    }
}
