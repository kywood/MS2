using RotSlot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ConstData;
using static Defines;

public class Bubble : MonoBehaviour
{
    public GameObject bubbleManager;

    Player _player;

    public Player Player { get { return _player; } }


    public void Start()
    {
        _player = GetPlayer();

        OnStart();
    }

    protected virtual void OnStart()
    {

        transform.localScale = new Vector3(Player.Scale,
            Player.Scale,
            1);
    }

    protected Player GetPlayer()
    {
        BubbleManager bbm = null;


        bbm = transform.parent.GetComponent<BubbleManager>();

        if ( bbm == null )
        {
            bbm = transform.parent.transform.parent.GetComponent<BubbleManager>();
        }
        

        return bbm.Player;
    }


    protected BubbleManager GetBubbleManager()
    {
        return bubbleManager.GetComponent<BubbleManager>();
    }

    public void SetVisible(bool value)
    {
        gameObject.SetActive(value);
    }


}
