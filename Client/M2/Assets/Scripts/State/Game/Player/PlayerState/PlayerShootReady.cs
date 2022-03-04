using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootReady : PlayerState<PlayerStateManager>
{
    // Start is called before the first frame update
    private GameObject Target;
    private GameObject ShootBody;
    private GameObject Bubble;
    private Rigidbody2D RbBubble;

    bool bMousePress;

    public PlayerShootReady(PlayerStateManager state_manager) : base(state_manager)
    {
    }

    public override void OnEnter()
    {

        //TODO

        Player Player = GetPlayer();

        Target = Player.Pick.GetComponent<Pick>().Target;
        ShootBody = Player.Pick.GetComponent<Pick>().ShootBody;


        Bubble = ((ShootBubbleManager)Player.BubbleManager.GetComponent<BubbleManager>()).shootBubble;
        RbBubble = Bubble.GetComponent<Rigidbody2D>();

        Player.BubbleManager.GetComponent<BubbleManager>().SetVisible(true);

        if (Player.PlayerType == PlayerManager.E_PLAYER_TYPE.MY_PLAYER)
        {
            ((MyPlayer)Player).Next.GetComponent<Next>().SetVisible(true);
            ((MyPlayer)Player).Next.GetComponent<Next>().UpdateNext();
        }
        Player.Pick.SetActive(true);

        Init();
    }

    public override void OnLeave()
    {
        //Debug.Log("ShootReady OnLeave");
    }

    private void Init()
    {
        bMousePress = false;

        //        RbBubble = Bubble.GetComponent<Rigidbody2D>();
    }

    void Shoot(float scale = 1.0f)
    {
        Bubble.transform.position = ShootBody.transform.position;
        float fAngle = CMath.GetAngle(ShootBody.transform.position, Target.transform.position);
        float RadianAngle = fAngle * Mathf.Deg2Rad;
        Vector2 vel = (new Vector2(Mathf.Cos(RadianAngle), Mathf.Sin(RadianAngle))).normalized * (Defines.G_SHOOT_FORCE * scale);
        RbBubble.velocity = vel;
        //Debug.Log("Shoot f : " + fAngle + " R : " + fAngle * Mathf.Deg2Rad);
    }


    public override void OnUpdate()
    {
        base.OnUpdate();

        if (GetPlayer().PlayerType != PlayerManager.E_PLAYER_TYPE.MY_PLAYER)
            return;

        if (Input.GetMouseButton(0))
        {
            bMousePress = true;

            Vector3 wPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            wPos.z = 0;
            Target.transform.position = wPos;

            float dis = Util.Distance(Target.transform.position, ShootBody.transform.position);
            float angle = CMath.GetAngle(Target.transform.position, ShootBody.transform.position);
            //Debug.Log(" Dis : " + dis + " Angle : " + angle);
        }
        else
        {
            if (bMousePress)
            {
                GetPlayer().SetPlayerState(PlayerStateManager.E_PLAYER_STATE.RUN);
//                GameManager.Instance.GetGameStateManager().SetGameState(GameStateManager.E_GAME_STATE.RUN);
                Shoot(
                    GetPlayer().Scale
                    );
            }

            bMousePress = false;
        }
    }
}
