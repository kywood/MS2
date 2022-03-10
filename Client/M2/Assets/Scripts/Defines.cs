using RotSlot;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defines 
{


    public enum E_WALL_NM
    {
        WT,
        WB,
        WL,
        WR
    }

    public enum E_SCENES
    {
        LOGO,
        TITLE,
        MENU ,
        LOBBY ,
        GAME ,
        MAX
    }

    private static Dictionary<E_SCENES, String> mScenesNms = new Dictionary<E_SCENES, string>()
        {
            { E_SCENES.LOGO , "LOGO" } ,
            { E_SCENES.TITLE , "TITLE" },
            { E_SCENES.MENU , "MENU" },
            { E_SCENES.LOBBY , "LOBBY" },
            { E_SCENES.GAME , "GAME" },
        };

    public static String GetScenesName(E_SCENES scene)
    {
        return mScenesNms[scene];
    }


    public enum E_MOVING_STATE
    {
        STOP,
        MOVE,

        MAX
    }


    public const int G_BUBBLE_PANG_COUNT = 3;

    public const float G_BUBBLE_DROP_GRAVITY_SCALE = 1.8f;
    public const float G_BUBBLE_RIGIDBODY_FORCE = 0.025f;
    public const float G_SHOOT_FORCE = 15.0f;


    public const string SERVER_IPADDR = "192.168.123.107";
    public const int    SERVER_PORT = 7779;
    

    //public const float G_BUBBLE_MOVING_SPEED = 5.0f;

    public const int G_BUBBLE_ROW_COUNT = 12;
    public const int G_BUBBLE_COL_COUNT = 8;

    public const int G_BUBBLE_START_ROW_COUNT = 5;

    public const int G_DROP_LOOP_TICK = 5;


    public const float G_BUBBLE_DIAMETER = 0.40f;
    //public const float G_BUBBLE_DIAMETER = 0.6f;
    public const float G_SLOT_RADIUS_GAP = 0.03f;


}
