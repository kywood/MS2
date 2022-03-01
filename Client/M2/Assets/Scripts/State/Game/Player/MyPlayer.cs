using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : Player
{

    public GameObject Pick;
    public GameObject BubbleManager;
    public GameObject Walls;
    public GameObject WallMaskArea;

    public GameObject RotSlot;
    public GameObject BG;


    protected override void OnStart()
    {
        base.OnStart();

        //Walls.GetComponent<Walls>().WB.transform.position;



    }

}
