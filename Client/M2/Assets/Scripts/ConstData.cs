using RotSlot;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ConstData
{
    public enum E_PREFAB_TYPE
    {
        SLOT ,
        COLS_SLOT ,
        MAX
    }


    public class cPreFabProperty
    {
        public cPreFabProperty(string nm)
        {
            mNM = nm;
        }

        public string mNM;
    }

    private static Dictionary<E_PREFAB_TYPE, cPreFabProperty> mPreFabProperty
        = new Dictionary<E_PREFAB_TYPE, cPreFabProperty>()
        {
            { E_PREFAB_TYPE.COLS_SLOT , new cPreFabProperty( "ColsSlot" ) } ,
            { E_PREFAB_TYPE.SLOT , new cPreFabProperty( "Slot" ) }
        };

    public static cPreFabProperty GetPreFabProperty(E_PREFAB_TYPE p_type )
    {
        return mPreFabProperty[p_type];
    }

    private static List<E_SLOT_CHK_DIR> mSlotChkDirStay = new List<E_SLOT_CHK_DIR>()
    {   E_SLOT_CHK_DIR.UP_LEFT ,
        E_SLOT_CHK_DIR.UP_RIGHT ,
        E_SLOT_CHK_DIR.LEFT ,
        E_SLOT_CHK_DIR.RIGHT ,
        E_SLOT_CHK_DIR.DOWN_LEFT ,
        E_SLOT_CHK_DIR.DOWN_RIGHT
    };

    public static List<E_SLOT_CHK_DIR> GetChkStaySlotOffset()
    {
        return mSlotChkDirStay;
    }




    public class cBubbleProperty
    {
        //public Color mColor;

        public String mImgPath;
        public E_BUBBLE_TYPE mBubbleType;

        public cBubbleProperty(E_BUBBLE_TYPE bubble_type)
        {
            mBubbleType = bubble_type;
        }

        public cBubbleProperty(E_BUBBLE_TYPE bubble_type, String img_path)
            //: base(bubble_type)
        {
            mBubbleType = bubble_type;
            mImgPath = img_path;
        }
    }

    private static Dictionary<E_BUBBLE_TYPE, cBubbleProperty> mBubblePropertys = new Dictionary<E_BUBBLE_TYPE, cBubbleProperty>()
    {
        { E_BUBBLE_TYPE.RED  , new cBubbleProperty(E_BUBBLE_TYPE.RED ,  "Imgs/Bubble/3"  ) },
        { E_BUBBLE_TYPE.BLUE , new cBubbleProperty(E_BUBBLE_TYPE.BLUE , "Imgs/Bubble/12" ) },
        { E_BUBBLE_TYPE.YELLOW  , new cBubbleProperty(E_BUBBLE_TYPE.YELLOW , "Imgs/Bubble/1" ) },
        { E_BUBBLE_TYPE.GREEN  , new cBubbleProperty(E_BUBBLE_TYPE.GREEN , "Imgs/Bubble/13" ) },
        { E_BUBBLE_TYPE.PURPLE  , new cBubbleProperty(E_BUBBLE_TYPE.PURPLE , "Imgs/Bubble/10" ) },
        { E_BUBBLE_TYPE.SPECIAL  , new cBubbleProperty(E_BUBBLE_TYPE.SPECIAL , "Imgs/Bubble/9" ) }
    };

    private static List<int> mBallTypeRandomSeed = new List<int>()
    {
        (int)E_BUBBLE_TYPE.RED ,
        (int)E_BUBBLE_TYPE.BLUE ,
        (int)E_BUBBLE_TYPE.YELLOW ,
        (int)E_BUBBLE_TYPE.GREEN ,
        (int)E_BUBBLE_TYPE.PURPLE ,
    };


    //private static Dictionary<E_BUBBLE_TYPE, cBubbleProperty> mBubblePropertys = new Dictionary<E_BUBBLE_TYPE, cBubbleProperty>()
    //{
    //    { E_BUBBLE_TYPE.RED  , new cBubbleProperty(E_BUBBLE_TYPE.RED ,  Util.NewColor(0xFF0021)  ) },
    //    { E_BUBBLE_TYPE.BLUE , new cBubbleProperty(E_BUBBLE_TYPE.BLUE , Util.NewColor(0x0066FF) ) },
    //    { E_BUBBLE_TYPE.YELLOW  , new cBubbleProperty(E_BUBBLE_TYPE.YELLOW , Util.NewColor(0xF7FF00) ) },
    //    { E_BUBBLE_TYPE.GREEN  , new cBubbleProperty(E_BUBBLE_TYPE.GREEN , Util.NewColor(0x00FF23) ) },
    //    { E_BUBBLE_TYPE.PURPLE  , new cBubbleProperty(E_BUBBLE_TYPE.PURPLE , Util.NewColor(0xB400FF) ) }
    //};

    public static Dictionary<E_BUBBLE_TYPE, cBubbleProperty> GetBubblePropertys()
    {
        return mBubblePropertys;
    }

    public static cBubbleProperty GetBubbleProperty(E_BUBBLE_TYPE bubble_type)
    {
        return mBubblePropertys[bubble_type];
    }

    public static E_BUBBLE_TYPE GetNextBubbleType()
    {
        return (E_BUBBLE_TYPE)UnityEngine.Random.Range(mBallTypeRandomSeed[0], mBallTypeRandomSeed[mBallTypeRandomSeed.Count - 1 ] + 1);
        //return (E_BUBBLE_TYPE)UnityEngine.Random.Range((int)E_BUBBLE_TYPE.NONE + 1, (int)E_BUBBLE_TYPE.MAX );
    }

}
