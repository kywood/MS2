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
        GAME ,
        MAX
    }

    private static Dictionary<E_SCENES, String> mScenesNms = new Dictionary<E_SCENES, string>()
        {
            { E_SCENES.LOGO , "LOGO" } ,
            { E_SCENES.TITLE , "TITLE" },
            { E_SCENES.MENU , "MENU" },
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

    public const float G_BUBBLE_DROP_GRAVITY_SCALE = 1.5f;
    public const float G_BUBBLE_FORCE_SCALE = 0.02f;
    public const float G_SHOOT_FORCE = 15.0f;

    

    //public const float G_BUBBLE_MOVING_SPEED = 5.0f;

    public const int G_BUBBLE_ROW_COUNT = 12;
    public const int G_BUBBLE_COL_COUNT = 8;

    public const float G_SLOT_RADIUS = 0.3f;

    

}
