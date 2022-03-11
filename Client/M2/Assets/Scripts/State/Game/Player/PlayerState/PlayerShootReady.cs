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



    protected Player Player;

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


        //여기서 보여준다.
        Player.BubbleManager.GetComponent<BubbleManager>().SetVisible(true);

      
        Player.Pick.SetActive(true);

        Init();
    }


    protected virtual void Init()
    {

    }


    protected virtual void Shoot(float scale = 1.0f)
    {      

        Bubble.transform.position = ShootBody.transform.position;
        //float fAngle = CMath.GetAngle(ShootBody.transform.position, Target.transform.position);

        float radianAngle = Random.Range(40, 140) * Mathf.Deg2Rad;
        Vector2 vel = (new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle))).normalized * (Defines.G_SHOOT_FORCE * scale);
        RbBubble.velocity = vel;
    }


    public override void OnUpdate()
    {
        base.OnUpdate();


    }
}
