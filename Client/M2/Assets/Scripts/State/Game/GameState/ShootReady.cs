using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootReady : State
{
    // Start is called before the first frame update
    private GameObject Target;
    private GameObject ShootBody;
    private GameObject Bubble;
    private Rigidbody2D RbBubble;

    bool bMousePress;

    public override void OnEnter()
    {
        
        Target = GameManager.Instance.GetMyPlayer().Pick.GetComponent<Pick>().Target;
        ShootBody = GameManager.Instance.GetMyPlayer().Pick.GetComponent<Pick>().ShootBody;


        Bubble = GameManager.Instance.GetMyPlayer().BubbleManager.GetComponent<BubbleManager>().shootBubble;
        RbBubble = Bubble.GetComponent<Rigidbody2D>();

        GameManager.Instance.GetMyPlayer().BubbleManager.GetComponent<BubbleManager>().SetVisible(true);


        GameManager.Instance.GetMyPlayer().Next.GetComponent<Next>().SetVisible(true);
        GameManager.Instance.GetMyPlayer().Next.GetComponent<Next>().UpdateNext();

        GameManager.Instance.GetMyPlayer().Pick.SetActive(true);


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

    void Shoot()
    {
        Bubble.transform.position = ShootBody.transform.position;
        float fAngle = CMath.GetAngle(ShootBody.transform.position, Target.transform.position);
        float RadianAngle = fAngle * Mathf.Deg2Rad;
        Vector2 vel = (new Vector2(Mathf.Cos(RadianAngle), Mathf.Sin(RadianAngle))).normalized * Defines.G_SHOOT_FORCE;
        RbBubble.velocity = vel;
        //Debug.Log("Shoot f : " + fAngle + " R : " + fAngle * Mathf.Deg2Rad);
    }


    public override void OnUpdate()
    {
        base.OnUpdate();

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
                GameManager.Instance.GetGameStateManager().SetGameState(GameStateManager.E_GAME_STATE.RUN);
                Shoot();
            }

            bMousePress = false;
        }
    }
}