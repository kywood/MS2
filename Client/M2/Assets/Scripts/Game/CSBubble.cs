using RotSlot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Defines;

public class CSBubble : MonoBehaviour
{
    cBubble mBubble = null;

    E_MOVING_STATE mMovingState = E_MOVING_STATE.STOP;

    public void SetBubbleWithPos(cBubble bubble, Vector3 pos)
    {
        mMovingState = E_MOVING_STATE.STOP;

        mBubble = bubble;

        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        sp.sprite = GameManager.Instance.GetBubbleManager().GetSprite(bubble.GetBubbleType());

        GetComponent<Rigidbody2D>().gravityScale = 0f;

        transform.position = pos;
    }

    public void PangAct()
    {
        GetComponent<Rigidbody2D>().gravityScale = G_BUBBLE_DROP_GRAVITY_SCALE;
        //GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.01f, 0.01f));

        
        Vector2 v2 = CMath.AngleToPoint2(Random.Range(40, 140));

        GetComponent<Rigidbody2D>().AddForce(v2.normalized * G_BUBBLE_FORCE_SCALE);


    }



    public void SetMoving()
    {
        mMovingState = E_MOVING_STATE.MOVE;

        GetComponent<Rigidbody2D>().gravityScale = G_BUBBLE_DROP_GRAVITY_SCALE;
        //GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f, 1f));

    }

    public E_MOVING_STATE GetMoving()
    {
        return mMovingState;
    }

     void ReSetMoving()
    {
        mMovingState = E_MOVING_STATE.STOP;
    }

    public void ReSet()
    {
        mBubble = null;
    }

    public void Update()
    {
        if( transform.position.y < GameManager.Instance.Walls.GetComponent<Walls>().WB.transform.position.y )
        {
            SetActive(false);
        }

        //AppManager.Instance.Walls.GetComponent<Walls>().WB.transform.y

        //if (mMovingState == E_MOVING_STATE.MOVE)
        //{
        ////    Vector3 cv = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        ////    cv.y -= G_BUBBLE_MOVING_SPEED * Time.deltaTime;
        ////    transform.position = cv;
        //}
    }

    public void SetActive( bool active )
    {
        if( active == false )
        {
            ReSetMoving();
        }
        gameObject.SetActive(active);
    }

    public bool IsEqBubble(cBubble bb )
    {
        return mBubble.IsEqID(bb.GetID());
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if( collision.gameObject.name.CompareTo("WB") == 0 )
    //    {
    //        SetActive(false);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.CompareTo(E_WALL_NM.WB.ToString()) == 0)
        {
            //Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAA");
            SetActive(false);
        }
    }

}
