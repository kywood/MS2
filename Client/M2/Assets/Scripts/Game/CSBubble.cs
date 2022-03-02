using RotSlot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Defines;

public class CSBubble : Bubble
{
    cBubble mBubble = null;

    E_MOVING_STATE mMovingState = E_MOVING_STATE.STOP;

    CSSlot mCsSlot;

    public void SetBubbleWithPos( Player player , cBubble bubble, CSSlot cs_slot )
    {
        mMovingState = E_MOVING_STATE.STOP;

        mBubble = bubble;

        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        sp.sprite = player.GetBubbleManager().GetSprite(bubble.GetBubbleType());

        GetComponent<Rigidbody2D>().gravityScale = 0f;

        transform.position = cs_slot.transform.position;

        mCsSlot = cs_slot;
    }

    public void PangAct()
    {
        mMovingState = E_MOVING_STATE.MOVE;

        GetComponent<Rigidbody2D>().gravityScale = G_BUBBLE_DROP_GRAVITY_SCALE;
        
        Vector2 v2 = CMath.AngleToPoint2(Random.Range(40, 140));

        GetComponent<Rigidbody2D>().AddForce(v2.normalized * G_BUBBLE_RIGIDBODY_FORCE);
    }

    public void SetMoving()
    {
        mMovingState = E_MOVING_STATE.MOVE;

        GetComponent<Rigidbody2D>().gravityScale = G_BUBBLE_DROP_GRAVITY_SCALE;
        //GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f, 1f));
    }

    public bool IsStayState()
    {
        if (mMovingState != E_MOVING_STATE.MOVE)
            return true;

        return false;
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
        if( transform.position.y < Player.Walls.GetComponent<Walls>().WB.transform.position.y )
        {
            SetActive(false);
        }

        if(mMovingState != E_MOVING_STATE.MOVE)
        {
            transform.position = mCsSlot.transform.position;
        }

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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.CompareTo(E_WALL_NM.WB.ToString()) == 0)
        {
            SetActive(false);
        }
    }

}
